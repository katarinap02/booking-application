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
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class FirstPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationService accommodationService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public HostService hostService { get; set; }

        public Host host { get; set; }

        public RelayCommand NavigateToRateGuestPageCommand { get; set; }

        public RelayCommand NavigateToDelayPageCommand { get; set; }
        public User User { get; set; }

        public NavigationService NavService { get; set; }

        public HostViewModel hostViewModel { get; set; }

        public MenuViewModel menuViewModel { get; set; }


        private void Execute_NavigateToGuestRatePageCommand(object obj)
        {
            CloseMenu();
            GuestRatePage page = new GuestRatePage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToDelayPageCommand(object obj)
        {
            CloseMenu();
            DelayPage page = new DelayPage(User);
            this.NavService.Navigate(page);
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public FirstPageViewModel(User user, NavigationService navService)

         {
                hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
                host = hostService.GetByUsername(user.Username);
                hostService.BecomeSuperHost(host);
                hostViewModel = new HostViewModel(host);
                User = user;
                NavService = navService;
                menuViewModel = new MenuViewModel(host);
                Accommodations = new ObservableCollection<AccommodationViewModel>();
                accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
                NavigateToRateGuestPageCommand = new RelayCommand(Execute_NavigateToGuestRatePageCommand, CanExecute_NavigateCommand);
                NavigateToDelayPageCommand = new RelayCommand(Execute_NavigateToDelayPageCommand, CanExecute_NavigateCommand);
                Update();

         }



        public void Update()
          {
                Accommodations.Clear();
                foreach (Accommodation accommodation in accommodationService.GetAll())
                {
                if (accommodation.HostId == host.Id && accommodation.Name.ToLower().Contains(menuViewModel.SearchHost.ToLower())) 
                    {
                        Accommodations.Add(new AccommodationViewModel(accommodation));
                    }

                }
          }

        private void CloseMenu()
        {
            menuViewModel.IsMenuOpened = false;
            menuViewModel.IsRatingOpened = false;
            menuViewModel.IsRenovationOpened = false;
        }



    }
}
