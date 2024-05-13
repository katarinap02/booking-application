using BookingApp.Domain.Model.Reservations;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        List<AccommodationReservation> GetGuestForRate();
        bool Rated(AccommodationReservation ar);

        AccommodationReservation Add(AccommodationReservation ar);
        int NextId();
        AccommodationReservation Update(AccommodationReservation ar);
        void Delete(AccommodationReservationViewModel selectedReservation);
        AccommodationReservation GetById(int id);
    }
}
