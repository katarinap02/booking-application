using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.Services.FeatureServices
{
    public class HostService
    {
        private readonly IHostRepository HostRepository;
        public Subject HostSubject;
        private readonly IAccommodationRateRepository AccommodationRateRepository;
        public AccommodationService AccommodationService { get; set; }

        public HostService(IAccommodationRepository accommodationRepository)
        {
            HostRepository = new HostRepository();
            HostSubject = new Subject();
            AccommodationRateRepository = new AccommodationRateRepository();
            AccommodationService = new AccommodationService(accommodationRepository);
        }

        public HostService(IHostRepository hostRepository, IAccommodationRateRepository accommodationRateRepository)
        {
            this.HostRepository = hostRepository;
            HostSubject = new Subject();
            this.AccommodationRateRepository = accommodationRateRepository;
        }

        public Host GetByUsername(string username)
        {

            return HostRepository.GetByUsername(username);
        }

        public Host GetById(int hostId)
        {
            return HostRepository.GetById(hostId);
        }

        public void BecomeSuperHost(Host host)
        {

            int counter = 0;
            double gradeSum = 0;
            foreach (AccommodationRate rate in AccommodationRateRepository.GetAll())
            {
                if (rate.HostId == host.Id)
                {
                    counter++;
                    gradeSum = gradeSum + Convert.ToDouble(rate.Correctness + rate.Cleanliness) / 2;
                }
            }

            double average = gradeSum / Convert.ToDouble(counter);
            host.RateAverage = average;
            host.RateCount = counter;

            host.IsSuperHost = isSuperHost(counter, gradeSum, average);
            HostRepository.Update(host);

            return;
        }

        public void SearchHost(Host host, string search)
        {
            host.Search = search;
            HostRepository.Update(host);
        }

        public bool isSuperHost(int counter, double gradeSum, double average)
        {
            if (counter < 10)
            {
                return false;
            }

            if (average >= 4.5)
            {
                return true;

            }
            else
            {
                return false;
            }

        }

        internal bool HasAccommodation(int userId, string city, string country)
        {
            foreach(Accommodation accommodation in AccommodationService.GetAll())
            {
                if(accommodation.HostId == userId && accommodation.City == city && accommodation.Country == country)
                    return true;
            }
            return false;
        }
    }
}
