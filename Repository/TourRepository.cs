using BookingApp.ViewModel;
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

        private readonly TourReservationRepository _reservationRepository;

        //private GuidedTourRepository GuidedTourRepository;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            //GuidedTourRepository = new GuidedTourRepository();
            _tours = _serializer.FromCSV(FilePath);
            _reservationRepository = new TourReservationRepository();
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath).FindAll(t => t.Status != TourStatus.Finnished);
        }

        public void Add(Tour tour)
        {
            _tours.Add(tour);
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

        public int NextId() //generise ID za novu grupu tura
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
            List<Tour> filteredTours = GetAll();

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
            return _tours.FindAll(tour => tour.City.ToLower().Equals(city.ToLower())).Where(tour => tour.AvailablePlaces > 0).ToList();
        }

        public List<Tour>? findToursNeedingGuide()
        {
            List<Tour> allTours = GetAll();
            List<Tour> ret = new List<Tour>();
            foreach (Tour tour in allTours)
            {
                if (tour.Date.Date == DateTime.Now.Date && tour.Status == TourStatus.inPreparation)
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
            if(allTours.Count != 0)
            {
                int maxTourists = allTours[0].MaxTourists;
                foreach (Tour tour in allTours)
                {
                    if (tour.MaxTourists > maxTourists)
                    {
                        maxTourists = tour.MaxTourists;
                    }
                }
                return maxTourists;
            }
            return 0;
        }

        public void bindGuideAndTour(Tour tour, User guide)
        {
            tour.Status = TourStatus.gotGuide;
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

        public void nextCheckpoint(int id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.currentCheckpoint++;
            _serializer.ToCSV(FilePath, _tours);
        }
        public List<Tour> findToursByGuideId(int guideId)
        {
            List<Tour> tours = GetAll();
            foreach(Tour tour in tours.ToList())
            {
                TimeSpan difference = DateTime.Now - tour.Date;
                /*
                if (tour.GuideId != guideId)
                {
                    tours.Remove(tour);
                }*/
            }
            return tours;
        }

        public List<Tour> findToursToCancel(int guideId)
        {
            List<Tour> tours = findToursByGuideId(guideId);
            foreach (Tour tour in tours.ToList())
            {
                TimeSpan difference = DateTime.Now - tour.Date;
                if (difference.TotalHours <= 48 || tour.Status != TourStatus.inPreparation)
                {
                    tours.Remove(tour);
                }
            }
            return tours;
        }

        public void cancelTour(int id)
        {
            Tour tour = GetTourById(id);
            if (tour == null) return;
            tour.Status = TourStatus.Canceled;
            //dodeli vaucer
            _serializer.ToCSV(FilePath, _tours);
        }

        public List<int> GetAgeStatistic(int id)
        {
            int below18 = 0;
            int above18 = 0;
            int above50 = 0;
            List<TourParticipant> participants = _reservationRepository.GetJoinedParticipantsByTour(id);

            foreach (TourParticipant participant in participants)
            {
                if(participant.Years < 18)
                {
                    below18++;
                }
                else if (participant.Years < 50)
                {
                    above18++;
                }
                else
                {
                    above50++;
                }
            }
            return new List<int> { below18, above18, above50 };

        }       

        public int GetMaximumParticipantsForGuide(int guide_id)
        {
            List<Tour> tours = findToursByGuideId(guide_id);
            List<int> participantNumber = new List<int>();
            foreach(Tour tour in tours)
            {
                participantNumber.Add(_reservationRepository.GetNumberOfJoinedParticipants(tour.Id));
            }

            return participantNumber.Max();
        }

    }
}
