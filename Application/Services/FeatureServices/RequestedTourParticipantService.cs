using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class RequestedTourParticipantService
    {
        private readonly IRequestedTourParticipantRepository _requestedTourParticipantRepository;

        public RequestedTourParticipantService(IRequestedTourParticipantRepository requestedTourParticipantRepository)
        {
            _requestedTourParticipantRepository = requestedTourParticipantRepository;
        }

        public void SaveRequestedTourParticipant(RequestedTourParticipant requestedTourParticipant, int requestId)
        {
            requestedTourParticipant.TourRequestId = requestId;
            _requestedTourParticipantRepository.Add(requestedTourParticipant);
        }   

        public int NextParticipantId()
        {
            return _requestedTourParticipantRepository.NextId();
        }
    }
}
