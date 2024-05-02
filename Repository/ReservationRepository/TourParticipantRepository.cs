using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.ReservationRepository
{
    public class TourParticipantRepository : ITourParticipantRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_participants.csv";

        private readonly Serializer<TourParticipant> _serializer;
        private TouristNotificationRepository _notificationRepository;
        private TourReservationRepository _reservationrepository;
        private UserRepository _userRepository;
        private TourRepository _tourRepository;
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

        public TourParticipant SaveParticipant(string name, string lastName, string age)
        {
            TourParticipant tourParticipant = new TourParticipant();
            tourParticipant.Name = name;
            tourParticipant.LastName = lastName;
            tourParticipant.Years = Convert.ToInt32(age);
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
            List<TourParticipant> allTourParticipants = GetAll();
            List<TourParticipant> tourParticipantsByReservation = allTourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            List<int> tourParticipantIds = new List<int>();

            foreach (TourParticipant tp in  tourParticipantsByReservation)
                tourParticipantIds.Add(tp.Id);

            return tourParticipantIds;
        }

        public List<TourParticipant> GetAllParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipantsByReservation = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            List<TourParticipant> tourParticipants = new List<TourParticipant>();

            foreach (TourParticipant tp in tourParticipantsByReservation)
                tourParticipants.Add(tp);

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
        
        public void Update(TourParticipant tourParticipant)
        {
            _tourParticipants = GetAll(); //ucitavanje
            TourParticipant current = GetById(tourParticipant.Id);
            int index = _tourParticipants.IndexOf(current);
            _tourParticipants.Remove(current);
            _tourParticipants.Insert(index, tourParticipant);
            _serializer.ToCSV(FilePath, _tourParticipants);
        }

        public bool IsUserJoined(int reservationId, string touristName, string touristLastName)
        {
            _tourParticipants = GetAll(); // ovo da bi procitao nov sadrzaj iz csv-a
            List<TourParticipant> tourParticipants = _tourParticipants.FindAll(tp => tp.ReservationId == reservationId);
            foreach(TourParticipant tp in tourParticipants){
                if(tp.Name.Equals(touristName) && tp.LastName.Equals(touristLastName) && tp.HasJoinedTour == true)
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
