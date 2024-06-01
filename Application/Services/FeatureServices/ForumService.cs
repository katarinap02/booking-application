using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
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

        public ForumCommentService ForumCommentService { get; set; }
        public HostService HostService { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public ForumService(IForumRepository forumRepository, IForumCommentRepository forumCommentRepository, IUserRepository userRepository, IAccommodationReservationRepository accommodationReservationRepository, IDelayRequestRepository delayRequestRepository)
        {
            ForumRepository = forumRepository;
            ForumCommentService = new ForumCommentService(forumCommentRepository, userRepository, accommodationReservationRepository, delayRequestRepository);
            HostService = new HostService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
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

        public void CalculateGuestHostComments(Forum forum)
        {
            int guestComments = 0;
            int hostComments = 0;
            foreach(ForumComment forumComment in ForumCommentService.GetAll())
            {
                if(forumComment.ForumId == forum.Id)
                {
                    if (forumComment.IsHost)
                        hostComments++;

                    if (!forumComment.IsHost && ForumCommentService.HasReservation(forum.UserId, forum.City, forum.Country))
                        guestComments++;
                   
                }

            }
            CheckForumUsefulness(forum, guestComments, hostComments);
            
        }

        private void CheckForumUsefulness(Forum forum, int guestComments, int hostComments)
        {
            if (guestComments >= 20 && hostComments >= 10)
                forum.IsVeryUseful = true;
            else
                forum.IsVeryUseful = false;

            Update(forum);
        }

        public List<Forum> GetForumsForHost(User user)
        {
            List<Forum> forums = new List<Forum>();
            List<String> locations = AccommodationService.GetHostLocations(user);
            foreach (Forum f in GetAll())
            {
                if (locations.Contains(f.City))
                {
                    forums.Add(f);
                }
            }

            return forums;
        }

    }
}
