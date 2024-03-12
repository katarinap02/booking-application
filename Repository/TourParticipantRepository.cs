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
        private const string FilePath = "../../../Resources/Data/tourparticipants.csv";

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
        
    }
}
