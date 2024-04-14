using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.View.Guest.GuestTools
{
    public class StartEndDateMarker
    {
        public Calendar ReservationCalendar { get; set; }

        public StartEndDateMarker(Calendar reservationCalendar) {
            
            ReservationCalendar = reservationCalendar;
                
        }
        public void CheckStartRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int startToUnavailableCount = (unavailableDateRanges[0].Start.AddDays(-1) - chosenDateRange.Start).Days;
            if (startToUnavailableCount < dayNumber)
            {

                CalendarDateRange startUnavailableRange = new CalendarDateRange(chosenDateRange.Start, unavailableDateRanges[0].Start);
                ReservationCalendar.BlackoutDates.Add(startUnavailableRange);
            }
        }

        public void CheckEndRange(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
        {
            int unavailableToEndCount = (chosenDateRange.End.AddDays(-1) - unavailableDateRanges[unavailableDateRanges.Count - 1].End).Days;
            if (unavailableToEndCount < dayNumber)
            {
                CalendarDateRange unavailableEndRange = new CalendarDateRange(unavailableDateRanges[unavailableDateRanges.Count - 1].End, chosenDateRange.End);
                ReservationCalendar.BlackoutDates.Add(unavailableEndRange);
            }
        }
    }
}
