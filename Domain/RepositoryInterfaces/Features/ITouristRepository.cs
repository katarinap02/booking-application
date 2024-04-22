using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface ITouristRepository
    {
        List<Tourist> GetAll();
        Tourist FindTouristById(int touristId);
    }
}
