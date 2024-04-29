using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class GuestService
    {
        private readonly IGuestRepository GuestRepository;
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public GuestService(IGuestRepository guestRepository, IAccommodationReservationRepository accommodationReservationRepository, IDelayRequestRepository delayRequest)
        {
            GuestRepository = guestRepository;
            AccommodationReservationService = new AccommodationReservationService(accommodationReservationRepository, delayRequest);
        }

        public Guest GetById(int id)
        {
            return GuestRepository.GetById(id);
        }

        public List<Guest> GetAll()
        {
            return GuestRepository.GetAll();
        }

        public Guest Update(Guest guest)
        {
            return GuestRepository.Update(guest);
        }

        public void CalculateGuestStats(Guest guest)
        {
            guest.YearlyReservations = GetYearlyReservations(guest);
            if(!guest.IsSuperGuest)
                BecomeSuperGuest(guest);

            if (guest.IsSuperGuest)
                CheckDueDate(guest);

            Update(guest);

        }

        private void CheckDueDate(Guest guest)
        {
            if(guest.EndDate == DateTime.Now)
            {
                if(guest.YearlyReservations >= 10)
                {
                    ActivateSuperGuest(guest);
                }
                else
                {
                    DeactivateSuperGuest(guest);
                    
                }
             
            }
        }

        private void DeactivateSuperGuest(Guest guest)
        {
            guest.IsSuperGuest = false;
            guest.BonusPoints = 0;
            guest.StartDate = DateTime.MinValue;
            guest.EndDate = DateTime.MinValue;
        }

        private void BecomeSuperGuest(Guest guest)
        {
            if(guest.YearlyReservations >= 10)
            {
                ActivateSuperGuest(guest);
                
            }    
        }

        private void ActivateSuperGuest(Guest guest)
        {
            guest.IsSuperGuest = true;
            guest.StartDate = DateTime.Now;
            guest.EndDate = DateTime.Now.AddYears(1);
            guest.BonusPoints = 5;
        }

        private int GetYearlyReservations(Guest guest)
        {
            int counter = 0;
            foreach(AccommodationReservation accommodationReservation in AccommodationReservationService.GetAll())
            {
                if (IsInThePastYear(accommodationReservation.StartDate) && guest.Id == accommodationReservation.GuestId)
                {
                    counter++;
                }
            }
            return counter;
        }

        private bool IsInThePastYear(DateTime startDate)
        {
            if(startDate <= DateTime.Now && startDate >= DateTime.Now.AddYears(-1))
                return true;
            else
                return false;
        }
    }
}
