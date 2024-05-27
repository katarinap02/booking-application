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
    }
}
