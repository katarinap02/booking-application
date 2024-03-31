using BookingApp.DTO;
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
using System.Windows.Shapes;
using BookingApp.Observer;
using BookingApp.Model;
using System.Security.Cryptography;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page, IObserver
    {
        public ObservableCollection<AccommodationReservationDTO> Reservations { get; set; }
        public User User { get; set; }  
        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }
        public Frame Frame { get; set; }

        public ProfilePage(User user, AccommodationReservationRepository accommodationReservationRepository, Frame frame)
        {
            InitializeComponent();
            Reservations = new ObservableCollection<AccommodationReservationDTO>();
            this.User = user;
            this.Frame = frame;
            this.AccommodationReservationRepository = accommodationReservationRepository;
            DataContext = this;
            Update();
           

        }

      

        public void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Update()
        {
            Reservations.Clear();

            foreach (AccommodationReservation reservation in AccommodationReservationRepository.GetAll())
            {
                if (reservation.GuestId == User.Id)
                {
                    Reservations.Add(new AccommodationReservationDTO(reservation));
                }
            }
        }
       
    }
}
