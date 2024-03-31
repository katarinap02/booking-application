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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RateAccommodationPage.xaml
    /// </summary>
    public partial class RateAccommodationPage : Page, IObserver
    {

        public ObservableCollection<AccommodationReservationDTO> Reservations { get; set; }
        public User User { get; set; }

        public AccommodationReservationDTO SelectedReservation {  get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }
        public AccommodationRateRepository AccommodationRateRepository { get; set; }
        
        public Frame Frame {  get; set; }   

        public RateAccommodationPage(User user, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, AccommodationRateRepository accommodationRateRepository, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.AccommodationReservationRepository = accommodationReservationRepository;
            this.Frame = frame;
            Reservations = new ObservableCollection<AccommodationReservationDTO>();
            DataContext = this;
            this.AccommodationRepository = accommodationRepository;
            this.AccommodationRateRepository = accommodationRateRepository;
            Update();

        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in AccommodationReservationRepository.GetAll())
            {
                if (reservation.GuestId == User.Id && IsBeforeFiveDays(reservation))
                {
                    Reservations.Add(new AccommodationReservationDTO(reservation));
                }
            }
        }

        private bool IsBeforeFiveDays(AccommodationReservation reservation)
        {
            int daysPassed = (DateTime.Now - reservation.EndDate).Days;
            if (daysPassed <= 5)
                return true;
            else
                return false;
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new RateAccommodationForm(User, SelectedReservation, AccommodationRepository, AccommodationRateRepository, Frame);
        }
    }
}
