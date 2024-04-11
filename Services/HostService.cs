using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class HostService
    {
        private readonly HostRepository hostRepository;
        public Subject hostSubject;
        private readonly AccommodationRateRepository accommodationRateRepository;

        public HostService() {
            hostRepository = new HostRepository();
            hostSubject = new Subject();
            accommodationRateRepository = new AccommodationRateRepository();
        }

        public HostService(HostRepository hostRepository, AccommodationRateRepository accommodationRateRepository)
        {
            this.hostRepository = hostRepository;
            hostSubject = new Subject();
            this.accommodationRateRepository = accommodationRateRepository;
        }

        public Host GetByUsername(string username)
        {

            return hostRepository.GetByUsername(username);
        }

        public Host GetById(int hostId)
        {
            return hostRepository.GetById(hostId);
        }

        public void BecomeSuperHost(Host host)
        {
            int counter = 0;
            double gradeSum = 0;
            foreach (AccommodationRate rate in accommodationRateRepository.GetAll())
            {
                if (rate.HostId == host.Id)
                {
                    counter++;
                    gradeSum = gradeSum + (Convert.ToDouble(rate.Correctness + rate.Cleanliness) / 2);
                }
            }

            if (counter < 50)
            {
                host.IsSuperHost = false;
                hostRepository.Update(host);
                return;
            }

            double average = gradeSum / Convert.ToDouble(counter);
            host.RateAverage = average;
            host.RateCount = counter;
            if (average >= 4.5)
            {
                host.IsSuperHost = true; ;

            }
            else
            {
                host.IsSuperHost = false;
            }

            hostRepository.Update(host);
            return;
        }
    }
}
