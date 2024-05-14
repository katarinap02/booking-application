using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tour_requests.csv";
        private readonly Serializer<TourRequest> _serializer;
        private List<TourRequest> _tourRequests;
        
        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequests = _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAll() 
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAllPending()
        {
            return GetAll().FindAll(x => x.Status == TourRequestStatus.Pending);
        }

        public List<TourRequest> GetAllPendingOrInvalid()
        {
            return GetAll().FindAll(tr => tr.Status == TourRequestStatus.Pending || tr.Status == TourRequestStatus.Invalid);
        }
        
        public void Add(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public List<TourRequest> GetByTouristId(int touristId)
        {
            _tourRequests = GetAll();
           return _tourRequests.FindAll(r => r.TouristId == touristId);
        }

        public int NextId()
        {
            _tourRequests = GetAll();
            if(_tourRequests.Count < 1)
                return 1;
            return _tourRequests.Max(r => r.Id) + 1;
        }
        public List<TourRequest> GetAllForYear(int year)
        {
            return GetAll().FindAll(x => x.DateRequested.Year == year);
        }     

        public TourRequest GetById(int id) // ostaje u repo
        {
            return _tourRequests.Find(x => x.Id == id);
        }

        public void UpdateRequest(TourRequest request) // ostaje u repo
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequests.Find(x => x.Id == request.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, request);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public List<TourRequest> GetRequestsBetweenDates(DateTime startDate, DateTime endDate)
        {
            List<TourRequest> requests = GetAll();
            return requests.Where(request => request.DateRequested >= startDate && request.DateRequested <= endDate).ToList();
        }


    }
}

