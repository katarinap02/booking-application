using BookingApp.WPF.ViewModel;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Domain.Model.Rates;
using BookingApp.Repository.RateRepository;

namespace BookingApp.Application.Services.ReservationServices
{
    public class DelayRequestService
    {
        private readonly IDelayRequestRepository DelayRequestRepository;
        private readonly IAccommodationReservationRepository AccommodationReservationRepository;

        public DelayRequestService(IDelayRequestRepository delayRequest)
        {
            this.DelayRequestRepository = delayRequest;
            AccommodationReservationRepository = new AccommodationReservationRepository();
        }

        public List<DelayRequest> GetAll()
        {
            return DelayRequestRepository.GetAll();
        }

        public DelayRequest Add(DelayRequest delayRequest)
        {
            return DelayRequestRepository.Add(delayRequest);
        }

        public void Delete(DelayRequest delayRequest)
        {
            DelayRequestRepository.Delete(delayRequest);
        }

        public DelayRequest Update(DelayRequest delayRequest)
        {
            return DelayRequestRepository.Update(delayRequest);
        }

        public int GetNumOfDelaysByYear(int accId, int year)
        {
            int num = 0;
            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.RepliedDate.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public int GetNumOfDelaysByMonth(int accId, int month, int year)
        {
            int num = 0;
            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.RepliedDate.Month == month && ar.RepliedDate.Year == year)
                {
                    num++;
                }
            }

            return num;
        }

        public List<int> GetAllYearsForAcc(int accId)
        {
            HashSet<int> uniqueYears = new HashSet<int>(); 

            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId)
                {
                    uniqueYears.Add(ar.RepliedDate.Year); 
                }
            }
            return uniqueYears.ToList();
        }

        public List<int> GetAllMonthsForAcc(int accId, int year)
        {
            HashSet<int> uniqueMonths = new HashSet<int>(); // Using HashSet to store unique years

            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.RepliedDate.Year == year)
                {
                    uniqueMonths.Add(ar.RepliedDate.Month); // Add the year to the HashSet
                }
            }


            return uniqueMonths.ToList();
        }

        public List<int> GetAllDelaysForYears(int accId)
        {
            List<int> list = new List<int>();
            foreach (int year in GetAllYearsForAcc(accId))
            {
                list.Add(GetNumOfDelaysByYear(accId, year));
            }
            return list;
        }

        public List<int> GetAllDelaysForMonths(int accId, int year)
        {
            List<int> list = new List<int>();
            foreach (int month in GetAllMonthsForAcc(accId, year))
            {
                list.Add(GetNumOfDelaysByMonth(accId, month, year));
            }
            return list;
        }


    }
}
