using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ReservationCancellationService
    {
        private readonly ReservationCancellationRepository ReservationCancellationRepository;

        public ReservationCancellationService()
        {
            ReservationCancellationRepository = new ReservationCancellationRepository();
        }

        public List<ReservationCancellation> GetAll()
        {
            return ReservationCancellationRepository.GetAll();
        }

        public ReservationCancellation Add(ReservationCancellation cancellation)
        {
            return ReservationCancellationRepository.Add(cancellation);
        }
    }
}
