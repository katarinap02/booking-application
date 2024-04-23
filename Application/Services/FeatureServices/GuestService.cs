using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class GuestService
    {
        private readonly IGuestRepository GuestRepository;

        public GuestService(IGuestRepository guestRepository)
        {
            GuestRepository = guestRepository;
        }

        public Guest GetById(int id)
        {
            return GuestRepository.GetById(id);
        }

        public List<Guest> GetAll()
        {
            return GuestRepository.GetAll();
        }

        public Guest Update(Guest guest)
        {
            return GuestRepository.Update(guest);
        }
    }
}
