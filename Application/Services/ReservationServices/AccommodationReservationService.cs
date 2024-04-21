using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BookingApp.WPF.ViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;

namespace BookingApp.Application.Services.ReservationServices
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository AccommodationReservationRepository;

        public DelayRequestService DelayRequestService { get; set; }
        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository, IDelayRequestRepository delayRequest)
        {
            AccommodationReservationRepository = accommodationReservationRepository;
            DelayRequestService = new DelayRequestService(delayRequest);

        }

        public List<AccommodationReservation> GetAll()
        {
            return AccommodationReservationRepository.GetAll();
        }

        public List<AccommodationReservation> GetGuestForRate()
        {
            return AccommodationReservationRepository.GetGuestForRate();

        }

        public bool Rated(AccommodationReservation ar)
        {
            return AccommodationReservationRepository.Rated(ar);
        }

        public AccommodationReservation Add(AccommodationReservation reservation)
        {
            return AccommodationReservationRepository.Add(reservation);
        }


        public void Delete(AccommodationReservationViewModel selectedReservation)
        {
            AccommodationReservationRepository.Delete(selectedReservation);
        }

        public AccommodationReservation Update(AccommodationReservation reservation)
        {
            return AccommodationReservationRepository.Update(reservation);
        }

        public AccommodationReservation GetById(int id)
        {
            return AccommodationReservationRepository.GetById(id);
        }

        public void UpdateReservation(int id, DateTime StartDate, DateTime EndDate)
        {
            AccommodationReservation reservation = GetById(id);
            reservation.StartDate = StartDate;
            reservation.EndDate = EndDate;

            Update(reservation);
        }



        public bool IsReserved(DateTime StartDate, DateTime EndDate, int accommodationId)
        {
            foreach (AccommodationReservation res in GetAll())
            {
                if ((StartDate <= res.EndDate && StartDate >= res.StartDate || EndDate <= res.EndDate && EndDate >= res.StartDate) && res.AccommodationId == accommodationId)
                { return true; }

            }
            return false;
        }

        internal void CancelReservation(AccommodationService accommodationService, ReservationCancellationService cancellationService, AccommodationReservationViewModel reservation)
        {
            int daysBefore = (reservation.StartDate - DateTime.Today).Days;
            int dayLimit = accommodationService.GetById(reservation.AccommodationId).ReservationDaysLimit;
            if (daysBefore < dayLimit)
            {
                MessageBox.Show("It is too late to cancel reservation");
            }
            else
            {
                Accommodation accommodation = accommodationService.GetById(reservation.AccommodationId);
                ReservationCancellation cancellation = new ReservationCancellation(reservation.GuestId, accommodation.HostId, reservation.Id, DateTime.Now, reservation.StartDate, reservation.EndDate);
                cancellationService.Add(cancellation);
                foreach (DelayRequest request in DelayRequestService.GetAll())
                    if (request.ReservationId == reservation.Id)
                        DelayRequestService.Delete(request);
                AccommodationReservationRepository.Delete(reservation);
                accommodationService.FreeDateRange(accommodation, reservation);
                MessageBox.Show("Reservation cancelled");
            }
        }

        internal void DelayReservation(CalendarDateRange selectedDateRange, DelayRequest delayRequest, DelayRequestService delayRequestService, AccommodationService accommodationService, AccommodationReservationViewModel selectedReservation)
        {
            delayRequest.GuestId = selectedReservation.GuestId;
            Accommodation tmpAccommodation = accommodationService.GetById(selectedReservation.AccommodationId);
            delayRequest.HostId = tmpAccommodation.HostId;
            delayRequest.ReservationId = selectedReservation.Id;
            delayRequest.StartDate = selectedDateRange.Start;
            delayRequest.EndDate = selectedDateRange.End;
            delayRequest.StartLastDate = selectedReservation.StartDate;
            delayRequest.EndLastDate = selectedReservation.EndDate;
            delayRequestService.Add(delayRequest);
            MessageBox.Show("Request sent");
        }
    }
}
