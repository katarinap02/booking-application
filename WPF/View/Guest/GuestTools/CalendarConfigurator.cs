using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest.GuestTools
{
    public class CalendarConfigurator
    {
        public Calendar ReservationCalendar { get; set; }

        public UnavailableDateMarker UnavailableDateMarker { get; set; }
        public CalendarConfigurator(Calendar reservationCalendar)
        {

            ReservationCalendar = reservationCalendar;
            UnavailableDateMarker = new UnavailableDateMarker(ReservationCalendar);

        }

        public void ConfigureCalendar(AccommodationViewModel selectedAccommodation, DateTime start, DateTime end, int dayNumber)
        {
            CalendarDateRange chosenDateRange = new CalendarDateRange(start, end);
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            ReservationCalendar.DisplayDateStart = start;
            ReservationCalendar.DisplayDateEnd = end;



            UnavailableDateMarker.BlackOutUnavailableDates(selectedAccommodation.UnavailableDates, dayNumber, chosenDateRange);


            if (IsDateRangeAvailable(ReservationCalendar) == false)
            {
                MessageBox.Show("All dates in the chosen date range are unavailable. All available dates will be shown now.");
                ShowReccommendedDates(selectedAccommodation, dayNumber);
            }


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
                UnavailableDateMarker.BetweenDateMarker.CheckDaysBetween(unavailableDateRanges, dayNumber, newDateRange);


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


    }
}
