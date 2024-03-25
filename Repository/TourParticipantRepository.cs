using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourParticipantRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_participants.csv";

        private readonly Serializer<TourParticipant> _serializer;

        private List<TourParticipant> _tourParticipants;

        public TourParticipantRepository()
        {
            _serializer = new Serializer<TourParticipant>();
            _tourParticipants = _serializer.FromCSV(FilePath);
        }

        public List<TourParticipant> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(TourParticipant tourParticipant)
        {
            _tourParticipants.Add(tourParticipant);
            _serializer.ToCSV(FilePath, _tourParticipants);
        }

        public int NextId()
        {
            _tourParticipants = _serializer.FromCSV(FilePath);
            if (_tourParticipants.Count < 1)
                return 1;
            return _tourParticipants.Max(t => t.Id) + 1;
        }

        public List<int> GetAllIdsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipantsByReservation = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            List<int> tourParticipantIds = new List<int>();

            foreach (TourParticipant tp in  tourParticipantsByReservation)
                tourParticipantIds.Add(tp.Id);

            return tourParticipantIds;
        }

        public string GetAllParticipantNames(int reservationId) 
        {
            List<TourParticipant> tourParticipantsByReservation = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            List<string> participantNames = new List<string>();
            foreach(TourParticipant tp in tourParticipantsByReservation)
            {
                participantNames.Add(tp.Name+" "+tp.LastName);
            }
            return string.Join(", ", participantNames);
        }

        
        
    }
}
