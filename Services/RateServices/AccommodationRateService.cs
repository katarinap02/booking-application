using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationRateService
    {
        private readonly AccommodationRateRepository AccommodationRateRepository;

        public AccommodationRateService()
        {
            AccommodationRateRepository = new AccommodationRateRepository();
        }

        public List<AccommodationRate> GetAll()
        {
            return AccommodationRateRepository.GetAll();
        }

        public AccommodationRate Add(AccommodationRate rate)
        {
            return AccommodationRateRepository.Add(rate);
        }

        
    }
}
