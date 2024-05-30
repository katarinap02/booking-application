using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.Features;

namespace BookingApp.Application.Services.FeatureServices
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        private static readonly TourRequestService _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());

        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository)
        {
            _complexTourRequestRepository = complexTourRequestRepository;
        }

        public void Add(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.Add(complexTourRequest);
        }

        public List<ComplexTourRequest> GetByTouristId(int touristId)
        {
            return _complexTourRequestRepository.GetAllById(touristId);
        }

        public List<TourRequest> GetTourRequestsByComplexId(int complexId)
        {
            List<TourRequest> requests = new List<TourRequest>();
            List<int> tourRequestIds = _complexTourRequestRepository.GetAllTourRequests(complexId);
            if(tourRequestIds == null)
            {
                return requests;
            }
            if(tourRequestIds.Count == 0)
            {
                return requests;
            }
            foreach(int id in tourRequestIds)
            {
                requests.Add(_tourRequestService.GetById(id));
            }
            return requests;
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequestRepository.GetAll();
        }

        public void UpdateComplexRequest(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestRepository.UpdateRequest(complexTourRequest);
        }
    }
}
