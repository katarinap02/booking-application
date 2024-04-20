using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.View.ViewModel;
using System.ComponentModel;
using BookingApp.Observer;
using BookingApp.View.HostPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.View.ViewModel.HostGuestViewModel.HostViewModels
{
    public class FirstPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }

        public HostService hostService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public Host host { get; set; }

        public Frame HostFrame { get; set; }

        public Menu LeftDock { get; set; }
        public User User { get; set; }

        public StackPanel RatingPanel { get; set; }

        public HostViewModel hostViewModel { get; set; }

        public FirstPageViewModel( User user, Frame frame, Menu dock, StackPanel panel) {
            hostService = new HostService();
            host = hostService.GetByUsername(user.Username);
            hostService.BecomeSuperHost(host);
            hostViewModel = new HostViewModel(host);
            HostFrame = frame;
            LeftDock = dock;
            User = user;
            RatingPanel = panel;
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            accommodationRepository = new AccommodationRepository();
            Update();

        }



        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationRepository.GetAll())
            {
                if(accommodation.HostId == host.Id) {
                    Accommodations.Add(new AccommodationViewModel(accommodation));
                }
            }
        }

        public void RateGuest_Navigate(object sender, RoutedEventArgs e)
        {
            GuestRatePage page = new GuestRatePage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        public void Delay_Navigate(object sender, RoutedEventArgs e)
        {
            DelayPage page = new DelayPage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }
    }

}
