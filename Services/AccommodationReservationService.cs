using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationReservationService
    {
        private readonly AccommodationReservationRepository AccommodationReservationRepository;

        public AccommodationReservationService()
        {
            AccommodationReservationRepository = new AccommodationReservationRepository();
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

        public AccommodationReservation GetById(int id)
        {
            return AccommodationReservationRepository.GetById(id);
        }

    }
}
