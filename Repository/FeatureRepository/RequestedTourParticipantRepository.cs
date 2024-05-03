using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class RequestedTourParticipantRepository : IRequestedTourParticipantRepository
    {
        private const string FilePath = "../../../Resources/Data/requested_tour_participants.csv";
        private readonly Serializer<RequestedTourParticipant> _serializer;
        private List<RequestedTourParticipant> _requestedTourParticipants;

        public RequestedTourParticipantRepository()
        {
            _serializer = new Serializer<RequestedTourParticipant>();
            _requestedTourParticipants = _serializer.FromCSV(FilePath);
        }

        public List<RequestedTourParticipant> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(RequestedTourParticipant requestedTourParticipant)
        {
            requestedTourParticipant.Id = NextId();
            _requestedTourParticipants.Add(requestedTourParticipant);
            _serializer.ToCSV(FilePath, _requestedTourParticipants);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _requestedTourParticipants);
        }

        public int NextId()
        {
            _requestedTourParticipants = GetAll();
            if (_requestedTourParticipants.Count < 1)
                return 1;
            return _requestedTourParticipants.Max(r => r.Id) + 1;
        }
    }
}
