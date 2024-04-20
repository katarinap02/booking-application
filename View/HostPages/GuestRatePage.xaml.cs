using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.ViewModel;
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
        public GuestRatePage(User user)
        {
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            
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
    }
}
