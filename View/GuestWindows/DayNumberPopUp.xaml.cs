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
    public partial class DayNumberPopUp : Window
    {
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public User User { get; set; }  
        public int DayNumber {  get; set; }
        public DayNumberPopUp(AccommodationRepository accommodationRepository, AccommodationDTO SelectedAccommodation, User user)
        {
            InitializeComponent();
            this.SelectedAccommodation = SelectedAccommodation;
            this.AccommodationRepository = accommodationRepository;
            this.User = user;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            DayNumber = Convert.ToInt32(txtDayNumber.Text);
            DateTime start = Convert.ToDateTime(txtStartDate.Text);
            DateTime end = Convert.ToDateTime(txtEndDate.Text);

            bool dateIsValid = ValidateDateInputs(start, end);
            bool dayNumberIsValid = ValidateDayNumber(DayNumber);

            if(dateIsValid && dayNumberIsValid) {
                ReservationCalendarWindow calendarWindow = new ReservationCalendarWindow(AccommodationRepository, SelectedAccommodation, DayNumber, User, start, end);
                calendarWindow.ShowDialog();
                this.Close();



            }

            
            
        }

        private bool ValidateDayNumber(int dayNumber)
        {
            if (DayNumber < SelectedAccommodation.MinReservationDays)
            {
                MessageBox.Show("Minimal number of reservation days is: " + SelectedAccommodation.MinReservationDays.ToString());
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if(start >= end)
            {
                MessageBox.Show("Dates are not valid");
                return false;
            }
            else
            {
                
                return true;
            }


        }
    }
}
