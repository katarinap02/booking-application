using BookingApp.Repository;
using BookingApp.Application.Services;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;

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

            if(PeopleNumberSection.IsEnabled == false)
            {
                HintLabel.Content = "Problem with choosing dates";
                Hint.Text = "It is neccessary to choose the same number of days you entered on the previous page.";
                Hint.Visibility = Visibility.Hidden;


            }

            guestNumberValidator.Visibility = Visibility.Hidden;
           

        }

        

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

           ViewModel.Calendar_SelectedDatesChanged(sender, e);
        }

     

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Hint.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Hint.Visibility = Visibility.Hidden;
        }
    }
}
