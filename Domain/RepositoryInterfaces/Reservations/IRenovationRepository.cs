using BookingApp.Domain.Model.Reservations;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface IRenovationRepository
    {
        List<Renovation> GetAll();

        Renovation Add(Renovation reservation);

        int NextId();

        public void Delete(Renovation selectedReservation);
    }
}
