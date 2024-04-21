using BookingApp.Application.Services.ReservationServices;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.Services.RateServices
{
    public class AccommodationRateService
    {
        private readonly AccommodationRateRepository AccommodationRateRepository;
        private AccommodationReservationService AccommodationReservationService;
        public AccommodationRateService()
        {
            AccommodationRateRepository = new AccommodationRateRepository();

        }

        public List<AccommodationRate> GetAll()
        {
            return AccommodationRateRepository.GetAll();
        }

        public AccommodationRate Add(AccommodationRate rate)
        {
            return AccommodationRateRepository.Add(rate);
        }

        public double GetAverageRate(int accommodationId)
        {
            AccommodationReservationService = new AccommodationReservationService();
            double sum = 0;
            double counter = 0;
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                foreach (AccommodationRate rate in AccommodationRateRepository.GetAll())
                {
                    if (rate.ReservationId == reservation.Id)
                    {
                        if (reservation.AccommodationId == accommodationId)
                        {
                            sum += rate.Correctness + rate.Cleanliness;
                            counter += 2;

                        }
                    }
                }
            }

            double average = sum / counter;

            if (average > 0)
                return average;
            else return 0;




        }


    }
}
