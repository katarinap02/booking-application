using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services.FeatureServices;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourReservationService
    {
        private static readonly TourReservationRepository _tourReservationRepository = new TourReservationRepository();
        private static readonly TourParticipantService _tourParticipantService = new TourParticipantService();
        private readonly TouristService _touristService;
        public TourReservationService()
        {
            _touristService = new TouristService();
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
