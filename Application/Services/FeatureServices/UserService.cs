using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class UserService
    {
        private readonly IUserRepository UserRepository;
        private readonly TourRequestService tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());

        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public User GetByUsername(string username)
        {
            return UserRepository.GetByUsername(username);
        }
        public User Update(User user)
        {
            return UserRepository.Update(user); 
        }
        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }

        public void UpdateTourRequests()
        {
            tourRequestService.UpdateTourRequests();
        }


    }
}
