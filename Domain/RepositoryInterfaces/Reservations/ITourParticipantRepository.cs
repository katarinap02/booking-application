using BookingApp.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface ITourParticipantRepository
    {
        List<TourParticipant> GetAll();
        void Add(TourParticipant tourParticipant);
        int NextId();
        TourParticipant GetById(int id);
        TourParticipant SaveParticipant(string name, string lastName, int age);
        void SaveParticipant(TourParticipant tourParticipant, int reservationId);
        List<int> GetAllIdsByReservation(int reservationId);
        List<TourParticipant> GetAllParticipantsByReservation(int reservationId);
        List<TourParticipant> GetAllNotJoinedParticipantsByReservation(int reservationId);
        bool IsUserJoined(int reservationId, string touristName, string touristLastName);
        void Update(TourParticipant tourParticipant);
    }
}
