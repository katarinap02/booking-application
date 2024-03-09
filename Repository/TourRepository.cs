using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        //add za sada

        public void Add(Tour tour) 
        {
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

        public int NextId() //generise ID za novu grupu tura
        {
            List<int> groupIds = new List<int>();
            foreach(Tour tour in _tours)
            {
                groupIds.Add(tour.GroupId);
            }

            return groupIds.Max()+1;
        }

        public List<Tour>? FindToursBy(string country, string city, float duration, string language, int numberOfPeople)
        {
            List<Tour> allTours = GetAll();
            string lowercaseCountry = country.ToLower();
            string lowecaseCity = city.ToLower();
            string lowercaseLanguage = language.ToLower();

            if (country != "")
            {
                allTours = _tours.FindAll(tour => tour.Country.ToLower().Contains(lowercaseCountry));
            }
            if(city != "")
            {
                allTours = allTours.Where(tour => tour.City.ToLower().Contains(lowecaseCity)).ToList();
            }
            if(duration != 0)
            {
                allTours = allTours.Where(tour => tour.Duration == duration).ToList();
            }
            if(language != "")
            {
                allTours = allTours.Where(tour => tour.Language.ToLower() == lowercaseLanguage).ToList();
            }
            if(numberOfPeople != 0)
            {
                allTours = allTours.Where(tour => tour.MaxTourists >= numberOfPeople).ToList();
            }
            return allTours;
        }
    }
}
