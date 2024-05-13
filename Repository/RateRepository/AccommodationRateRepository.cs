using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRateRepository : IAccommodationRateRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodation_rate.csv";
        private readonly Serializer<AccommodationRate> _serializer;
        private List<AccommodationRate> _rates;
        
        public Subject RateSubject { get; set; }

        public AccommodationRateRepository()
        {
            _serializer = new Serializer<AccommodationRate>();
            _rates = new List<AccommodationRate>();
            RateSubject = new Subject();
           
        }

        public List<AccommodationRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationRate Add(AccommodationRate rate)
        {
            _rates = _serializer.FromCSV(FilePath);
            _rates.Add(rate);
            _serializer.ToCSV(FilePath, _rates);
            RateSubject.NotifyObservers();
            
            return rate;
        }
    }
}
