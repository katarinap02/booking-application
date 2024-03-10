using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
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
