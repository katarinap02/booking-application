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
using BookingApp.Domain.Model.Features;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationSuccessfulViewModel
    {

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationReservationViewModel Reservation { get; set; }
        public User User { get; set; }
        public Guest Guest { get; set; }

        public GuestService GuestService { get; set; }
        public Frame Frame { get; set; }



        public ReservationSuccessfulViewModel(AccommodationViewModel selectedAccommodation, User user, Frame frame, AccommodationReservationViewModel reservation)
        {
            SelectedAccommodation = selectedAccommodation;
            User = user;
            Frame = frame;
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Guest = GuestService.GetById(User.Id);
            GuestService.CalculateGuestStats(Guest);
            Reservation = reservation;


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
