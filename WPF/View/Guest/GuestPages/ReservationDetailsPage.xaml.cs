using BookingApp.Domain.Model.Features;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
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

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for ReservationDetailsPage.xaml
    /// </summary>
    public partial class ReservationDetailsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public AccommodationReservationViewModel Reservation { get; set; }
        public GuestICommand BackToProfileCommand { get; set; }
        public ReservationDetailsPage(User user, Frame frame, AccommodationReservationViewModel reservation)
        {
            InitializeComponent();
            Reservation = reservation;
            User = user;
            Frame = frame;
            BackToProfileCommand = new GuestICommand(OnBackToProfile);
            DataContext = this;
        }

        private void OnBackToProfile()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }
    }
}
