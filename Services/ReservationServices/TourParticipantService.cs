using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourParticipantService
    {
        private readonly TourParticipantRepository _tourParticipantRepository;
        private readonly TourReservationService _tourReservationService;

        public TourParticipantService()
        {
            _tourParticipantRepository = new TourParticipantRepository();
            _tourReservationService = new TourReservationService();
        }

        public TourParticipantViewModel saveParticipantToDTO(string name, string lastName, string years)
        {
            TourParticipantViewModel tourParticipantViewModel = new TourParticipantViewModel(_tourParticipantRepository.SaveParticipant(name, lastName, years));
            return tourParticipantViewModel;
        }

        public void saveParticipant(TourParticipantViewModel tourParticipantViewModel, int reservationId)
        {
            _tourParticipantRepository.SaveParticipant(tourParticipantViewModel.ToTourParticipant(), reservationId);
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
