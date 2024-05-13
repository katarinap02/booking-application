using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IHostRepository
    {
        List<Host> GetAll();

        Host GetByUsername(string username);

        Host Update(Host host);

        Host GetById(int hostId);

    }
}
