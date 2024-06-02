using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
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

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class TourRequestsViewModel: ViewModelBase, INotifyPropertyChanged
    {
        #region POLJA
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequestDTOViewModel> tourRequestViewModels { get; set; }
        public TourRequestDTOViewModel SearchCriteria { get; set; }
        public TourRequestDTOViewModel SelectedRequest { get; set; }

        public StringToNumberViewModel ParticipantnumberString { get; set; }

        private DateTime _AcceptingDate;
        public DateTime AcceptingDate
        {
            get { return _AcceptingDate; }
            set { _AcceptingDate = value; NotifyPropertyChanged(nameof(AcceptingDate)); }
        }
        private int GuideId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MyICommand Clear { get; set; }
        public MyICommand Filtering { get; set; }
        public MyICommand Accept { get; set; }

        public TourRequestsViewModel() { }
        public TourRequestsViewModel(int guide_id) 
        {
            GuideId = guide_id;
            Clear = new MyICommand(ClearData);
            Filtering = new MyICommand(Filter);
            Accept = new MyICommand(Accepting);
            ParticipantnumberString = new StringToNumberViewModel();
            SelectedRequest = new TourRequestDTOViewModel();
            SearchCriteria = new TourRequestDTOViewModel(DateTime.MaxValue);
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            tourRequestViewModels = new ObservableCollection<TourRequestDTOViewModel>();
            tourRequestViewModels = getData();
        }

        public ObservableCollection<TourRequestDTOViewModel> getData()
        {
            List<TourRequest> tourRequests = _tourRequestService.filterRequests(SearchCriteria.ToTourRequest(), ParticipantnumberString.convertToInt());

            ObservableCollection<TourRequestDTOViewModel> viewModels = new ObservableCollection<TourRequestDTOViewModel>();
            foreach (var tourRequest in tourRequests)
            {
                viewModels.Add(new TourRequestDTOViewModel(tourRequest));
            }
            return viewModels;
        }

        private void ClearData()
        {
            SearchCriteria.StartDate = DateTime.MinValue;
            SearchCriteria.EndDate = DateTime.MaxValue;
            SearchCriteria.Language = "";
            SearchCriteria.City = "";
            SearchCriteria.Country = "";
            ParticipantnumberString.Number = "";
        }

        private void Filter()
        {
            tourRequestViewModels.Clear();

            var newData = getData();
            foreach (var item in newData)
            {
                tourRequestViewModels.Add(item);
            }

        }

        private void Accepting()
        {
            if(SelectedRequest == null) { return; } // izmeniti
            CalendarGuideWindow calendarGuideWindow = new CalendarGuideWindow(GuideId, SelectedRequest);
            calendarGuideWindow.Show();
        }
    }
}
