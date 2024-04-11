using BookingApp.Model;
using BookingApp.Services;
using System;
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
using BookingApp.View.ViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for DelayRequestPage.xaml
    /// </summary>
    public partial class DelayRequestPage : Page
    {
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationService AccommodationService { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }

       
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public DelayRequest DelayRequest { get; set; }
        public DelayRequestService DelayRequestService { get; set; }

        public int DayNumber {  get; set; }
        public AccommodationViewModel SelectedAccommodation {  get; set; }
        public Frame Frame { get; set; }
        public DelayRequestPage(AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, AccommodationReservationViewModel selectedReservation, User user, Frame frame)
        {
            InitializeComponent();
            this.AccommodationReservationService = accommodationReservationService;
            this.AccommodationService = accommodationService;
            this.SelectedReservation = selectedReservation;
            this.User = user;
            this.Frame = frame;
            DelayRequestService = new DelayRequestService();
            DelayRequest = new DelayRequest();
            reserveButton.IsEnabled = false;
            DataContext = this;

            StartDate = DateTime.Now;
            EndDate = DateTime.MaxValue;

            DayNumber = (SelectedReservation.EndDate - SelectedReservation.StartDate).Days + 1;
            SelectedAccommodation = new AccommodationViewModel(AccommodationService.GetById(SelectedReservation.AccommodationId));
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

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                reserveButton.IsEnabled = false;
               

            }
            else
            {
                reserveButton.IsEnabled = true;
             

            }

            Mouse.Capture(null);
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);

            DelayRequest.GuestId = SelectedReservation.GuestId;
            Accommodation tmpAccommodation = AccommodationService.GetById(SelectedReservation.AccommodationId);
            DelayRequest.HostId = tmpAccommodation.HostId;
            DelayRequest.ReservationId = SelectedReservation.Id;
            DelayRequest.StartDate = SelectedDateRange.Start;
            DelayRequest.EndDate = SelectedDateRange.End;

            DelayRequestService.Add(DelayRequest);
            MessageBox.Show("radi");


        }
    }
}
