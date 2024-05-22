using BookingApp.WPF.ViewModel;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository.ReservationRepository;
using System.Net;

namespace BookingApp.Repository.FeatureRepository
{
    public class TourRepository : ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        private readonly TourReservationRepository _reservationRepository;

        private readonly VoucherRepository _voucherRepository;

        //private GuidedTourRepository GuidedTourRepository;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            //GuidedTourRepository = new GuidedTourRepository();
            _tours = _serializer.FromCSV(FilePath);
            _reservationRepository = new TourReservationRepository();
            _voucherRepository = new VoucherRepository();
        }

        public List<Tour> GetAll() 
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAllNotFinishedAndNotCancelled()
        {
            return _serializer.FromCSV(FilePath).FindAll(t => t.Status != TourStatus.Finnished && t.Status != TourStatus.Canceled);
        }

        public void Add(Tour tour)
        {
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

        public void Save()
        {
            _serializer.ToCSV(FilePath, _tours);
        }

        public int NextPersonalId()
        {
            List<int> personalIds = new List<int>();
            foreach (Tour tour in _tours)
            {
                personalIds.Add(tour.Id);
            }

            int max = 0;
            if (personalIds.Count == 0)
            {
                max = -1;
            }
            else
            {
                max = personalIds.Max();
            }

            return max + 1;
        }

        public int NextId() 
        {
            List<int> groupIds = new List<int>();
            foreach (Tour tour in _tours)
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
            return max + 1;
        }

        public List<Tour>? SearchTours(Tour searchCriteria)
        {
            List<Tour> filteredTours = GetAllNotFinishedAndNotCancelled();

            if (!string.IsNullOrEmpty(searchCriteria.Country.ToLower()))
            {
                filteredTours = _tours.FindAll(tour => tour.Country.ToLower().Contains(searchCriteria.Country.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.City.ToLower()))
            {
                filteredTours = filteredTours.Where(tour => tour.City.ToLower().Contains(searchCriteria.City.ToLower())).ToList();
            }
            if (searchCriteria.Duration != 0)
            {
                filteredTours = filteredTours.Where(tour => tour.Duration == searchCriteria.Duration).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Language.ToLower()))
            {
                filteredTours = filteredTours.Where(tour => tour.Language.ToLower().Contains(searchCriteria.Language.ToLower())).ToList();
            }
            if (searchCriteria.AvailablePlaces != 0)
            {
                filteredTours = filteredTours.Where(tour => tour.MaxTourists >= searchCriteria.AvailablePlaces).ToList();
            }
            return filteredTours;
        }

        public List<Tour> GetTourByCityWithAvailablePlaces(string city)
        {
            _tours = GetAllNotFinishedAndNotCancelled();
            return _tours.FindAll(tour => tour.City.ToLower().Equals(city.ToLower()) && tour.Status != TourStatus.Finnished).Where(tour => tour.AvailablePlaces > 0).ToList();
        }

        public Tour? GetTourById(int id)
        {
            _tours = GetAll(); // ovo da bi procitao nov sadrzaj iz csv-a
            return _tours.Find(t => t.Id == id);
        }

        public int ToursCount()
        {
            return _tours.Count();
        }

        public void finnishTour(int id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.Status = TourStatus.Finnished;
            _serializer.ToCSV(FilePath, _tours);
        }

        public void activateTour(int id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.Status = TourStatus.Active;
            _serializer.ToCSV(FilePath, _tours);
        }
        
        public void activateTour(int id, int guide_id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.Status = TourStatus.Active;
            tour.GuideId = guide_id;
            _serializer.ToCSV(FilePath, _tours);
        }

        public void nextCheckpoint(int id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.currentCheckpoint++;
            _serializer.ToCSV(FilePath, _tours);
        }
        public List<Tour> findToursByGuideId(int guideId) 
        {
            List<Tour> tours = _serializer.FromCSV(FilePath);
            return tours.FindAll(t => t.GuideId == guideId);
        }
        

        public void save(Tour tour) {
            _tours = _serializer.FromCSV(FilePath);
            Tour current = _tours.Find(t => t.Id == tour.Id);
            int index = _tours.IndexOf(current);
            _tours.Remove(current);
            _tours.Insert(index, tour);
            _serializer.ToCSV(FilePath, _tours);
            // notify observers
        }        

        public List<Tour> findFinnishedToursByGuide(int guide_id) // izmeniti pozivanja
        {
            List<Tour> tours = _serializer.FromCSV(FilePath).FindAll(t => t.Status == TourStatus.Finnished && t.GuideId == guide_id);
            return tours;
        }

        public bool isTourFinished(int tourId)
        {
            var tour = GetTourById(tourId);
            if (tour != null)
            {
                if (tour.Status == TourStatus.Finnished)
                    return true;
                return false;
            }
            return false;
        }

        public List<string> GetCheckpointsByTour(int tourId)
        {
            if(_tours.Find(t => t.Id == tourId) != null)
                return _tours.Find(t => t.Id == tourId).Checkpoints;
            return null;
        }
    }
}
