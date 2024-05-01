using BookingApp.Domain.Model;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class TourParticipantService
    {
        private readonly ITourParticipantRepository _tourParticipantRepository;
        private readonly TourReservationService _tourReservationService;

        public TourParticipantService(ITourParticipantRepository tourParticipantRepository)
        {
            _tourParticipantRepository = tourParticipantRepository;
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
        }

        public TourParticipantViewModel saveParticipantToDTO(string name, string lastName, int age)
        {
            TourParticipantViewModel tourParticipantViewModel = new TourParticipantViewModel(_tourParticipantRepository.SaveParticipant(name, lastName, age));
            return tourParticipantViewModel;
        }

        public void saveParticipant(TourParticipantViewModel tourParticipantViewModel, int reservationId)
        {
            _tourParticipantRepository.SaveParticipant(tourParticipantViewModel.ToTourParticipant(), reservationId);
        }

        public bool IsUserJoined(int reservationId, string touristName, string touristLastName)
        {
            return _tourParticipantRepository.IsUserJoined(reservationId, touristName, touristLastName);
        }

        public List<TourParticipant> GetAllJoinedParticipantsByReservation(int reservationId)
        {
            List<TourParticipant> tourParticipants = _tourParticipantRepository.GetAllParticipantsByReservation(reservationId);
            foreach (TourParticipant tp in tourParticipants.ToList())
            {
                if (!tp.HasJoinedTour)
                {
                    tourParticipants.Remove(tp);
                }
            }

            return tourParticipants;
        }

        public TourParticipant GetById(int id)
        {
            return _tourParticipantRepository.GetById(id);
        }


        public int getjoinedCheckpoint(int tour_id)
        {
            TourParticipant participant = GetById(_tourReservationService.getTouristParticipantID(tour_id));
            if (!participant.HasJoinedTour)
            {
                return -1;
            }
            return participant.JoinedCheckpointIndex;
        }
    }
}
