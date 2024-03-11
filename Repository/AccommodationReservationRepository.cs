using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_reservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        private List<AccommodationReservation> _reservations;

        public Subject ReservationSubject { get;  set; }

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _reservations = new List<AccommodationReservation>();
            ReservationSubject = new Subject();

        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<AccommodationReservation> GetGuestForRate()
        {
            DateTime today = DateTime.Now;
            List < AccommodationReservation > returnGuest = new List<AccommodationReservation> ();
            _reservations = _serializer.FromCSV(FilePath);
            foreach (AccommodationReservation ar in _reservations)
            {
                if (ar.EndDate.AddDays(5) >= today && ar.EndDate < today)
                {
                    returnGuest.Add(ar);
                }
                
            }
            return returnGuest;

        }

        public AccommodationReservation Add(AccommodationReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            ReservationSubject.NotifyObservers();
            return reservation;
        }


    }
}
