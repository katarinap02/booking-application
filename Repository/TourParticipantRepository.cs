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

        public TourParticipant GetById(int id)
        {
            return _tourParticipants.Find(tp => tp.Id == id);
        }

        public TourParticipant SaveParticipant(string name, string lastName, string years)
        {
            TourParticipant tourParticipant = new TourParticipant();
            tourParticipant.Name = name;
            tourParticipant.LastName = lastName;
            tourParticipant.Years = Convert.ToInt32(years);
            tourParticipant.Id = NextId();

            return tourParticipant;
        }

        public void SaveParticipant(TourParticipant tourParticipant, int reservationId)
        {
            tourParticipant.ReservationId = reservationId;
            tourParticipant.Id = NextId();
            Add(tourParticipant);
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

        public List<TourParticipant> GetAllParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipantsByReservation = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            List<TourParticipant> tourParticipants = new List<TourParticipant>();

            foreach (TourParticipant tp in tourParticipantsByReservation)
                tourParticipants.Add(tp);

            return tourParticipants;
        }

        public List<TourParticipant> GetAllJoinedParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipants = GetAllParticipantsByReservation(reservationId);
            foreach(TourParticipant tp in tourParticipants.ToList())
            {
                if (!tp.HasJoinedTour) //ako se nije prikljucio
                {
                    tourParticipants.Remove(tp);
                }
            }

            return tourParticipants;
        }

        public List<TourParticipant> GetAllNotJoinedParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipants = GetAllParticipantsByReservation(reservationId);
            foreach (TourParticipant tp in tourParticipants.ToList())
            {
                if (tp.HasJoinedTour)
                {
                    tourParticipants.Remove(tp);
                }
            }

            return tourParticipants;
        }
        
        public void JoinTour(int participant_id, int current_checkpoint_index)
        {
            TourParticipant tourParticipant = GetById(participant_id);
            tourParticipant.HasJoinedTour = true;
            tourParticipant.JoinedCheckpointIndex = current_checkpoint_index;
            _serializer.ToCSV(FilePath, _tourParticipants);
        }

        public bool IsSomeoneJoinedToTourByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipants = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            foreach(TourParticipant tp in tourParticipants){
                if(tp.HasJoinedTour == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
