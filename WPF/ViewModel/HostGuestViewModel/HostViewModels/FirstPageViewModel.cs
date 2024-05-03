using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BookingApp.Observer;

using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using System.Windows.Controls;
using BookingApp.View.HostPages;
using System.Windows;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class FirstPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationService accommodationService { get; set; }

        public HostService hostService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public Host host { get; set; }

        public Frame HostFrame { get; set; }

        public Menu LeftDock { get; set; }
        public User User { get; set; }

        public StackPanel RatingPanel { get; set; }

        public HostViewModel hostViewModel { get; set; }

        public FirstPageViewModel(User user)

         {
                hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
                host = hostService.GetByUsername(user.Username);
                hostService.BecomeSuperHost(host);
                hostViewModel = new HostViewModel(host);
                User = user;
                Accommodations = new ObservableCollection<AccommodationViewModel>();
                accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
                Update();

         }



        public void Update()
          {
                Accommodations.Clear();
                foreach (Accommodation accommodation in accommodationService.GetAll())
                {
                    if (accommodation.HostId == host.Id)
                    {
                        Accommodations.Add(new AccommodationViewModel(accommodation));
                    }

                }
          }

     /*    public void RateGuest_Navigate(object sender, RoutedEventArgs e)
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
        */

    }
}
