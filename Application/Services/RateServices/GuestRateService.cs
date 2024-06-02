using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.RateServices
{
    public class GuestRateService
    {
        private readonly IGuestRateRepository GuestRateRepository;

        public GuestRateService()
        {
            GuestRateRepository = new GuestRateRepository();
        }

        public GuestRateService(IGuestRateRepository guestRateRepository)
        {
            this.GuestRateRepository = guestRateRepository;
        }

        public List<GuestRate> GetAll()
        {
            return GuestRateRepository.GetAll();
        }

        public double CalculateAverageGrade(int id)
        {
            double sum = 0;
            double counter = 0;

            foreach (GuestRate rate in GetAll())
            {
                if (rate.UserId == id)
                {
                   
                        sum += rate.RulesFollowing + rate.Cleanliness;
                        counter += 2;

                    
                }
            }

            double average = sum / counter;

            if (average > 0)
                return Math.Round(average, 2);
            else return 0;

        }
    }
}
