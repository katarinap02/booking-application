using BookingApp.Model;
using BookingApp.Application.Services;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationSuccessfulViewModel
    {

        public AccommodationViewModel SelectedAccommodation { get; set; }

        public User User { get; set; }

        public Frame Frame { get; set; }



        public ReservationSuccessfulViewModel(AccommodationViewModel selectedAccommodation, User user, Frame frame)
        {
            SelectedAccommodation = selectedAccommodation;
            User = user;
            Frame = frame;

        }

        public void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new HomePage(User, Frame);
        }

        public void ProfilePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProfilePage(User, Frame);
        }

    }
}
