using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IComplexTourRequestRepository
    {
        List<ComplexTourRequest> GetAll();
        List<ComplexTourRequest> GetAllPending();
        void Add(ComplexTourRequest tourRequest);
        int NextId();
        ComplexTourRequest GetById(int id);
        List<ComplexTourRequest> GetAllById(int touristId);
        List<int> GetAllTourRequests(int complexId);
        void UpdateRequest(ComplexTourRequest request);
    }
}
