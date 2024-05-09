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
    public class RateAccommodationSuccessfulViewModel
    {
        public AccommodationViewModel Accommodation { get; set; }
        public string HostUsername { get; set; }

        public User User { get; set; }
        public Frame Frame { get; set; }

        // KOMANDE
        public GuestICommand RateAnotherCommand { get; set; }
        public GuestICommand ProfileCommand { get; set; }

        public RateAccommodationSuccessfulViewModel(AccommodationViewModel accommodation, string hostUsername, User user, Frame frame)
        {
            Accommodation = accommodation;
            User = user;
            Frame = frame;
            HostUsername = hostUsername;
            RateAnotherCommand = new GuestICommand(OnRateAnother);
            ProfileCommand = new GuestICommand(OnProfile);
        }

        private void OnProfile()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }

        private void OnRateAnother()
        {
            Frame.Content = new RateAccommodationPage(User, Frame);
        }
    }
}
