using BookingApp.Model;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using BookingApp.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.ViewModel
{
    public class ReservationCalendarViewModel
    {

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationReservation Reservation { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public Frame Frame { get; set; }
        public int DayNumber { get; set; }

        public int GuestNumber { get; set; }

        public Calendar ReservationCalendar { get; set; }

        public CalendarPage Page { get; set; }
        public ReservationCalendarViewModel(AccommodationViewModel selectedAccommodation, int dayNumber, User user, DateTime start, DateTime end, Frame frame, CalendarPage page) {
            
            this.SelectedAccommodation = selectedAccommodation;
            this.User = user;
            this.StartDate = start;
            this.EndDate = end;
            this.DayNumber = dayNumber;
            this.Frame = frame;
            ReservationCalendar = page.ReservationCalendar;
            Page = page;
            page.reserveButton.IsEnabled = false;
            page.PeopleNumberSection.IsEnabled = false;
            Reservation = new AccommodationReservation();

            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();

            ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);

        }

        private void ConfigureCalendar(AccommodationViewModel selectedAccommodation, DateTime start, DateTime end, int dayNumber)
        {
            CalendarDateRange chosenDateRange = new CalendarDateRange(start, end);
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            ReservationCalendar.DisplayDateStart = start;
            ReservationCalendar.DisplayDateEnd = end;



            BlackOutUnavailableDates(selectedAccommodation.UnavailableDates, dayNumber, chosenDateRange);


            if (IsDateRangeAvailable(ReservationCalendar) == false)
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
                if (unavailableDateRange.Start >= chosenDateRange.Start && unavailableDateRange.End <= chosenDateRange.End)
                {

                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);




                }


            }
            if (unavailableDateRanges.Count > 0)
                CheckDaysBetween(unavailableDateRanges, dayNumber, chosenDateRange);



        }

        private void ShowReccommendedDates(AccommodationViewModel selectedAccommodation, int dayNumber)
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



            }

            if (unavailableDateRanges.Count > 0)
                CheckDaysBetween(unavailableDateRanges, dayNumber, newDateRange);




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
            foreach (var blackoutDateRange in calendar.BlackoutDates)
            {
                if (date >= blackoutDateRange.Start && date <= blackoutDateRange.End)
                    return false;
            }

            return true;
        }

        private void CheckDaysBetween(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            List<CalendarDateRange> sortedDateRanges = unavailableDateRanges.OrderBy(range => range.End).ToList();

            for (int i = 0; i < sortedDateRanges.Count - 1; i++)
            {
                CalendarDateRange betweenRange = new CalendarDateRange(sortedDateRanges[i].End, sortedDateRanges[i + 1].Start.AddDays(-1));
                int betweenDaysCount = (betweenRange.End - betweenRange.Start).Days;

                if (betweenDaysCount < dayNumber)
                {
                    ReservationCalendar.BlackoutDates.Add(betweenRange);


                }



            }

            CheckStartRange(sortedDateRanges, dayNumber, chosenDateRange);
            CheckEndRange(sortedDateRanges, dayNumber, chosenDateRange);







        }

        private void CheckStartRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int startToUnavailableCount = (unavailableDateRanges[0].Start.AddDays(-1) - chosenDateRange.Start).Days;
            // MessageBox.Show(startToUnavailableCount.ToString() + " " + unavailableDateRanges[0].Start.AddDays(-1).ToString());
            if (startToUnavailableCount < dayNumber)
            {

                CalendarDateRange startUnavailableRange = new CalendarDateRange(chosenDateRange.Start, unavailableDateRanges[0].Start);
                ReservationCalendar.BlackoutDates.Add(startUnavailableRange);
            }
        }

        private void CheckEndRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int unavailableToEndCount = (chosenDateRange.End.AddDays(-1) - unavailableDateRanges[unavailableDateRanges.Count - 1].End).Days;
            if (unavailableToEndCount < dayNumber)
            {
                CalendarDateRange unavailableEndRange = new CalendarDateRange(unavailableDateRanges[unavailableDateRanges.Count - 1].End, chosenDateRange.End);
                ReservationCalendar.BlackoutDates.Add(unavailableEndRange);
            }
        }

        public void SelectDate_Click(object sender, RoutedEventArgs e)
        {

            Page.PeopleNumberSection.IsEnabled = true;
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);




        }

        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                Page.reserveButton.IsEnabled = false;
                /* warningLabel.Visibility = Visibility.Visible;
                 dayNumberLabel.Visibility = Visibility.Visible;
                 continueLabel.Visibility = Visibility.Hidden;*/

            }
            else
            {
                Page.reserveButton.IsEnabled = true;
                /*  warningLabel.Visibility = Visibility.Hidden;
                  dayNumberLabel.Visibility = Visibility.Hidden;
                  continueLabel.Visibility = Visibility.Visible;*/

            }

            Mouse.Capture(null);
        }

        public void Reserve_Click(object sender, RoutedEventArgs e)
        {
            GuestNumber = Convert.ToInt32(Page.txtGuestNumber.Text);
            if (GuestNumber > SelectedAccommodation.MaxGuestNumber)
            {
                // finishReservation.IsEnabled = false;

            }
            else
            {
                Page.finishReservation.IsEnabled = true;
                Reservation.GuestId = User.Id;
                Reservation.AccommodationId = SelectedAccommodation.Id;
                Reservation.StartDate = SelectedDateRange.Start;
                Reservation.EndDate = SelectedDateRange.End;
                Reservation.Name = SelectedAccommodation.Name;
                Reservation.City = SelectedAccommodation.City;
                Reservation.Country = SelectedAccommodation.Country;
                Reservation.NumberOfPeople = GuestNumber;

                SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
                AccommodationService.Update(SelectedAccommodation.ToAccommodation());

                AccommodationReservationService.Add(Reservation);

                

                Frame.Content = new ReservationSuccessfulPage(SelectedAccommodation, SelectedDateRange, GuestNumber, User, Frame);




            }

        }
    }
}
