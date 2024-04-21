using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Application.Services;
using BookingApp.WPF.ViewModel;
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
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for GuestRatePage.xaml
    /// </summary>
    public partial class GuestRatePage : Page, IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public HostService hostService { get; set; }

        public Host host { get; set; }
        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public GuestRateViewModel guestRateViewModel { get; set; }

        public GuestRateRepository guestRateRepository { get; set; }
        public GuestRatePage(User user)
        {
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            guestRateRepository = new GuestRateRepository();
            guestRateViewModel = new GuestRateViewModel();
            hostService = new HostService();
            host = hostService.GetByUsername(user.Username);
            DataContext = this;
            InitializeComponent();

            Update();
        }

        public void Update()
        {
              Accommodations.Clear();
               foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
             {
                if(accommodationRepository.GetById(accommodation.AccommodationId).HostId == host.Id)
                 Accommodations.Add(new AccommodationReservationViewModel(accommodation));

             }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                guestRateViewModel.ReservationId = SelectedAccommodation.Id;
                guestRateViewModel.GuestId = SelectedAccommodation.GuestId;
                guestRateViewModel.AccommodationId = SelectedAccommodation.AccommodationId;
                guestRateRepository.Add(guestRateViewModel.toGuestRate());
                MessageBox.Show("Guest rate added.");
            }
            Update();

        }
    }
}
