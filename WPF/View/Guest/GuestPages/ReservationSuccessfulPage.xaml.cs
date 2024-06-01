using BookingApp.Repository;
using BookingApp.Application.Services;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.Animation;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for AccommodationSuccessfulPage.xaml
    /// </summary>
    public partial class ReservationSuccessfulPage : Page
    {
      
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public ReservationSuccessfulViewModel ViewModel { get; set; }
       
        public AccommodationReservationViewModel Reservation { get; set; }
        public CalendarDateRange SelectedDateRange { get; set; }
        public User User { get; set; }

        public Frame Frame { get; set; }

        public int GuestNumber { get; set; }
        public ReservationSuccessfulPage(AccommodationReservationViewModel reservation, AccommodationViewModel selectedAccommodation, CalendarDateRange selectedDateRange, int guestNumber, User user, Frame frame)
        {
            InitializeComponent();

            
            this.SelectedAccommodation = selectedAccommodation;
            this.SelectedDateRange = selectedDateRange;
            this.GuestNumber = guestNumber;
            this.User = user;
            this.Frame = frame;
            Reservation = reservation;

            ViewModel = new ReservationSuccessfulViewModel(SelectedAccommodation, User, Frame, Reservation);
             DataContext = ViewModel;



            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }



    }
}
