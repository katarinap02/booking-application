using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.GuideWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class RequestMainViewModel : INotifyPropertyChanged
    {
        #region POLJA
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
        #endregion

        public MyICommand Clear {  get; set; }
        public MyICommand Filtering { get; set; }
        public MyICommand Accept { get; set; }
        public MyICommand ShowingStats { get; set; }

        public RequestMainViewModel(int ID) 
        {
            Clear = new MyICommand(Testic);
            Filtering = new MyICommand(Filter);
            Accept = new MyICommand(Accepting);
            ShowingStats = new MyICommand(ShowStats);
            GuideId = ID;
            SelectedRequest = new TourRequestDTOViewModel();
            SearchCriteria = new TourRequestDTOViewModel(DateTime.MaxValue);
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            tourRequestViewModels = new ObservableCollection<TourRequestDTOViewModel>();
            tourRequestViewModels = getData();
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

        private void Testic()
        {
            SearchCriteria.StartDate = DateTime.MinValue;
            SearchCriteria.EndDate = DateTime.MaxValue;
            SearchCriteria.Language = "";
            SearchCriteria.City = "";
            SearchCriteria.Country = "";
            ParticipantNumber = 0;
        }

        private void Filter()
        {
            tourRequestViewModels.Clear();

            var newData = getData();
            foreach (var item in newData)
            {
                tourRequestViewModels.Add(item);
            }

            MessageBox.Show(tourRequestViewModels.Count.ToString());
        }

        private void Accepting()
        {
            if (SelectedRequest != null && AcceptingDate != DateTime.MinValue && AcceptingDate != DateTime.MaxValue)
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

        private void ShowStats()
        {
            RequestStatsWindow requestStatsWindow = new RequestStatsWindow(GuideId);
            requestStatsWindow.Show();
        }
    }
}
