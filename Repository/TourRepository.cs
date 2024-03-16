using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        //private GuidedTourRepository GuidedTourRepository;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            //GuidedTourRepository = new GuidedTourRepository();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(Tour tour) 
        {
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }


        public int NextPersonalId() {
            List<int> personalIds = new List<int>();
            foreach (Tour tour in _tours)
            {
                personalIds.Add(tour.Id);
            }

            int max = 0;
            if(personalIds.Count == 0) {
                max = -1;
            }
            else
            {
                max = personalIds.Max();
            }

            return max + 1;
        }

        public int NextId() //generise ID za novu grupu tura
        {
            List<int> groupIds = new List<int>();
            foreach(Tour tour in _tours)
            {
                groupIds.Add(tour.GroupId);
            }
            int max = 0;
            if (groupIds.Count == 0)
            {
                max = -1;
            }
            else
            {
                max = groupIds.Max();
            }
            return max + 1 ;
        }

        public List<Tour>? SearchTours(Tour Tour)
        {
            List<Tour> filteredTours = GetAll();
            string lowercaseCountry = Tour.Country.ToLower();
            string lowecaseCity = Tour.City.ToLower();
            string lowercaseLanguage = Tour.Language.ToLower();
            
            if(!string.IsNullOrEmpty(lowercaseCountry))
            {
                filteredTours = _tours.FindAll(tour => tour.Country.ToLower().Contains(lowercaseCountry));
            }
            if(!string.IsNullOrEmpty(lowecaseCity))
            {
                filteredTours = filteredTours.Where(tour => tour.City.ToLower().Contains(lowecaseCity)).ToList();
            }
            if(Tour.Duration != 0)
            {
                filteredTours = filteredTours.Where(tour => tour.Duration == Tour.Duration).ToList();
            }
            if (!string.IsNullOrEmpty(lowercaseLanguage))
            {
            filteredTours = filteredTours.Where(tour => tour.Language.ToLower().Contains(lowercaseLanguage)).ToList();
            }
            if(Tour.AvailablePlaces != 0)
            {
                filteredTours = filteredTours.Where(tour => tour.MaxTourists >= Tour.AvailablePlaces).ToList();
            }
            return filteredTours;
        }

        public List<Tour> GetTourByCountryWithAvailablePlaces(string country)
        {
            return _tours.FindAll(tour => tour.Country.ToLower().Equals(country.ToLower())).Where(tour => tour.AvailablePlaces > 0).ToList();
        }

        public List<Tour>? findToursNeedingGuide() 
        {
            List<Tour> allTours = GetAll();
            List<Tour> ret = new List<Tour>();
            foreach(Tour tour in allTours)
            {

                /*
                MessageBox.Show("tour.Date.Date: " + tour.Date.Date+"              "+ "DateTime.Now.Date: " + DateTime.Now.Date);
                MessageBox.Show((tour.Date.Date == DateTime.Now.Date).ToString());
                */
                if (tour.Date.Date == DateTime.Now.Date && tour.Status==TourStatus.inPreparation)
                {
                    ret.Add(tour);
                }
            }
            return ret;
        }

        public Tour? UpdateAvailablePlaces(Tour tour, int reducer)
        {
            Tour? oldTour = GetTourById(tour.Id);
            if (oldTour == null)
                return null;

            oldTour.AvailablePlaces -= reducer;
            _serializer.ToCSV(FilePath, _tours);
            return oldTour;
        }

        public Tour? GetTourById(int id)
        {
            return _tours.Find(t => t.Id == id);
        }

        public int ToursCount()
        {
            return _tours.Count();
        }

        public int FindMaxNumberOfParticipants()
        {
            List<Tour> allTours = GetAll();
            int maxTourists = allTours[0].MaxTourists;
            foreach(Tour tour in allTours)
            {
                if(tour.MaxTourists > maxTourists)
                {
                    maxTourists = tour.MaxTourists;
                }
            }

            return maxTourists;
        }

        public void bindGuideAndTour(Tour tour, User guide)
        {
            tour.Status = TourStatus.gotGuide;
            //GuidedTourRepository.Add(guide, tour);
        }

    }
}
