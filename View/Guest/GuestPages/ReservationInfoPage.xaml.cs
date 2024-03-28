using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ReservationInfoPage.xaml
    /// </summary>
    public partial class ReservationInfoPage : Page
    {
        public AccommodationDTO SelectedAccommodation { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public User User { get; set; }
        public int DayNumber { get; set; }
        public Frame Frame { get; set; }

       
        public ReservationInfoPage(AccommodationRepository accommodationRepository, AccommodationDTO SelectedAccommodation, User user, Frame frame)
        {
            InitializeComponent();
            this.SelectedAccommodation = SelectedAccommodation;
            this.AccommodationRepository = accommodationRepository;
            this.User = user;
            this.Frame = frame;
            bool dateIsValid;
            bool dayNumberIsValid;
            DataContext = this;
          
           

           
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            

           
            
            DayNumber = Convert.ToInt32(txtDayNumber.Text);
            DateTime start = Convert.ToDateTime(txtStartDate.Text);
            DateTime end = Convert.ToDateTime(txtEndDate.Text);

           

           

            Frame.Content = new CalendarPage(AccommodationRepository, SelectedAccommodation, DayNumber, User, start, end, Frame);


        }

        private bool ValidateDayNumber(int dayNumber)
        {
            if (DayNumber < SelectedAccommodation.MinReservationDays)
            {
                
                return false;
            }
            else
            {
                
                return true;
            }
        }

        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if (start >= end)
            {
               
                return false;
            }
            else
            {
               
                return true;
            }


        }
    }
}
