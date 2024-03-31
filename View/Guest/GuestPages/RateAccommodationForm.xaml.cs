using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RateAccommodationForm.xaml
    /// </summary>
    public partial class RateAccommodationForm : Page
    {
        public AccommodationReservationDTO SelectedReservation { get; set; }
        public User User { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public Frame Frame { get; set; }
        public RateAccommodationForm(User user, AccommodationReservationDTO selectedReservation, AccommodationRepository accommodationRepository, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.SelectedReservation = selectedReservation;
            this.AccommodationRepository = accommodationRepository;
            this.Frame = frame;

        }
    }
}
