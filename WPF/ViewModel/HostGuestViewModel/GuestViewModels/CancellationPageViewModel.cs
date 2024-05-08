using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class CancellationPageViewModel
    {
        public string AccommodationDetails { get; set; }


        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public ReservationCancellationService ReservationCancellationService { get; set; }

        public User User { get; set; }
        public Frame Frame { get; set; }

        // KOMANDE
        public GuestICommand CancelReservationCommand { get; set; }
        public GuestICommand GiveUpCommand { get; set; }
        public CancellationPageViewModel(AccommodationReservationViewModel selectedReservation, User user, Frame frame)
        {
            SelectedReservation = selectedReservation;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
            CancelReservationCommand = new GuestICommand(OnCancelReservation);
            GiveUpCommand = new GuestICommand(OnGiveUp);
            User = user;
            Frame = frame;


        }

        private void OnGiveUp()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }

        private void OnCancelReservation()
        {
            AccommodationReservationService.CancelReservation(AccommodationService, ReservationCancellationService, SelectedReservation);
            Frame.Content = new CancelReservationSuccessfulPage(User, Frame, SelectedReservation);
        }

     

    }
}
