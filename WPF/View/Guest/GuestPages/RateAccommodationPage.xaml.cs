using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Application.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RateAccommodationPage.xaml
    /// </summary>
    public partial class RateAccommodationPage : Page
    {

      
        public User User { get; set; }

        public AccommodationReservationViewModel SelectedReservation {  get; set; }
        
        
        public Frame Frame {  get; set; }   

        public ReservationsToRateViewModel ViewModel { get; set; }

        public RateAccommodationPage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;

            ViewModel = new ReservationsToRateViewModel(User, Frame, SelectedReservation);
           
            DataContext = ViewModel;
            Hint.Visibility = Visibility.Hidden;
            ViewModel.Update();

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
