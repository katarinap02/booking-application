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

       
    }
}
