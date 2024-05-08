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

        public int GetNumOfCancelationByYear(int accId, int year)
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

        public int GetNumOfCancellationsByMonth(int accId, int month, int year)
        {
            int num = 0;
            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.CancellationDate.Month == month && ar.CancellationDate.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public List<int> GetAllYearsForAcc(int accId)
        {
            HashSet<int> uniqueYears = new HashSet<int>(); // Using HashSet to store unique years

            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId)
                {
                    uniqueYears.Add(ar.CancellationDate.Year); // Add the year to the HashSet
                }
            }

            return uniqueYears.ToList();
        }

        public List<int> GetAllMonthsForAcc(int accId, int year)
        {
            HashSet<int> uniqueMonths = new HashSet<int>(); // Using HashSet to store unique years

            foreach (ReservationCancellation ar in ReservationCancellationRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.CancellationDate.Year == year)
                {
                    uniqueMonths.Add(ar.CancellationDate.Month); // Add the year to the HashSet
                }
            }


            return uniqueMonths.ToList();
        }


        public List<int> GetAllCancellationsForYears(int accId)
        {
            List<int> list = new List<int>();
            foreach (int year in GetAllYearsForAcc(accId))
            {
                list.Add(GetNumOfCancelationByYear(accId, year));
            }
            return list;
        }

        public List<int> GetAllCancellationsForMonths(int accId, int year)
        {
            List<int> list = new List<int>();
            foreach (int month in GetAllMonthsForAcc(accId, year))
            {
                list.Add(GetNumOfCancellationsByMonth(accId, month, year));
            }
            return list;
        }
    }
}
