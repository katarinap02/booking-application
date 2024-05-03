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

        public int getNumOfDelaysByYear(int accId, int year)
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

        public int getNumOfnDelaysByMonth(int accId, int month)
        {
            int num = 0;
            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId && ar.RepliedDate.Month == month)
                {
                    num++;
                }
            }

            return num;
        }

        public List<int> getAllYearsForAcc(int accId)
        {
            List<int> list = new List<int>();
            foreach (DelayRequest ar in DelayRequestRepository.GetAll())
            {
                AccommodationReservation arr = AccommodationReservationRepository.GetById(ar.ReservationId);
                if (arr.AccommodationId == accId)
                {
                    list.Add(ar.RepliedDate.Year);
                }
            }

            return list;
        }


    }
}
