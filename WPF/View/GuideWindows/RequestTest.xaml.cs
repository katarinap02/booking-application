using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.View.GuideWindows
{
    public partial class RequestTest: Window, INotifyPropertyChanged
    {
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequestDTOViewModel> tourRequestViewModels { get; set; }
        public TourRequestDTOViewModel SearchCriteria { get; set; }
        public TourRequestDTOViewModel SelectedRequest { get; set; }

        private int _participantNumber;
        public int ParticipantNumber
        {
            get => _participantNumber;
            set
            {
                _participantNumber = value;
                NotifyPropertyChanged(nameof(ParticipantNumber));
            }
        }

        private DateTime _AcceptingDate;
        public DateTime AcceptingDate
        {
            get { return _AcceptingDate; }
            set { _AcceptingDate = value; NotifyPropertyChanged(nameof(AcceptingDate)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int GuideId;

        public RequestTest(int ID)
        {
            GuideId = ID;
            SelectedRequest = new TourRequestDTOViewModel();
            SearchCriteria = new TourRequestDTOViewModel(DateTime.MaxValue);
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());            
            DataContext = this;
            tourRequestViewModels = new ObservableCollection<TourRequestDTOViewModel>();
            tourRequestViewModels = getData();
            InitializeComponent();
        }

        public ObservableCollection<TourRequestDTOViewModel> getData() 
        {            
            List<TourRequest> tourRequests = _tourRequestService.filterRequests(SearchCriteria.ToTourRequest(), ParticipantNumber); 
            
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
            ParticipantNumber = 0;
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            tourRequestViewModels.Clear();

            // Add new items to the existing collection
            var newData = getData();
            foreach (var item in newData)
            {
                tourRequestViewModels.Add(item);
            }

            MessageBox.Show(tourRequestViewModels.Count.ToString());
        }

        private void Accepting(object sender, RoutedEventArgs e)
        {
            if(SelectedRequest != null && AcceptingDate != DateTime.MinValue && AcceptingDate != DateTime.MaxValue)
            {
                if (AcceptingDate <= SelectedRequest.EndDate)
                {
                    if (AcceptingDate >= SelectedRequest.StartDate)
                    {
                        _tourRequestService.AcceptRequest(SelectedRequest.ToTourRequest(), GuideId, AcceptingDate);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Date that is after the start date.", "Error while accepting a request");
                    }

                }
                else
                {
                    MessageBox.Show("Please select a Date that is before the end date.", "Error while accepting a request");
                }

            }
            else
            {                
                MessageBox.Show("Please select a request before proceeding, and select a date.", "Error while accepting a request");
            }
        }

        private void ShowStats(object sender, RoutedEventArgs e)
        {
            RequestStatsWindow requestStatsWindow = new RequestStatsWindow(GuideId);
            requestStatsWindow.Show();
        }
    }
}
