using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Observer;
using BookingApp.Model;
using System.Security.Cryptography;

namespace BookingApp.View
{
    public partial class ReservationCalendarWindow : Window
    {
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }   

        public int DayNumber { get; set; }
        

        public ReservationCalendarWindow(AccommodationRepository accommodationRepository, AccommodationDTO selectedAccommodation, int dayNumber, User user, DateTime start, DateTime end)
        {
            InitializeComponent();
            this.SelectedAccommodation = selectedAccommodation;
            this.AccommodationRepository = accommodationRepository;
            this.User = user;
            this.StartDate = start;
            this.EndDate = end;
            this.DayNumber = dayNumber;
            DataContext = this;
            reserveButton.IsEnabled = false;
            continueLabel.Visibility = Visibility.Hidden;
            ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
            
           

        }

        private void ConfigureCalendar(AccommodationDTO selectedAccommodation, DateTime start, DateTime end, int dayNumber)
        {
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            ReservationCalendar.DisplayDateStart = start;
            ReservationCalendar.DisplayDateEnd = end;
            
            CalendarDateRange chosenDateRange = new CalendarDateRange(start, end);


           // CalendarDateRange pastDates = new CalendarDateRange(DateTime.MinValue, start.AddDays(-1));
           // ReservationCalendar.BlackoutDates.Add(pastDates);

           // MessageBox.Show(selectedAccommodation.UnavailableDates.Count.ToString());
           List<CalendarDateRange> unavailableDateRanges = new List<CalendarDateRange>();
            foreach(CalendarDateRange unavailableDateRange in selectedAccommodation.UnavailableDates)
            {
                if(unavailableDateRange.Start >= start || unavailableDateRange.End <= end)
                {
                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);
                    CheckDaysBetween(unavailableDateRanges, dayNumber);

                }
                    
                //MessageBox.Show("uslo");

            }

         

           
                
            
        }

        private void CheckDaysBetween(List<CalendarDateRange> unavailableDateRanges, int dayNumber)
        {
            for(int i = 0; i <  unavailableDateRanges.Count-1; i++)
            {
                CalendarDateRange betweenRange = new CalendarDateRange(unavailableDateRanges[i].End.AddDays(1), unavailableDateRanges[i + 1].Start.AddDays(-1));
                int betweenDaysCount = (betweenRange.End - betweenRange.Start).Days;

                if(betweenDaysCount < dayNumber)
                {
                    ReservationCalendar.BlackoutDates.Add(betweenRange);


                }

            }
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {

          
            if (ReservationCalendar.SelectedDates.Count != DayNumber)
                MessageBox.Show("Please select " + DayNumber + " days.");
            else
            {
                SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
                CalendarDateRange selectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);

                //  MessageBox.Show(SelectedAccommodation.UnavailableDates.Count.ToString());

                ReservationInfoWindow reservationInfo = new ReservationInfoWindow(AccommodationRepository, SelectedAccommodation, User, selectedDateRange);
                reservationInfo.ShowDialog();


            }



        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                reserveButton.IsEnabled = false;
                warningLabel.Visibility = Visibility.Visible;
                dayNumberLabel.Visibility = Visibility.Visible;
                continueLabel.Visibility = Visibility.Hidden;

            }
            else
            {
                reserveButton.IsEnabled = true;
                warningLabel.Visibility = Visibility.Hidden;
                dayNumberLabel.Visibility = Visibility.Hidden;
                continueLabel.Visibility = Visibility.Visible;

            }
                
            Mouse.Capture(null);
        }


    }
}
