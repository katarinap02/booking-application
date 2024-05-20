using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Serializer;
using BookingApp.WPF.View.GuideTestWindows.GuideControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/complex_tour_requests.csv";
        private readonly Serializer<ComplexTourRequest> _serializer;
        private List<ComplexTourRequest> _complexTourRequests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequests = _serializer.FromCSV(FilePath);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public List<ComplexTourRequest> GetAllPending()
        {
            return GetAll().FindAll(x => x.Status == ComplexTourRequestStatus.Pending);
        }
        public void Add(ComplexTourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _complexTourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
        }
        public int NextId()
        {
            _complexTourRequests = GetAll();
            if (_complexTourRequests.Count < 1)
                return 1;
            return _complexTourRequests.Max(r => r.Id) + 1;
        }
        public ComplexTourRequest GetById(int id)
        {
            return _complexTourRequests.Find(x => x.Id == id);
        }
    }
}
