using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface IForumCommentRepository
    {
        ForumComment GetById(int id);
        List<ForumComment> GetAll();
        ForumComment Add(ForumComment comment);
        ForumComment Update(ForumComment comment);

        void Delete(ForumComment comment);
        int NextId();
    }
}
