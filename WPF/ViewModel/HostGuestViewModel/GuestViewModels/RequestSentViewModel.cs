using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.Model;
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
    public class RequestSentViewModel
    {
        public DelayRequestViewModel SelectedRequest { get; set; }

        public HostService HostService { get; set; }
        public string HostUsername { get; set; }

        public string OldDateRange { get; set; }
        public string NewDateRange { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfDays { get; set; }
        public AccommodationViewModel Accommodation { get; set; }

        public DelayRequestService DelayRequestService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public RequestSentViewModel(DelayRequestViewModel selectedRequest)
        {
            SelectedRequest = selectedRequest;
            DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            AccommodationReservation reservation = AccommodationReservationService.GetById(SelectedRequest.ReservationId);
            Accommodation = new AccommodationViewModel(AccommodationService.GetById(reservation.AccommodationId));
            HostUsername = HostService.GetById(SelectedRequest.HostId).Username;
            OldDateRange = SelectedRequest.StartLastDate.ToString("MM/dd/yyyy") + " -> " + selectedRequest.EndLastDate.ToString("MM/dd/yyyy");
            NewDateRange = SelectedRequest.StartDate.ToString("MM/dd/yyyy") + " -> " + SelectedRequest.EndDate.ToString("MM/dd/yyyy");
            NumberOfPeople = reservation.NumberOfPeople;
     
            NumberOfDays = (reservation.EndDate - reservation.StartDate).Days + 1;
        }
    }
}
