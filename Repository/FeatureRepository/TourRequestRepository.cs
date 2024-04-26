using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class TourRequestRepository
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
            
        public void AcceptRequest(TourRequest request, int GuideId, DateTime acceptedDate) // za servis
        {
            TourRequest tourRequest = GetById(request.Id);
            tourRequest.Status = TourRequestStatus.Accepted;
            tourRequest.AcceptedDate = acceptedDate;
            tourRequest.GuideId = GuideId;
            UpdateRequest(tourRequest);
        }

        public List<TourRequest> filterRequests(TourRequest searchCriteria) // service prebaciti
        {
            List<TourRequest> tourRequests = GetAllPending();
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

