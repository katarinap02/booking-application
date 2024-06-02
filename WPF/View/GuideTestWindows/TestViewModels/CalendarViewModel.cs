using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class CalendarViewModel: ViewModelBase
    {
        private int GuideId;
        private DateTime _displayDateStart;
        private DateTime _displayDateEnd;

        public ObservableCollection<DateTime> BlackoutDates { get; set; }

        public DateTime DisplayDateStart
        {
            get => _displayDateStart;
            set
            {
                if (_displayDateStart != value)
                {
                    _displayDateStart = value;
                    OnPropertyChanged(nameof(DisplayDateStart));
                }
            }
        }

        public DateTime DisplayDateEnd
        {
            get => _displayDateEnd;
            set
            {
                if (_displayDateEnd != value)
                {
                    _displayDateEnd = value;
                    OnPropertyChanged(nameof(DisplayDateEnd));
                }
            }
        }
        public ObservableCollection<DateTime> blackoutDates {  get; set; }
        private readonly TourRequestService _tourRequestService;
        public CalendarViewModel(int GuideId, TourRequestDTOViewModel tourRequest)
        {
            this.GuideId = GuideId;
            blackoutDates = new ObservableCollection<DateTime>();
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            DisplayDateEnd = tourRequest.EndDate;
            DisplayDateStart = tourRequest.StartDate;
            GetUnavailableDates();
        }

        private void GetUnavailableDates()
        {
            List<DateTime> dateTimes = _tourRequestService.getUnavailableDates(GuideId, DisplayDateStart, DisplayDateEnd);
            if(dateTimes == null) { return; }
            foreach (DateTime date in dateTimes)
            {
                blackoutDates.Add(date);
            }
        }
    }
}
