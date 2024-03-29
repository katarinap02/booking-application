using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    public class TourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_reservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _tourReservations;
        private readonly TourParticipantRepository _participantRepository;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservations = GetAll();
            _participantRepository = new TourParticipantRepository();
        }

        public List<TourReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservations);
        }

        public int NextId()
        {
            _tourReservations = _serializer.FromCSV(FilePath);
            if(_tourReservations.Count < 1)
                return 1;
            return _tourReservations.Max(t => t.Id) + 1;
        }

        public List<TourReservation> GetReservationsByTour(int tour_id)
        {
            List < TourReservation > reservations = new List < TourReservation >();
            List<TourReservation> allReservations = GetAll();
            reservations = allReservations.FindAll(res  => res.TourId == tour_id);
            return reservations;
        }

        public List<TourParticipant> GetJoinedParticipantsByTour(int tour_id)
        {
            List<TourReservation> tourReservations = GetReservationsByTour(tour_id);
            List<TourParticipant> participants = new List<TourParticipant>();
            foreach(TourReservation tourReservation in tourReservations.ToList())
            {
                foreach (TourParticipant participant in _participantRepository.GetAllJoinedParticipantsByReservation(tourReservation.Id))
                {
                    participants.Add(participant);
                }
            }
            return participants;
        }

        public int GetNumberOfJoinedParticipants(int tour_id)
        {
            List<TourParticipant> tourParticipants = GetJoinedParticipantsByTour(tour_id);
            return tourParticipants.Count();
        }


        public List<TourParticipant> GetNotJoinedReservations(int tour_id) 
        {
            List<TourReservation> tourReservations = GetReservationsByTour(tour_id);
            List<TourParticipant> participants = new List<TourParticipant>();
            foreach (TourReservation tourReservation in tourReservations.ToList())
            {
                foreach (TourParticipant participant in _participantRepository.GetAllNotJoinedParticipantsByReservation(tourReservation.Id))
                {
                    participants.Add(participant);
                }
            }
            return participants;
        }

        public TourReservation GetById(int reservation_id)
        {
            return _tourReservations.Find(res => res.Id == reservation_id);
        }        

    }
}
