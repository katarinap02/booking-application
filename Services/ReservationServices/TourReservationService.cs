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
        private readonly TourReservationRepository _tourReservationRepository;

        private readonly TouristService _touristService;
        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();

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
    }
}
