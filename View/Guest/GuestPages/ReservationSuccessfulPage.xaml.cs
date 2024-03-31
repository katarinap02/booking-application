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
    /// Interaction logic for AccommodationSuccessfulPage.xaml
    /// </summary>
    public partial class ReservationSuccessfulPage : Page
    {
        public AccommodationRepository AccommodationRepository { get; set; }
        public AccommodationRateRepository AccommodationRateRepository { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }

        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }
        public CalendarDateRange SelectedDateRange { get; set; }
        public User User { get; set; }

        public Frame Frame { get; set; }

        public int GuestNumber { get; set; }
        public ReservationSuccessfulPage(AccommodationRepository accommodationRepository, AccommodationReservationRepository accommodationReservationRepository, AccommodationDTO selectedAccommodation, CalendarDateRange selectedDateRange, int guestNumber, User user, Frame frame)
        {
            InitializeComponent();

            this.AccommodationRepository = accommodationRepository;
            this.SelectedAccommodation = selectedAccommodation;
            this.SelectedDateRange = selectedDateRange;
            this.GuestNumber = guestNumber;
            this.User = user;
            this.Frame = frame;
            this.AccommodationReservationRepository = accommodationReservationRepository;

            PrintAccommodationLocation(SelectedAccommodation);
            PrintDateRange(SelectedDateRange);
            PrintDayNumber(SelectedDateRange);
            PrintGuestNumber(GuestNumber);
        }

        private void PrintGuestNumber(int guestNumber)
        {
            reservationDetailsCard.lblNumberOfPeople.Content = guestNumber.ToString();
        }

        private void PrintDayNumber(CalendarDateRange selectedDateRange)
        {
           reservationDetailsCard.lblNumberOfDays.Content = ((selectedDateRange.End - selectedDateRange.Start).Days + 1).ToString();
        }

        private void PrintDateRange(CalendarDateRange selectedDateRange)
        {
            string startDate = selectedDateRange.Start.ToString();
            string endDate = selectedDateRange.End.ToString();
            reservationDetailsCard.lblDateRange.Content = startDate + "-" + endDate;
        }

        private void PrintAccommodationLocation(AccommodationDTO selectedAccommodation)
        {
            reservationDetailsCard.lblAccommodationDetails.Content = selectedAccommodation.Name + ", " + selectedAccommodation.City + ", " + selectedAccommodation.Country;
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new HomePage(AccommodationRepository, AccommodationReservationRepository, User, Frame);
        }

        private void ProfilePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProfilePage(User, AccommodationReservationRepository, AccommodationRepository, AccommodationRateRepository, Frame);
        }
    }
}
