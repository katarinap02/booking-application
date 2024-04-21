using BookingApp.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface IReservationCancellationRepository
    {
        List<ReservationCancellation> GetAll();
        ReservationCancellation Add(ReservationCancellation cancellation);
    }
}
