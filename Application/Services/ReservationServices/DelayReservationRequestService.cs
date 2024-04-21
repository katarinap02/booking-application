using BookingApp.WPF.ViewModel;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.Reservations;

namespace BookingApp.Application.Services.ReservationServices
{
    public class DelayRequestService
    {
        private readonly DelayRequestRepository DelayRequestRepository;

        public DelayRequestService()
        {
            DelayRequestRepository = new DelayRequestRepository();
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


    }
}
