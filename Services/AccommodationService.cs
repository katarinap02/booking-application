using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationService
    {
        private readonly AccommodationRepository AccommodationRepository;
        
        public AccommodationService()
        {
            AccommodationRepository = new AccommodationRepository();
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationRepository.GetAll();
        }

        public Accommodation Add(Accommodation accommodation)
        {
            return AccommodationRepository.Add(accommodation);
        }

        public void Delete(Accommodation accommodation) { 
        
             AccommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return AccommodationRepository.Update(accommodation);
        }

        public Accommodation GetById(int id)
        {
            return AccommodationRepository.GetById(id);
        }

        
    }
}
