using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ProfileInfoViewModel : IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public Frame Frame { get; set; }
        public string Status { get; set; }
        public int TotalReservations { get; set; }
        public int TotalYearReservations { get; set; }
        public ProfileInfoViewModel(User user, Frame frame)
        {
            Frame = frame;
            User = user;
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            AccommodationReservationService = new AccommodationReservationService();
            AccommodationService = new AccommodationService();
            Status = "guest";
            TotalReservations = GetTotalReservations(AccommodationReservationService);
            TotalYearReservations = GetTotalReservationsYear(AccommodationReservationService);
        }



        private int GetTotalReservationsYear(AccommodationReservationService accommodationReservationService)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id && reservation.StartDate.Year == DateTime.Now.Year)
                    number++;
            }

            return number;
        }

        private int GetTotalReservations(AccommodationReservationService accommodationReservationService)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                    number++;
            }

            return number;
        }

        public void Update()
        {
            Reservations.Clear();

            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                {
                    Reservations.Add(new AccommodationReservationViewModel(reservation));
                }
            }
        }
    }
}
