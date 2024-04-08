using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Services
{
    public class AccommodationService
    {
        private readonly AccommodationRepository AccommodationRepository;
        
        public AccommodationService()
        {
            AccommodationRepository = new AccommodationRepository();
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationRepository.GetAll();
        }

        public Accommodation Add(Accommodation accommodation)
        {
            return AccommodationRepository.Add(accommodation);
        }

        public void Delete(Accommodation accommodation) { 
        
             AccommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return AccommodationRepository.Update(accommodation);
        }

        public Accommodation GetById(int id)
        {
            return AccommodationRepository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            AccommodationRepository.AccommodationSubject.Subscribe(observer);
        }

        public void FreeDateRange(Accommodation accommodation, AccommodationReservationDTO selectedReservation)
        {
            foreach(CalendarDateRange dateRange in accommodation.UnavailableDates)
            {
                if(dateRange.Start == selectedReservation.StartDate && dateRange.End == selectedReservation.EndDate)
                {
                    accommodation.UnavailableDates.Remove(dateRange);
                    break;
                }

            }

            AccommodationRepository.Update(accommodation);
        }
    }
}
