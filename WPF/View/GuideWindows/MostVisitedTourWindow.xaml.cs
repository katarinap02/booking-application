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

namespace BookingApp.View.GuideWindows
{
    public partial class MostVisitedTourWindow: Window
    {

        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private User Guide {  get; set; }
        private bool FirstTime {  get; set; }
        public MostVisitedTourWindow(User guide)
        {
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            Guide = guide;
            FirstTime = true;
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
                tourNameLabel.Content = mostVisitedTourAllTime.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
                participantsLabel.Content = $"Participants: {participantCount}";
            }
            else
            {
                tourNameLabel.Content = "No data available";
                participantsLabel.Content = "";
            }
        }

        private void ShowMostVisitedTourPreviousYears(int year)
        {
            Tour mostVisitedTourPreviousYears = _tourService.GetMostPopularTourForGuideInYear(Guide.Id, year);

            if (mostVisitedTourPreviousYears != null)
            {
                tourNameLabel.Content = mostVisitedTourPreviousYears.Name;
                int participantCount = _tourReservationService.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
                participantsLabel.Content = $"Participants: {participantCount}";
            }
            else
            {
                tourNameLabel.Content = "No data available";
                participantsLabel.Content = "";
            }
        }

    }
}
