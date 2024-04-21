using BookingApp.WPF.ViewModel;
using BookingApp.Domain.Model;
using BookingApp.Application.Services;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Application.Services;
using BookingApp.Domain.Model.Features;
using BookingApp.Serializer;
using System.Windows;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Reservations;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TourService
    {
        private readonly TourRepository tourRepository;
        private readonly TourParticipantRepository tourparticipantRepository;
        private readonly TourReservationRepository tourReservationRepository;
        private readonly VoucherService voucherService;
        private readonly TourReservationService tourReservationService;

        private TouristService touristService;
        public TourService()
        {
            tourRepository = new TourRepository();
            tourparticipantRepository = new TourParticipantRepository();
            tourReservationRepository = new TourReservationRepository();
            voucherService = new VoucherService();
            touristService = new TouristService();
            tourReservationService = new TourReservationService();
        }

        public List<TourViewModel> GetAllTours() // izmeniti posto ne vraca sve vec samo one koje nisu zavrsene! BITNO!!!
        {
            return ToTourViewModel(tourRepository.GetAll());
        }

        public int FindMaxNumberOfParticipants()
        {
            return tourRepository.FindMaxNumberOfParticipants();
        }

        public int FindMaxNumberOfParticipants(List<TourViewModel> tours)
        {
            return tourRepository.FindMaxNumberOfParticipants(ToTour(tours));
        }

        public List<TourViewModel> ToTourViewModel(List<Tour> Tours)
        {
            // creating list from Tour to TourViewModel
            List<TourViewModel> ToursViewModel = new List<TourViewModel>();
            foreach (Tour tour in Tours)
            {
                ToursViewModel.Add(new TourViewModel(tour));
            }
            return ToursViewModel;
        }

        public List<Tour> ToTour(List<TourViewModel> toursViewModel)
        {
            List<Tour> tours = new List<Tour>();
            foreach(TourViewModel tourViewModel in toursViewModel){
                tours.Add(tourViewModel.ToTour());
            }
            return tours;
        }

        public List<TourViewModel>? SearchTours(Tour searchCriteria)
        {
            return ToTourViewModel(tourRepository.SearchTours(searchCriteria));
        }

        public int ToursCount()
        {
            return tourRepository.ToursCount();
        }

        public Tour UpdateAvailablePlaces(TourViewModel tour, int reducer)
        {
            return tourRepository.UpdateAvailablePlaces(tour.ToTour(), reducer);
        }

        public List<TourViewModel> FindMyTours(int id)
        {
            Tourist tourist = touristService.GetTouristById(id);
            return ToTourViewModel(tourReservationRepository.FindMyTours(id, tourist.Name, tourist.LastName));
        }

        public List<TourViewModel> FindMyEndedTours(int id)
        {
            Tourist tourist = touristService.GetTouristById(id);
            return ToTourViewModel(tourReservationRepository.FindMyEndedTours(id, tourist.Name, tourist.LastName));
        }


        public List<string> GetParticipantsThatJoinedNow(TouristNotification notification)
        {
            // moram naci rezervacije
            List<TourReservation> reservations = tourReservationRepository.FindReservationsByUserIdAndTourId(notification.TourId, notification.TouristId);

            List<string> nowJoinedParticipantNames = new List<string>();
            foreach (var reservation in reservations)
            {
                List<TourParticipant> joinedTourParticipants = tourparticipantRepository.GetAllJoinedParticipantsByReservation(reservation.Id);
                foreach(var participant in joinedTourParticipants)
                {
                    // ako se sad prikljucio onda cemo prikazati u notifitikaciji
                    if(participant.JoinedCheckpointIndex == notification.CurrentCheckpoint)
                    {
                        nowJoinedParticipantNames.Add(participant.LastName + " " + participant.Name);
                    }
                }
            }
            return nowJoinedParticipantNames;
        }

        public List<TourViewModel> GetTourByCityWithAvailablePlaces(string city)
        {
            return ToTourViewModel(tourRepository.GetTourByCityWithAvailablePlaces(city));
        }

        public List<string> GetCheckpointsByTour(int tourId)
        {
            return tourRepository.GetCheckpointsByTour(tourId);
        }

        public List<Tour> findToursToCancel(int guideId) //izmeniti referencu!
        {
            List<Tour> tours = tourRepository.findToursByGuideId(guideId);
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
            List<TourReservation> Reservations = tourReservationRepository.GetReservationsByTour(tour_id);
            foreach (TourReservation reservation in Reservations)
            {
                Voucher voucher = new Voucher(0, reservation.TouristId, guide_id, false, reason, expireDate);
                voucherService.Add(voucher);
            }
        }

        public void cancelTour(int tour_id, int guide_id)
        {
            Tour tour = tourRepository.GetTourById(tour_id);
            if (tour == null) return;
            tour.Status = TourStatus.Canceled;

            string reason = "Tour " + tour.Name + " has been canceled";
            DateOnly expireDate = new DateOnly(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day);
            grantVoucher(reason, expireDate, tour_id, guide_id);
            MessageBox.Show(reason);
            tourRepository.save(tour);
        }

        public List<int> GetAgeStatistic(int id)
        {
            int below18 = 0;
            int above18 = 0;
            int above50 = 0;
            List<TourParticipant> participants = tourReservationService.GetJoinedParticipantsByTour(id);

            foreach (TourParticipant participant in participants)
            {
                if (participant.Years < 18)
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
            List<Tour> tours = tourRepository.findToursByGuideId(guide_id);
            Tour mostPopularTour = null;
            int maxParticipants = 0;

            foreach (Tour tour in tours)
            {
                int numberOfParticipants = tourReservationService.GetNumberOfJoinedParticipants(tour.Id);

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
            List<Tour> tours = tourRepository.findToursByGuideId(guide_id);

            Tour mostPopularTourInYear = null;
            int maxParticipantsInYear = 0;

            foreach (Tour tour in tours)
            {
                if (tour.Date.Year == year)
                {
                    int numberOfParticipants = tourReservationService.GetNumberOfJoinedParticipants(tour.Id);

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
            return tourRepository.findFinnishedToursByGuide(guide_id);
        }

    }
}
