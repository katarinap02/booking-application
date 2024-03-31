using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationReservation Reservation { get; set; }
        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }

        public Frame Frame { get; set; }
        public int DayNumber { get; set; }

        public int GuestNumber { get; set; }
        public CalendarPage(AccommodationRepository accommodationRepository, AccommodationDTO selectedAccommodation, int dayNumber, User user, DateTime start, DateTime end, Frame frame)
        {
            InitializeComponent();
            this.SelectedAccommodation = selectedAccommodation;
            this.AccommodationRepository = accommodationRepository;
            this.User = user;
            this.StartDate = start;
            this.EndDate = end;
            this.DayNumber = dayNumber;
            this.Frame = frame;

            DataContext = this;
            reserveButton.IsEnabled = false;
            PeopleNumberSection.IsEnabled = false;
            Reservation = new AccommodationReservation();
           // finishReservation.IsEnabled = false;
            AccommodationReservationRepository = new AccommodationReservationRepository();

            // continueLabel.Visibility = Visibility.Hidden;
             ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
        }

        private void ConfigureCalendar(AccommodationDTO selectedAccommodation, DateTime start, DateTime end, int dayNumber)
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

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {

            PeopleNumberSection.IsEnabled = true;
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);




        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                reserveButton.IsEnabled = false;
               /* warningLabel.Visibility = Visibility.Visible;
                dayNumberLabel.Visibility = Visibility.Visible;
                continueLabel.Visibility = Visibility.Hidden;*/

            }
            else
            {
                reserveButton.IsEnabled = true;
              /*  warningLabel.Visibility = Visibility.Hidden;
                dayNumberLabel.Visibility = Visibility.Hidden;
                continueLabel.Visibility = Visibility.Visible;*/

            }

            Mouse.Capture(null);
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            GuestNumber = Convert.ToInt32(txtGuestNumber.Text);
            if (GuestNumber > SelectedAccommodation.MaxGuestNumber)
            {
               // finishReservation.IsEnabled = false;
                
            }
            else
            {
                finishReservation.IsEnabled = true;
                Reservation.GuestId = User.Id;
                Reservation.AccommodationId = SelectedAccommodation.Id;
                Reservation.StartDate = SelectedDateRange.Start;
                Reservation.EndDate = SelectedDateRange.End;
                Reservation.Name = SelectedAccommodation.Name;
                Reservation.City = SelectedAccommodation.City;
                Reservation.Country = SelectedAccommodation.Country;

                SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
                AccommodationRepository.Update(SelectedAccommodation.ToAccommodation());

                AccommodationReservationRepository.Add(Reservation);

                Frame.Content = new ReservationSuccessfulPage(AccommodationRepository, SelectedAccommodation, SelectedDateRange, GuestNumber, User, Frame);




            }

        }


    }
}
