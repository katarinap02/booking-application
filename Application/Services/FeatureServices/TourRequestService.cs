using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository;

        public TourRequestService(ITourRequestRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }

        public void AcceptRequest(TourRequest request, int GuideId, DateTime acceptedDate)
        {
            TourRequest tourRequest = _tourRequestRepository.GetById(request.Id);
            tourRequest.Status = TourRequestStatus.Accepted;
            tourRequest.AcceptedDate = acceptedDate;
            tourRequest.GuideId = GuideId;
            _tourRequestRepository.UpdateRequest(tourRequest);
        }
        public List<TourRequest> filterRequests(TourRequest searchCriteria)
        {
            List<TourRequest> tourRequests = _tourRequestRepository.GetAllPending();
            List<TourRequest> filteredRequests = new List<TourRequest>();

            if (!string.IsNullOrEmpty(searchCriteria.City))
            {
                filteredRequests = tourRequests.FindAll(x => x.City.ToLower().Contains(searchCriteria.City.ToLower())).ToList();
            }
            else if (!string.IsNullOrEmpty(searchCriteria.Country))
            {
                filteredRequests = filteredRequests.FindAll(x => x.Country.ToLower().Contains(searchCriteria.Country.ToLower())).ToList();
            }
            else if (!string.IsNullOrEmpty(searchCriteria.Language))
            {
                filteredRequests = filteredRequests.FindAll(x => x.Language.ToLower().Contains(searchCriteria.Language.ToLower())).ToList();
            }
            else if (searchCriteria.StartDate != null && searchCriteria.EndDate != null)
            {
                filteredRequests = filteredRequests.FindAll(x => x.StartDate >= searchCriteria.StartDate).ToList();
                filteredRequests = filteredRequests.FindAll(x => x.EndDate <= searchCriteria.EndDate).ToList();
            }


            return filteredRequests;
        }
    
    }
}
