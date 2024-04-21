using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
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
        public CancellationPageViewModel(AccommodationReservationViewModel selectedReservation)
        {
            SelectedReservation = selectedReservation;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());


        }

        public void CancelReservation_Click(object sender, RoutedEventArgs e)
        {


            AccommodationReservationService.CancelReservation(AccommodationService, ReservationCancellationService, SelectedReservation);


        }

    }
}
