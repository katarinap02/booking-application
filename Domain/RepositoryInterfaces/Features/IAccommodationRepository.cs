using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IAccommodationRepository
    {
        Accommodation GetById(int id);
        List<Accommodation> GetAll();
        Accommodation Add(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        void Delete(Accommodation accommodation);
        int NextId();
    }
}
