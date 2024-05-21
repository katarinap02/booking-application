using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using Microsoft.VisualBasic.ApplicationServices;
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
        public UserService UserService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public ForumCommentService(IForumCommentRepository forumCommentRepository, IUserRepository userRepository, IAccommodationReservationRepository accommodationReservationRepository, IDelayRequestRepository delayRequestRepository)
        {
            ForumCommentRepository = forumCommentRepository;
            UserService = new UserService(userRepository);
            AccommodationReservationService = new AccommodationReservationService(accommodationReservationRepository, delayRequestRepository);
           
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

        public ForumComment CreateComment(int id, string comment, string city, string country)
        {
            ForumComment result = new ForumComment();
            result.UserId = id;
            result.Comment = comment;
            Domain.Model.Features.User user = UserService.GetById(id);
            if (user.Type == UserType.host)
                result.IsHost = true;

            if(user.Type == UserType.guest)
            {
                result.IsHost = false;
                result.IsSpecial = HasReservation(id, city, country);
            }

            result.Date = DateTime.Now;
            return this.Add(result);
          


        }

        private bool HasReservation(int id, string city, string country)
        {
            foreach(AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                if (reservation.GuestId == id && reservation.City == city && reservation.Country == country)
                    return true;
            }
            return false;
        }
    }
}
