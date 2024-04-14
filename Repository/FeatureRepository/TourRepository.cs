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
            return _tours.FindAll(tour => tour.City.ToLower().Equals(city.ToLower()) && tour.Status != TourStatus.Finnished).Where(tour => tour.AvailablePlaces > 0).ToList();
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
            List<Tour> tours = _serializer.FromCSV(FilePath);
            return tours.FindAll(t => t.GuideId == guideId);
        }

        public List<Tour> findToursToCancel(int guideId)
        {
            List<Tour> tours = findToursByGuideId(guideId);
            foreach (Tour tour in tours.ToList())
            {
                TimeSpan difference = tour.Date - DateTime.Now;
                if (difference.TotalHours <= 48 || tour.Status != TourStatus.inPreparation || tour.Date < DateTime.Now)
                {
                    tours.Remove(tour);
                }
            }
            return tours;
        }

        public void grantVoucher(string reason, DateOnly expireDate, int tour_id, int guide_id)
        {
            List<TourReservation> Reservations = _reservationRepository.GetReservationsByTour(tour_id);
            foreach (TourReservation reservation in Reservations)
            {
                Voucher voucher = new Voucher(0, reservation.TouristId, guide_id, false, reason, expireDate);
                _voucherRepository.Add(voucher);
            }
        }

        public void cancelTour(int tour_id, int guide_id)
        {
            Tour tour = GetTourById(tour_id);
            if (tour == null) return;
            tour.Status = TourStatus.Canceled;

            string reason = "Tour " + tour.Name + " has been canceled";
            DateOnly expireDate = new DateOnly(DateTime.Now.Year+1, DateTime.Now.Month, DateTime.Now.Day);
            grantVoucher(reason, expireDate, tour_id, guide_id);
            MessageBox.Show(reason);
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
        
        public Tour GetMostPopularTourForGuide(int guide_id)
        {
            List<Tour> tours = findToursByGuideId(guide_id);
            Tour mostPopularTour = null;
            int maxParticipants = 0;

            foreach (Tour tour in tours)
            {
                int numberOfParticipants = _reservationRepository.GetNumberOfJoinedParticipants(tour.Id);

                if (numberOfParticipants > maxParticipants)
                {
                    maxParticipants = numberOfParticipants;
                    mostPopularTour = tour;
                }
            }

            return mostPopularTour;
        }

        public Tour GetMostPopularTourForGuideInYear(int guide_id, int year)
        {
            List<Tour> tours = findToursByGuideId(guide_id);

            Tour mostPopularTourInYear = null;
            int maxParticipantsInYear = 0;

            foreach (Tour tour in tours)
            {
                if (tour.Date.Year == year)
                {
                    int numberOfParticipants = _reservationRepository.GetNumberOfJoinedParticipants(tour.Id);

                    if (numberOfParticipants > maxParticipantsInYear)
                    {
                        maxParticipantsInYear = numberOfParticipants;
                        mostPopularTourInYear = tour;
                    }
                }
            }

            return mostPopularTourInYear;
        }

        public List<Tour> findFinnishedToursByGuide(int guide_id)
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
    }
}
