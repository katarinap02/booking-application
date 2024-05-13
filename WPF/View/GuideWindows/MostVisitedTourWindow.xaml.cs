using BookingApp.Domain.Model.Features;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using System.ComponentModel;
using System.Windows.Data;

namespace BookingApp.View.GuideWindows
{
    public partial class MostVisitedTourWindow: Window
    {

        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private User Guide {  get; set; }
        private bool FirstTime {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _tourName;
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        private string _participantCount;
        public string ParticipantCount
        {
            get { return _participantCount; }
            set
            {
                _participantCount = value;
                OnPropertyChanged(nameof(ParticipantCount));
            }
        }


        public MostVisitedTourWindow(User guide)
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            Guide = guide;
            FirstTime = true;
            TourName = "Tour name";
            ParticipantCount = "Number of participants";
            InitializeComponent();
            DataContext = this;
        }

        private void TimePeriodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedPeriod = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();

            if (selectedPeriod == "All Time")
            {
                ShowMostVisitedTourAllTime();
            }
            else
            {
                int selectedYear = int.Parse(((ComboBoxItem)comboBox.SelectedItem).Content.ToString());
                ShowMostVisitedTourPreviousYears(selectedYear);
            }
        }

        private void ShowMostVisitedTourAllTime()
        {
            Tour mostVisitedTourAllTime = _tourService.GetMostPopularTourForGuide(Guide.Id);

            if (FirstTime)
            {
                FirstTime = false;
            }
            else if (mostVisitedTourAllTime != null)
            {
                TourName = mostVisitedTourAllTime.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
                ParticipantCount = $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantCount = "";
            }

            RaisePropertyChanged(nameof(TourName));
            RaisePropertyChanged(nameof(ParticipantCount));
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                RaisePropertyChanged(nameof(TourName));
                RaisePropertyChanged(nameof(ParticipantCount));
            });


        }

        private void ShowMostVisitedTourPreviousYears(int year)
        {
            Tour mostVisitedTourPreviousYears = _tourService.GetMostPopularTourForGuideInYear(Guide.Id, year);

            if (mostVisitedTourPreviousYears != null)
            {
                TourName = mostVisitedTourPreviousYears.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
                ParticipantCount = $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantCount = "";
            }

            RaisePropertyChanged(nameof(TourName));
            RaisePropertyChanged(nameof(ParticipantCount));
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                RaisePropertyChanged(nameof(TourName));
                RaisePropertyChanged(nameof(ParticipantCount));
            });


        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateBindings()
        {
            // Call UpdateSource on each binding
            foreach (var bindingExpression in BindingOperations.GetSourceUpdatingBindings(this))
            {
                bindingExpression.UpdateSource();
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
