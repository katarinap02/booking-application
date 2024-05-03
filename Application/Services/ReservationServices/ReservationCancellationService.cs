using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class ReservationCancellationService
    {
        private readonly IReservationCancellationRepository ReservationCancellationRepository;
        private readonly IAccommodationReservationRepository AccommodationReservationRepository;

        public ReservationCancellationService(IReservationCancellationRepository reservationCancellationRepository)
        {
            ReservationCancellationRepository = reservationCancellationRepository;
            AccommodationReservationRepository = new AccommodationReservationRepository();
        }

        public List<ReservationCancellation> GetAll()
        {
            return ReservationCancellationRepository.GetAll();
        }

        public ReservationCancellation Add(ReservationCancellation cancellation)
        {
            return ReservationCancellationRepository.Add(cancellation);
        }

        public int getNumOfCancelationByYear(int accId, int year)
        {
            int num = 0;
            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.CancellationDate.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public int getNumOfnCancellationsByMonth(int accId, int month)
        {
            int num = 0;
            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.CancellationDate.Month == month)
                {
                    num++;
                }
            }

            return num;
        }

        public List<int> getAllYearsForAcc(int accId)
        {
            List<int> list = new List<int>();
            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId)
                {
                    list.Add(ar.CancellationDate.Year);
                }
            }

            return list;
        }
    }
}
