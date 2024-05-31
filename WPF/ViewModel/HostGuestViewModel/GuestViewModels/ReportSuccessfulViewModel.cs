using BookingApp.Domain.Model.Features;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
   public class ReportSuccessfulViewModel
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public string Path { get; set; }
        public GuestICommand YourProfileCommand { get; set; }
        public ReportSuccessfulViewModel(User user, Frame frame, string path)
        {
          
            User = user;
            Frame = frame;
            Path = path;
            YourProfileCommand = new GuestICommand(OnYourProfile);
           
        }

        private void OnYourProfile()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }
    }
}
