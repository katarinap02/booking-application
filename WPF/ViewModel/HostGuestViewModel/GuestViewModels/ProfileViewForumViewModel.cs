using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ProfileViewForumViewModel
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public ForumViewModel SelectedForum { get; set; }

        public string Username => User.Username;
        public string UserType => User.Type.ToString();

      

        public ProfileViewForumViewModel(User user, Frame frame, ForumViewModel selectedForum)
        {
            User = user;
            Frame = frame;
            SelectedForum = selectedForum;
        }
    }
}
