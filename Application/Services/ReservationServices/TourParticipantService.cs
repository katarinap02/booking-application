using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class TourParticipantService
    {
        private readonly TourParticipantRepository _tourParticipantRepository;

        public TourParticipantService()
        {
            _tourParticipantRepository = new TourParticipantRepository();
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
    }
}
