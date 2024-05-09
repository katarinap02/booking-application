using BookingApp.Repository;
using BookingApp.Application.Services;
using Microsoft.Win32;
using System;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RateAccommodationForm.xaml
    /// </summary>
    public partial class RateAccommodationForm : Page
    {
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        
        public User User { get; set; }
      

        public Frame Frame { get; set; }

        public RateFormViewModel ViewModel { get; set; }
        public RateAccommodationForm(User user, AccommodationReservationViewModel selectedReservation, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.SelectedReservation = selectedReservation;
          
            this.Frame = frame;
            ViewModel = new RateFormViewModel(User, Frame, SelectedReservation, this);
            Hint.Visibility = Visibility.Hidden;
            DataContext = ViewModel;

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
