using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class ForumService
    {
        private readonly IForumRepository ForumRepository;

        public ForumService(IForumRepository forumRepository)
        {
            ForumRepository = forumRepository;
        }

        public List<Forum> GetAll()
        {
            return ForumRepository.GetAll();
        }

        public Forum Add(Forum forum)
        {
            return ForumRepository.Add(forum);
        }

        public void Delete(Forum forum)
        {
            ForumRepository.Delete(forum);
        }

        public Forum Update(Forum forum)
        {
            return ForumRepository.Update(forum);
        }

        public Forum GetById(int id)
        {
            return ForumRepository.GetById(id);
        }
    }
}
