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
using BookingApp.Observer;
using System.ComponentModel;
using System.Windows.Media.Animation;

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

            if(ViewModel.ReservationCalendar.DisplayDateEnd == DateTime.MaxValue)
            {
                txtAllDates.Visibility = Visibility.Visible;
            }
            else
            {
                txtAllDates.Visibility = Visibility.Collapsed;
            }
            if (PeopleNumberSection.IsEnabled == false)
            {
                UpdateHintContent();
               
            }
            langTextbox.TextChanged += ContentChanged;
            guestNumberValidator.Visibility = Visibility.Hidden;


            Loaded += Page_Loaded;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }


        private void ContentChanged(object sender, EventArgs e)
        {
            UpdateHintContent();
           // ViewModel.UpdateHintContent();
        }

        public void UpdateHintContent()
        {
           
                if (langTextbox.Text == "English")
                {
                    HintLabel.Content = "Problem with choosing dates";
                    Hint.Text = "It is neccessary to choose the same number of days you entered on the previous page.";
                    Hint.Visibility = Visibility.Hidden;
                }
                if (langTextbox.Text == "Srpski")
                {
                    HintLabel.Content = "Problem sa biranjem datuma";
                    Hint.Text = "Potrebno je izabrati onoliko dana koliko je uneto na prethodnoj strani.";
                    Hint.Visibility = Visibility.Hidden;
                }


            

        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

           ViewModel.Calendar_SelectedDatesChanged(sender, e);
        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Hint.Visibility = Visibility.Visible;
            var showHint = (Storyboard)FindResource("ShowTextBlock");
            showHint.Begin(Hint);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

            var hideHint = (Storyboard)FindResource("HideTextBlock");
            hideHint.Completed += (s, a) => Hint.Visibility = Visibility.Hidden;
            hideHint.Begin(Hint);

        }


    }
}
