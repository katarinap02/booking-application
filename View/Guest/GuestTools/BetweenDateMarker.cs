using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.View.Guest.GuestTools
{
    public class BetweenDateMarker
    {
        public Calendar ReservationCalendar { get; set; }

        public StartEndDateMarker StartEndDateMarker { get; set; }
        public BetweenDateMarker(Calendar reservationCalendar)
        {
            ReservationCalendar = reservationCalendar;
            StartEndDateMarker = new StartEndDateMarker(ReservationCalendar);
        }
        public void CheckDaysBetween(List<CalendarDateRange> unavailableDateRanges, int dayNumber, CalendarDateRange chosenDateRange)
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

            StartEndDateMarker.CheckStartRange(sortedDateRanges, dayNumber, chosenDateRange);
            StartEndDateMarker.CheckEndRange(sortedDateRanges, dayNumber, chosenDateRange);

        }
    }
}
