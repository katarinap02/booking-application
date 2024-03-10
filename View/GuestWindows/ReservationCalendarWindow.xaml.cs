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

namespace BookingApp.View
{
    public partial class ReservationCalendarWindow : Window
    {
        public AccommodationDTO SelectedAccommodation { get; set; }

        public ReservationCalendarWindow(AccommodationDTO SelectedAccommodation, int dayNumber)
        {
            InitializeComponent();
            this.SelectedAccommodation = SelectedAccommodation;
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            CalendarDateRange datesBeforeToday = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            ReservationCalendar.BlackoutDates.Add(datesBeforeToday);
        }
    }
}
