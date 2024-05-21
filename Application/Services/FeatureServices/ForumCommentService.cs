using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository.FeatureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class ForumCommentService
    {
        private readonly IForumCommentRepository ForumCommentRepository;
        public ForumCommentService(IForumCommentRepository forumCommentRepository)
        {
            ForumCommentRepository = forumCommentRepository;
        }

        public List<ForumComment> GetAll()
        {
            return ForumCommentRepository.GetAll();
        }

        public ForumComment Add(ForumComment forumComment)
        {
            return ForumCommentRepository.Add(forumComment);
        }

        public void Delete(ForumComment forumComment)
        {
            ForumCommentRepository.Delete(forumComment);
        }

        public ForumComment Update(ForumComment forumComment)
        {
            return ForumCommentRepository.Update(forumComment);
        }

        public ForumComment GetById(int id)
        {
            return ForumCommentRepository.GetById(id);
        }
    }
}
