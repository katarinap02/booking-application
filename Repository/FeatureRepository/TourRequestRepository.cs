using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
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
    }
}

