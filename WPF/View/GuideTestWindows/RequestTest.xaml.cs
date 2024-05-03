using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.GuideTestWindows
{
    public partial class RequestTest: Window
    {
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequestDTOViewModel> tourRequestViewModels { get; set; }
        public RequestTest()
        {
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            InitializeComponent();
            DataContext = this;
            tourRequestViewModels = getData();
        }

        public ObservableCollection<TourRequestDTOViewModel> getData() 
        {
            List<int> ints = new List<int>();
            DateTime dateTime = new DateTime(2023, 1, 1);
            DateTime dateTime2 = new DateTime(2024, 6, 30);
            TourRequest searchCriteria = new TourRequest("desc", "English", ints, dateTime, dateTime2, "", "");
            List<TourRequest> tourRequests = _tourRequestService.GetRequestsForLastYear();
            ObservableCollection<TourRequestDTOViewModel> viewModels = new ObservableCollection<TourRequestDTOViewModel>();
            foreach (var tourRequest in tourRequests)
            {
                viewModels.Add(new TourRequestDTOViewModel(tourRequest));
            }            
            return viewModels;
        }
    }
}
