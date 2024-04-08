using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Services;
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
    /// Interaction logic for CancelReservationPage.xaml
    /// </summary>
    /// 

   

    public partial class CancelReservationPage : Page
    {
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationDTO SelectedReservation { get; set; }

        public User User { get; set; }  
        public Frame Frame { get; set; }
        public CancelReservationPage(AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, AccommodationReservationDTO selectedReservation, User user, Frame frame)
        {
            InitializeComponent();
            this.AccommodationReservationService = accommodationReservationService;
            this.AccommodationService = accommodationService;
            this.SelectedReservation = selectedReservation;
            this.User = user;
            this.Frame = frame;

        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            int daysBefore = (SelectedReservation.StartDate - DateTime.Today).Days;
            int dayLimit = AccommodationService.GetById(SelectedReservation.AccommodationId).ReservationDaysLimit;
            if (daysBefore < dayLimit)
            {
                MessageBox.Show("It is too late to cancel reservation");
            }
            else
            {
                AccommodationReservationService.Delete(SelectedReservation);
                AccommodationService.FreeDateRange(AccommodationService.GetById(SelectedReservation.AccommodationId), SelectedReservation);
                MessageBox.Show("jej");
            }

        }
    }
}
