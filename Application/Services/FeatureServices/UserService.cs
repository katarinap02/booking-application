using BookingApp.Model;
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
        private readonly UserRepository UserRepository;

        public UserService()
        {
            UserRepository = new UserRepository();
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
