using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.ViewModel;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TouristService
    {
        private readonly TourRepository tourRepository;
        private readonly TourParticipantRepository tourparticipantRepository;
        private readonly TourReservationRepository tourReservationRepository;

        public TouristService()
        {
            tourRepository = new TourRepository();
            tourparticipantRepository = new TourParticipantRepository();
            tourReservationRepository = new TourReservationRepository();
        }

        public List<TourViewModel> GetAllTours()
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
            foreach (TourViewModel tourViewModel in toursViewModel)
            {
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
            return ToTourViewModel(tourReservationRepository.FindMyTours(id));
        }

        public List<TourViewModel> FindMyEndedTours(int id)
        {
            return ToTourViewModel(tourReservationRepository.FindMyEndedTours(id));
        }


        public List<string> GetParticipantsThatJoinedNow(TouristNotification notification)
        {
            // moram naci rezervacije
            List<TourReservation> reservations = tourReservationRepository.FindReservationsByUserIdAndTourId(notification.TourId, notification.TouristId);

            List<string> nowJoinedParticipantNames = new List<string>();
            foreach (var reservation in reservations)
            {
                List<TourParticipant> joinedTourParticipants = tourparticipantRepository.GetAllJoinedParticipantsByReservation(reservation.Id);
                foreach (var participant in joinedTourParticipants)
                {
                    // ako se sad prikljucio onda cemo prikazati u notifitikaciji
                    if (participant.JoinedCheckpointIndex == notification.CurrentCheckpoint)
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
    }
}
