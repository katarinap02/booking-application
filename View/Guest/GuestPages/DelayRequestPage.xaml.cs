using BookingApp.Model;
using BookingApp.Services;
using System;
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
using BookingApp.View.ViewModel.HostGuestViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for DelayRequestPage.xaml
    /// </summary>
    public partial class DelayRequestPage : Page
    {
        public User User {  get; set; }
        public Frame Frame { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }

      

        public DelayRequestPageViewModel ViewModel { get; set; }
        public DelayRequestPage(AccommodationReservationViewModel selectedReservation, User user, Frame frame)
        {
            InitializeComponent();
           
            this.User = user;
            this.Frame = frame;
            SelectedReservation = selectedReservation;
            reserveButton.IsEnabled = false;
            ViewModel = new DelayRequestPageViewModel(User, Frame, SelectedReservation, this);
            DataContext = ViewModel;

           
          
        }

      

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Calendar_SelectedDatesChanged(sender, e);
            
        }

        private void SelectDate_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectDate_Click(sender, e);

        }
    }
}
