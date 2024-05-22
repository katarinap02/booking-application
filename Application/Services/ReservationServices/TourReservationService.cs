

using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
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
        private static readonly TourService _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
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

        public Tourist FindTouristById(int touristId)
        {
            return _touristService.FindTouristById(touristId);
        }

        public TourReservation FindReservationByTOuristIdAndTourId(int userId, int tourId)
        {
            if(_tourReservationRepository.FindReservationByTouristIdAndTourId(userId, tourId) == null)
            {
                return null;
            }
            return _tourReservationRepository.FindReservationByTouristIdAndTourId(userId, tourId);
        }

        public List<TourReservation> FindReservationsByUserIdAndTourId(int tourId, int userId)
        {
            return _tourReservationRepository.FindReservationsByUserIdAndTourId(tourId, userId);
        }

        public List<Tour> FindMyTours(int touristId, string touristName, string touristLastName)
        {
            List<TourReservation> tourReservations = _tourReservationRepository.FindReservationsByTouristId(touristId);

            List<Tour> tours = FindToursForTouristWhereHeJoined(tourReservations, touristName, touristLastName);
            return tours.FindAll(t => t.Status != TourStatus.Finnished);
        }

        public List<Tour> FindMyEndedTours(int touristId, string touristName, string touristLastName)
        {
            List<TourReservation> tourReservations = _tourReservationRepository.FindReservationsByTouristId(touristId);

            List<Tour> tours = FindToursForTouristWhereHeJoined(tourReservations, touristName, touristLastName);
            return tours.FindAll(t => t.Status == TourStatus.Finnished);
        }

        public List<Tour> FindToursForTouristWhereHeJoined(List<TourReservation> tourReservations, string touristName, string touristLastName)
        {
            List<Tour> tours = new List<Tour>();

            foreach (TourReservation tourReservation in tourReservations)
            {

                // daj mi turu koja je sa tim tourId-jem, ali mora da se user prikljucio turi
                if (_tourService.GetTourById(tourReservation.TourId) != null && _tourParticipantService.IsUserJoined(tourReservation.Id, touristName, touristLastName))
                {
                    tours.Add(_tourService.GetTourById(tourReservation.TourId));
                }
            }
            return tours;
        }

        public TourReservation GetById(int id)
        {
            return _tourReservationRepository.GetById(id);
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

        public List<TourParticipant> GetNotJoinedParticipants(int tour_id)
        {
            return _tourReservationRepository.GetNotJoinedReservations(tour_id);
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
