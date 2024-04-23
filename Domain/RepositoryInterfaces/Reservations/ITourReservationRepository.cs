using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface ITourReservationRepository
    {
        List<TourReservation> GetAll();
        void Add(TourReservation tourReservation);
        int NextId();
        List<TourReservation> GetReservationsByTour(int tour_id);
        List<TourParticipant> GetNotJoinedReservations(int tour_id);
        TourReservation GetById(int reservation_id);
        void saveReservation(Tour selectedTour, int userId);
        List<Tour> FindMyTours(int id, string touristName, string touristLastName);
        List<Tour> FindToursForUserByReservation(int id, string touristName, string touristLastName);
        List<Tour> FindMyEndedTours(int id, string touristName, string touristLastName);
        List<TourReservation> FindReservationsByUserIdAndTourId(int tourId, int userId);
        int getTouristParticipantID(int tour_id);
        TourReservation FindReservationByTouristIdAndTourId(int userId, int tourId);
        void addParticipant(TourParticipant participant, TourReservation reservation);
    }
}
