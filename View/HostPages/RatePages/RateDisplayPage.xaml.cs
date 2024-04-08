using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
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

namespace BookingApp.View.HostPages.RatePages
{
    /// <summary>
    /// Interaction logic for RateDisplayPage.xaml
    /// </summary>
    public partial class RateDisplayPage : Page, IObserver
    {
        public ObservableCollection<AccommodationRateDTO> Accommodations { get; set; }

        public AccommodationReservationRepository accommodationRepository { get; set; }

        public UserRepository userRepository { get; set; }

        public AccommodationRateRepository accommodationRateRepository { get; set; }
        public RateDisplayPage()
        {
            InitializeComponent();
            Accommodations = new ObservableCollection<AccommodationRateDTO>();
            accommodationRepository = new AccommodationReservationRepository();
            userRepository = new UserRepository();
            accommodationRateRepository = new AccommodationRateRepository();
            DataContext = this;

            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationRate accommodationRate in accommodationRateRepository.GetAll())
            {
                AccommodationReservation accommodationReservation = accommodationRepository.GetById(accommodationRate.ReservationId);
                User user = userRepository.GetById(accommodationRate.GuestId);
                Accommodations.Add(new AccommodationRateDTO(accommodationRate, accommodationReservation, user));

            }
        }

        private void RateGuest_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
