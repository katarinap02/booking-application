using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
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
using BookingApp.View.ViewModel.HostGuestViewModel;

namespace BookingApp.View.HostPages.RatePages
{
    /// <summary>
    /// Interaction logic for RateDisplayPage.xaml
    /// </summary>
    public partial class RateDisplayPage : Page, IObserver
    {
        public ObservableCollection<AccommodationRateViewModel> Accommodations { get; set; }

        public AccommodationReservationService accommodationService { get; set; }

        public UserService userService { get; set; }

        public AccommodationRateService accommodationRateService { get; set; }

        public GuestRateService guestRateService { get; set; }
        public RateDisplayPage()
        {
            InitializeComponent();
            Accommodations = new ObservableCollection<AccommodationRateViewModel>();
            accommodationService = new AccommodationReservationService();
            userService = new UserService();
            accommodationRateService = new AccommodationRateService();
            guestRateService = new GuestRateService();
            DataContext = this;

            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationRate accommodationRate in accommodationRateService.GetAll())
            {
                AccommodationReservation accommodationReservation = accommodationService.GetById(accommodationRate.ReservationId);
                User user = userService.GetById(accommodationRate.GuestId);


                foreach (GuestRate guestRate in guestRateService.GetAll())
                {
                    if (guestRate.ReservationId == accommodationRate.ReservationId)
                    {
                        Accommodations.Add(new AccommodationRateViewModel(accommodationRate, accommodationReservation, user));
                        break;
                    }
                }

               

            }
        }

        private void RateGuest_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
