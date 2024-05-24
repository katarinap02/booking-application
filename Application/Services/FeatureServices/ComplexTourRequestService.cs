using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        
        public ComplexTourRequestService(IComplexTourRequestRepository complexTourRequestRepository)
        {
            _complexTourRequestRepository = complexTourRequestRepository;
        }
    }
}
