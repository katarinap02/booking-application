using System;
using System.Collections.Generic;
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
                if (unavailableDateRange.Start >= chosenDateRange.Start || unavailableDateRange.End <= chosenDateRange.End)
                {
                    ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                    unavailableDateRanges.Add(unavailableDateRange);

                }


            }
            if (unavailableDateRanges.Count > 0)
                BetweenDateMarker.CheckDaysBetween(unavailableDateRanges, dayNumber, chosenDateRange);



        }




    }
}
