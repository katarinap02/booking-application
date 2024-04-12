using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections;
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
using BookingApp.ViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }
       
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationCalendarViewModel ViewModel { get; set; }

        public AccommodationReservation Reservation { get; set; }
      

        public Frame Frame { get; set; }
        public int DayNumber { get; set; }

     
        public CalendarPage(AccommodationViewModel selectedAccommodation, int dayNumber, User user, DateTime start, DateTime end, Frame frame)
        {
            InitializeComponent();
            this.SelectedAccommodation = selectedAccommodation;
            
            this.User = user;
            this.StartDate = start;
            this.EndDate = end;
            this.DayNumber = dayNumber;
            this.Frame = frame;

            ViewModel = new ReservationCalendarViewModel(SelectedAccommodation, DayNumber, User, StartDate, EndDate, Frame, this);
            DataContext = ViewModel;

            // finishReservation.IsEnabled = false;
          

            // continueLabel.Visibility = Visibility.Hidden;
             
        }

       

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.SelectDate_Click(sender, e);




        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

           ViewModel.Calendar_SelectedDatesChanged(sender, e);
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
           ViewModel.Reserve_Click(sender, e);

        }


    }
}
