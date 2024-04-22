
﻿using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.WPF.ViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TouristService
    {
        private readonly ITouristRepository _touristRepository;

        public TouristService(ITouristRepository touristRepository)
        {
            _touristRepository = touristRepository;
        }

        public TourParticipantViewModel ToTourParticipantViewModel(Tourist tourist)
        {
            TourParticipantViewModel viewModel = new TourParticipantViewModel();
            viewModel.Name = tourist.Name;
            viewModel.LastName = tourist.LastName;
            viewModel.Years = tourist.Age;
            return viewModel;
        }

        public TourParticipantViewModel FindTouristById(int touristId)
        {
            return ToTourParticipantViewModel(_touristRepository.FindTouristById(touristId));
        }
        public Tourist GetTouristById(int touristId)
        {

            return _touristRepository.FindTouristById(touristId);

           // return tourRepository.FindMaxNumberOfParticipants(ToTour(tours));
        }

        /*public List<TourViewModel> ToTourViewModel(List<Tour> Tours)
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

        }*/
    }
}
