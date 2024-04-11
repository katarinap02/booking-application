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
using BookingApp.View.ViewModel;

namespace BookingApp.View
{
    public partial class ReservationInfoWindow : Window
    {

        public AccommodationRepository AccommodationRepository { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationReservation Reservation { get; set; }
        public User User { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }
        public ReservationInfoWindow(AccommodationRepository accommodationRepository, AccommodationViewModel selectedAccommodation, User user, CalendarDateRange selectedDateRange) {

            InitializeComponent();
            this.AccommodationRepository = accommodationRepository;
            this.SelectedAccommodation = selectedAccommodation;
            this.User = user;
            this.SelectedDateRange = selectedDateRange;
            Reservation = new AccommodationReservation();
            AccommodationReservationRepository = new AccommodationReservationRepository();
        
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            int inputGuestNumber = Convert.ToInt32(txtGuestNumber.Text);
            if(inputGuestNumber > SelectedAccommodation.MaxGuestNumber) {

                MessageBox.Show("Max. guest number is: " + SelectedAccommodation.MaxGuestNumber.ToString());
            }
            else
            {
                Reservation.GuestId = User.Id;
                Reservation.AccommodationId = SelectedAccommodation.Id;
                Reservation.StartDate = SelectedDateRange.Start;
                Reservation.EndDate = SelectedDateRange.End;

                SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
                AccommodationRepository.Update(SelectedAccommodation.ToAccommodation());

                AccommodationReservationRepository.Add(Reservation);

                MessageBox.Show("Reservation added");

                this.Close();


            }
            
        }
    }
}
