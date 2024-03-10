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
            ConfigureCalendar(SelectedAccommodation, dayNumber);
           

        }

        private void ConfigureCalendar(AccommodationDTO selectedAccommodation, int dayNumber)
        {
            ReservationCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            CalendarDateRange datesBeforeToday = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1));
            ReservationCalendar.BlackoutDates.Add(datesBeforeToday);

            MessageBox.Show(selectedAccommodation.UnavailableDates.Count.ToString());
            foreach(CalendarDateRange unavailableDateRange in selectedAccommodation.UnavailableDates)
            {
                ReservationCalendar.BlackoutDates.Add(unavailableDateRange);
                //MessageBox.Show("uslo");

            }
                
            
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            CalendarDateRange selectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count-1]);
            
            SelectedAccommodation.UnavailableDates.Add(selectedDateRange);
            MessageBox.Show(SelectedAccommodation.UnavailableDates.Count.ToString());


        }
    }
}
