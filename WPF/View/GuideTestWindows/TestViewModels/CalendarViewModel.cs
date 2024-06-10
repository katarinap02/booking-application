using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class CalendarViewModel: ViewModelBase
    {
        #region Polja
        private int GuideId;
        private DateTime _displayDateStart;
        private DateTime _displayDateEnd;

        public DateTime DisplayDateStart
        {
            get { return _displayDateStart; }
            set
            {
                if (_displayDateStart != value)
                {
                    _displayDateStart = value;
                    OnPropertyChanged("DisplayDateStart");
                }
            }
        }

        public DateTime DisplayDateEnd
        {
            get { return _displayDateEnd; }
            set
            {
                if (_displayDateEnd != value)
                {
                    _displayDateEnd = value;
                    OnPropertyChanged("DisplayDateEnd");
                }
            }
        }

        private string _AmPm;
        public string AmPm
        {
            get { return _AmPm; }
            set
            {
                _AmPm = value;
                OnPropertyChanged(nameof(AmPm));
            }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
        #endregion 

        private bool _isDateErrorVisible;
        public bool IsDateErrorVisible
        {
            get { return _isDateErrorVisible; }
            set
            {
                _isDateErrorVisible = value;
                OnPropertyChanged(nameof(IsDateErrorVisible));
            }
        }

        private bool _isTimeErrorVisible;
        public bool IsTimeErrorVisible
        {
            get { return _isTimeErrorVisible; }
            set
            {
                _isTimeErrorVisible = value;
                OnPropertyChanged(nameof(IsTimeErrorVisible));
            }
        }

        public ObservableCollection<string> options {  get; set; }
        public ObservableCollection<DateTime> blackoutDates {  get; set; }
        private readonly TourRequestService _tourRequestService;
        private TourRequest tourRequest { get; set; }
        public MyICommand Accept { get; set; }

        public bool Successfull { get; set; }
        public CalendarViewModel(int GuideId, TourRequestDTOViewModel tourRequest)
        {
            Successfull = false;
            IsDateErrorVisible = false;
            IsTimeErrorVisible = false;
            this.tourRequest = tourRequest.ToTourRequest(); 
            Accept = new MyICommand(Accepting);
            this.GuideId = GuideId;
            _AmPm = "AM";
            options = new ObservableCollection<string> { "AM", "PM" };
            blackoutDates = new ObservableCollection<DateTime>();
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            DisplayDateEnd = tourRequest.EndDate;
            DisplayDateStart = tourRequest.StartDate;
            SelectedDate = tourRequest.StartDate;
            List<DateTime> dateTimes = _tourRequestService.getUnavailableDates(GuideId, DisplayDateStart, DisplayDateEnd);
            if (dateTimes == null) { return; }
            foreach (DateTime date in dateTimes)
            {
                blackoutDates.Add(date);
            }
            MessageBox.Show(blackoutDates.Count.ToString(), "Blackoutdates");
        }

        private void Accepting()
        {
            DateTime selectedDate = (DateTime)SelectedDate;

            IsDateErrorVisible = false;
            IsTimeErrorVisible = false;

            if (SelectedDate == null)
            {
                IsDateErrorVisible = true;
                return;
            }

            if (string.IsNullOrEmpty(Time))
            {
                IsTimeErrorVisible = true;
                return;
            }

            string timeString = Time.Trim();
            string[] timeParts = timeString.Split(':');
            
            if (timeParts.Length == 2 && int.TryParse(timeParts[0], out int hour) && int.TryParse(timeParts[1], out int minute))
            {
                string amPm = AmPm?.ToUpper() == "PM" ? "PM" : "AM";

                if (hour == 12)
                {
                    hour = amPm == "AM" ? 0 : 12;
                }
                else if (amPm == "PM")
                {
                    hour += 12;
                }
                DateTime selectedDateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, hour, minute, 0);

                _tourRequestService.AcceptRequest(tourRequest, GuideId, selectedDateTime);
                Successfull = true;
                MessageBox.Show("Tour Request successfully accepted.", "System Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                IsTimeErrorVisible = true;
            }
        }

        private void GetUnavailableDates()
        {
            
        }
    }
}
