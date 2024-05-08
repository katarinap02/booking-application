using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RateDetailsViewModel
    {
        public GuestRateViewModel SelectedRate { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public HostService HostService { get; set; }

        public string HostUsername { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public string DateRange { get; set; }
        public RateDetailsViewModel(GuestRateViewModel selectedRate)
        {
            SelectedRate = selectedRate;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            HostUsername = GetHostUsername(SelectedRate, AccommodationService);
            DateRange = GetReservationDates(SelectedRate, AccommodationReservationService);
        }

        private string? GetReservationDates(GuestRateViewModel selectedRate, AccommodationReservationService accommodationReservationService)
        {
            AccommodationReservation reservation = AccommodationReservationService.GetById(selectedRate.ReservationId);
            return reservation.StartDate.ToString("MM/dd/yyyy") + " -> " + reservation.EndDate.ToString("MM/dd/yyyy");
        }

        private string? GetHostUsername(GuestRateViewModel selectedRate, AccommodationService accommodationService)
        {
            Accommodation accommodation = AccommodationService.GetById(selectedRate.AccommodationId);
            Host host = HostService.GetById(accommodation.HostId);
            return host.Username;
        }
    }
}
