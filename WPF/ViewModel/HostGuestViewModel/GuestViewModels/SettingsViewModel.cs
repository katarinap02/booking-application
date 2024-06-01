using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class SettingsViewModel
    {
        public User User { get; set; }
        public UserService UserService { get; set; }

        
        public SettingsViewModel(User user) { 
            User = user;
            UserService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
        
        
        }
    }
}
