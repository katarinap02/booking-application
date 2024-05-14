using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IForumRepository
    {
        Forum GetById(int id);
        List<Forum> GetAll();
        Forum Add(Forum forum);
        Forum Update(Forum forum);
        void Delete(Forum forum);
        int NextId();
    }
}
