using BookingApp.Domain.Model.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Reservations
{
    public interface IDelayRequestRepository
    {
        List<DelayRequest> GetAll();
        DelayRequest Add(DelayRequest request);

        int NextId();
        void Delete(DelayRequest request);
        DelayRequest Update(DelayRequest request);
    }
}
