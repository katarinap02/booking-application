using BookingApp.Domain.Model.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Rates
{
    public interface IGuestRateRepository
    {
        List<GuestRate> GetAll();

        GuestRate Add(GuestRate rate);
    }
}
