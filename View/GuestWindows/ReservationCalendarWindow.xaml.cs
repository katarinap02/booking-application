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
using System.Collections;
using System.Globalization;
using Calendar = System.Windows.Controls.Calendar;

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
            CalendarDateRange chosenDateRange = new CalendarDateRange(start, end);
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            ReservationCalendar.DisplayDateStart = start;
            ReservationCalendar.DisplayDateEnd = end;


            BlackOutUnavailableDates(selectedAccommodation.UnavailableDates, dayNumber, chosenDateRange);
           

            if(IsDateRangeAvailable(ReservationCalendar) == false)
            {
                MessageBox.Show("All dates in the chosen date range are unavailable. All available dates will be shown now.");
                ShowReccommendedDates(SelectedAccommodation, DayNumber);
            }





        }

        private void BlackOutUnavailableDates(List<CalendarDateRange> unavailableDates, int dayNumber, CalendarDateRange chosenDateRange)
        {

            List<CalendarDateRange> unavailableDateRanges = new List<CalendarDateRange>();
            foreach (CalendarDateRange unavailableDateRange in unavailableDates)
            {
                if (unavailableDateRange.Start >= chosenDateRange.Start || unavailableDateRange.End <= chosenDateRange.End)
                {

                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);
                    CheckDaysBetween(unavailableDateRanges, dayNumber, chosenDateRange);



                }


            }
        }

        private void ShowReccommendedDates(AccommodationDTO selectedAccommodation, int dayNumber)
        {
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            ReservationCalendar.DisplayDateStart = DateTime.Today;
            ReservationCalendar.DisplayDateEnd = DateTime.MaxValue;
            ReservationCalendar.BlackoutDates.Clear();
            CalendarDateRange newDateRange = new CalendarDateRange(DateTime.Today, DateTime.MaxValue);

            List<CalendarDateRange> unavailableDateRanges = new List<CalendarDateRange>();
            foreach (CalendarDateRange unavailableDateRange in selectedAccommodation.UnavailableDates)
            {
                
                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);
                    CheckDaysBetween(unavailableDateRanges, dayNumber, newDateRange);
                   

            }
        }

        private bool IsDateRangeAvailable(Calendar calendar)
        {

            DateTime startDate = calendar.DisplayDateStart ?? DateTime.MinValue; 
            DateTime endDate = calendar.DisplayDateEnd ?? DateTime.MaxValue; 
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (IsDateSelectable(calendar, date) == true)
                    return true;

            }
               
            return false;
        }

        private bool IsDateSelectable(Calendar calendar, DateTime date)
        {
            foreach(var blackoutDateRange in calendar.BlackoutDates)
            {
                if (date >= blackoutDateRange.Start && date <= blackoutDateRange.End)
                    return false;
            }

            return true;
        }

        private void CheckDaysBetween(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
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

            CheckStartRange(unavailableDateRanges, dayNumber, chosenDateRange);
            CheckEndRange(unavailableDateRanges, dayNumber, chosenDateRange);

            


           


        }

        private void CheckStartRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int startToUnavailableCount = (unavailableDateRanges[0].Start.AddDays(-1) - chosenDateRange.Start).Days;
            if (startToUnavailableCount < dayNumber)
            {
                CalendarDateRange startUnavailableRange = new CalendarDateRange(chosenDateRange.Start, unavailableDateRanges[0].Start.AddDays(-1));
                ReservationCalendar.BlackoutDates.Add(startUnavailableRange);
            }
        }

        private void CheckEndRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int unavailableToEndCount = (chosenDateRange.End - unavailableDateRanges[unavailableDateRanges.Count - 1].End.AddDays(1)).Days;
            if (unavailableToEndCount < dayNumber)
            {
                CalendarDateRange unavailableEndRange = new CalendarDateRange(unavailableDateRanges[unavailableDateRanges.Count - 1].End.AddDays(1), chosenDateRange.End);
                ReservationCalendar.BlackoutDates.Add(unavailableEndRange);
            }
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {

          
           /* if (ReservationCalendar.SelectedDates.Count != DayNumber)
                MessageBox.Show("Please select " + DayNumber + " days.");
            else
            {*/ // Implementirana provera na drugi nacin

                SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
                CalendarDateRange selectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);

                ReservationInfoWindow reservationInfo = new ReservationInfoWindow(AccommodationRepository, SelectedAccommodation, User, selectedDateRange);
                reservationInfo.ShowDialog();

                this.Close();


           // }



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
