using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BookingApp.WPF.View.GuideWindows
{
    public partial class RequestTest: Window
    {
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequestDTOViewModel> tourRequestViewModels { get; set; }
        public TourRequestDTOViewModel SearchCriteria { get; set; }

        public RequestTest()
        {
            DataContext = this;
            SearchCriteria = new TourRequestDTOViewModel(DateTime.MaxValue);
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());            
            DataContext = this;
            tourRequestViewModels = new ObservableCollection<TourRequestDTOViewModel>();
            tourRequestViewModels = getData();
            InitializeComponent();
        }

        public ObservableCollection<TourRequestDTOViewModel> getData() 
        {            
            List<TourRequest> tourRequests = _tourRequestService.filterRequests(SearchCriteria.ToTourRequest()); 
            
            ObservableCollection<TourRequestDTOViewModel> viewModels = new ObservableCollection<TourRequestDTOViewModel>();
            foreach (var tourRequest in tourRequests)
            {
                viewModels.Add(new TourRequestDTOViewModel(tourRequest));
            }
            return viewModels;
        }

        private void Testic(object sender, RoutedEventArgs e)
        {
            SearchCriteria.StartDate = DateTime.MinValue;
            SearchCriteria.EndDate = DateTime.MaxValue;
            SearchCriteria.Language = "";
            SearchCriteria.City = "";
            SearchCriteria.Country = "";
            // no of tourists
        }
    }
}
