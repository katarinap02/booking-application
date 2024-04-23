using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IGuestRepository
    {
        List<Guest> GetAll();

        Guest Update(Guest guest);

        Guest GetById(int hostId);
    }
}
