using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.GuideWindows
{
    public partial class MostVisitedTourWindow: Window
    {

        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private User Guide {  get; set; }
        private bool FirstTime {  get; set; }
        public MostVisitedTourWindow(User guide)
        {
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
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
            Tour mostVisitedTourAllTime = _tourRepository.GetMostPopularTourForGuide(Guide.Id);

            if (FirstTime)
            {
                FirstTime = false;
            }
            else if (mostVisitedTourAllTime != null)
            {
                tourNameLabel.Content = mostVisitedTourAllTime.Name;
                int participantCount = _tourReservationRepository.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
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
            Tour mostVisitedTourPreviousYears = _tourRepository.GetMostPopularTourForGuideInYear(Guide.Id, year);

            if (mostVisitedTourPreviousYears != null)
            {
                tourNameLabel.Content = mostVisitedTourPreviousYears.Name;
                int participantCount = _tourReservationRepository.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
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
