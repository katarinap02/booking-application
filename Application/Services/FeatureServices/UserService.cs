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

        public UserService(IUserRepository userRepository)
        {
            this.UserRepository = userRepository;
        }

        public User GetByUsername(string username)
        {
            return UserRepository.GetByUsername(username);
        }

        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }
    }
}
