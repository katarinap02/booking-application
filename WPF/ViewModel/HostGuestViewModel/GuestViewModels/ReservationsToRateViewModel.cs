using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationsToRateViewModel : IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }

        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }

        public Frame Frame { get; set; }

        public ReservationsToRateViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation)
        {

            User = user;
            Frame = frame;
            // SelectedReservation = reservation;
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            AccommodationRateService = new AccommodationRateService();
            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();
            SelectedReservation = selectedReservation;


        }
        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {

                if (reservation.GuestId == User.Id && IsBeforeFiveDays(reservation) && !IsReservationRated(reservation))
                {
                    Reservations.Add(new AccommodationReservationViewModel(reservation));

                }
            }
        }

        private bool IsReservationRated(AccommodationReservation reservation)
        {
            bool isFound = false;
            foreach (AccommodationRate rate in AccommodationRateService.GetAll())
            {
                if (rate.ReservationId == reservation.Id)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;


        }

        private bool IsBeforeFiveDays(AccommodationReservation reservation)
        {
            int daysPassed = (DateTime.Now - reservation.EndDate).Days;
            if (daysPassed <= 5 && daysPassed > 0)
                return true;
            else
                return false;
        }

        public void Rate_Click(object sender, RoutedEventArgs e)
        {

            Frame.Content = new RateAccommodationForm(User, SelectedReservation, Frame);
        }
    }
}
