

using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.WPF.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private static readonly TourParticipantService _tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
        private static readonly TouristService _touristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
        public TourReservationService(ITourReservationRepository tourReservationRepository)
        {
            _tourReservationRepository = tourReservationRepository;
        }

        public int NextReservationId()
        {
            return _tourReservationRepository.NextId();
        }

        public void saveReservation(TourViewModel selectedTour, int userId)
        {
            _tourReservationRepository.saveReservation(selectedTour.ToTour(), userId);
        }

        public TourParticipantViewModel FindTouristById(int touristId)
        {
            return _touristService.FindTouristById(touristId);
        }

        public TourReservationViewModel ToTouReservationViewModel(TourReservation reservation)
        {
            TourReservationViewModel tourReservationViewModel = new TourReservationViewModel();
            tourReservationViewModel.Id = reservation.Id;
            tourReservationViewModel.TourId = reservation.TourId;
            tourReservationViewModel.TouristId = reservation.TouristId;
            tourReservationViewModel.ParticipantIds = reservation.ParticipantIds;
            return tourReservationViewModel;
        }

        public TourReservationViewModel FindReservationByTOuristIdAndTourId(int userId, int tourId)
        {
            if(_tourReservationRepository.FindReservationByTouristIdAndTourId(userId, tourId) == null)
            {
                return null;
            }
            return ToTouReservationViewModel(_tourReservationRepository.FindReservationByTouristIdAndTourId(userId, tourId));
        }

        public List<TourReservation> FindReservationsByUserIdAndTourId(int tourId, int userId)
        {
            return _tourReservationRepository.FindReservationsByUserIdAndTourId(tourId, userId);
        }

        public List<Tour> FindMyTours(int reservationId, string name, string lastName)
        {
            return _tourReservationRepository.FindMyTours(reservationId, name, lastName);
        }

        public List<Tour> FindMyEndedTours(int reservationId, string name, string lastName)
        {
            return _tourReservationRepository.FindMyEndedTours(reservationId, name, lastName);
        }

        public void addParticipant(TourParticipantViewModel tourParticipantViewModel, TourReservationViewModel reservation)
        {
            _tourReservationRepository.addParticipant(tourParticipantViewModel.ToTourParticipant(), reservation.ToTourReservation());
        }
        
        public List<TourReservation> GetReservationsByTour(int tour_id)
        {
            return _tourReservationRepository.GetReservationsByTour(tour_id);
        }

        public List<TourParticipant> GetJoinedParticipantsByTour(int tour_id)
        {
            List<TourReservation> tourReservations = GetReservationsByTour(tour_id);
            List<TourParticipant> participants = new List<TourParticipant>();
            foreach (TourReservation tourReservation in tourReservations.ToList())
            {
                foreach (TourParticipant participant in _tourParticipantService.GetAllJoinedParticipantsByReservation(tourReservation.Id))
                {
                    participants.Add(participant);
                }
            }
            return participants;
        }

        public int GetNumberOfJoinedParticipants(int tour_id)
        {
            List<TourParticipant> tourParticipants = GetJoinedParticipantsByTour(tour_id);
            return tourParticipants.Count();
        }

        public int getTouristParticipantID(int tour_id)
        {
            return _tourReservationRepository.getTouristParticipantID(tour_id);
        }

    }
}
