using BookingApp.Repository;
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

        public TourReservationService()
        {
            _tourReservationRepository = new TourReservationRepository();
        }

        public int NextReservationId()
        {
            return _tourReservationRepository.NextId();
        }

        public void saveReservation(TourReservationViewModel tourReservationViewModel, TourViewModel selectedTour, int userId)
        {
            _tourReservationRepository.saveReservation(tourReservationViewModel.ToTourReservation(), selectedTour.ToTour(), userId);
        }
    }
}
