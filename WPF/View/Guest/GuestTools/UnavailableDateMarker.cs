using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest.GuestTools
{
    public class UnavailableDateMarker
    {
        public Calendar ReservationCalendar { get; set; }

        public BetweenDateMarker BetweenDateMarker { get; set; }
        public UnavailableDateMarker(Calendar reservationCalendar)
        {
            ReservationCalendar = reservationCalendar;
            BetweenDateMarker = new BetweenDateMarker(ReservationCalendar);
        }

        public void BlackOutUnavailableDates(List<CalendarDateRange> unavailableDates, int dayNumber, CalendarDateRange chosenDateRange)
        {

            List<CalendarDateRange> unavailableDateRanges = new List<CalendarDateRange>();
            foreach (CalendarDateRange unavailableDateRange in unavailableDates)
            {

                if (IsInBetweenDates(unavailableDateRange, chosenDateRange) || IsEndInUnavailableDates(unavailableDateRange, chosenDateRange) || IsStartInUnavailableDates(unavailableDateRange, chosenDateRange))

                {
                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);

                }


            }
            if (unavailableDateRanges.Count > 0)
                BetweenDateMarker.CheckDaysBetween(unavailableDateRanges, dayNumber, chosenDateRange);



        }

        private bool IsStartInUnavailableDates(CalendarDateRange unavailableDateRange, CalendarDateRange chosenDateRange)
        {
            if (chosenDateRange.Start >= unavailableDateRange.Start && chosenDateRange.Start <= unavailableDateRange.End)
                return true;
            else
                return false;
        }

        private bool IsEndInUnavailableDates(CalendarDateRange unavailableDateRange, CalendarDateRange chosenDateRange)
        {
            if (chosenDateRange.End >= unavailableDateRange.Start && chosenDateRange.End <= unavailableDateRange.End)
                return true;
            else
                return false;
        }

        private bool IsInBetweenDates(CalendarDateRange unavailableDateRange, CalendarDateRange chosenDateRange)
        {
            if(unavailableDateRange.Start >= chosenDateRange.Start && unavailableDateRange.End <= chosenDateRange.End)
                    return true;
            else
                return false;
            

        }
    }
}
