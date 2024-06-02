using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class RateDisplayPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationRateViewModel> Accommodations { get; set; }

        public AccommodationReservationService accommodationService { get; set; }

        public UserService userService { get; set; }

        public HostService hostService { get; set; }

        public AccommodationRateService accommodationRateService { get; set; }

        public NavigationService NavService { get; set; }

        public Host host { get; set; }

        public User User { get; set; }

        public RelayCommand NavigateToRateGuestPageCommand { get; set; }

        public GuestRateService guestRateService { get; set; }

        public MenuViewModel menuViewModel { get; set; }

        private void Execute_NavigateToGuestRatePageCommand(object obj)
        {
            GuestRatePage page = new GuestRatePage(User, NavService);
            this.NavService.Navigate(page);
            Update();
        }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public RateDisplayPageViewModel(User user, NavigationService navService)
        {
            Accommodations = new ObservableCollection<AccommodationRateViewModel>();
            accommodationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            accommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            guestRateService = new GuestRateService(Injector.Injector.CreateInstance<IGuestRateRepository>());
            userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            host = hostService.GetByUsername(user.Username);
            User = user;
            menuViewModel = new MenuViewModel(host);
            NavigateToRateGuestPageCommand = new RelayCommand(Execute_NavigateToGuestRatePageCommand, CanExecute_NavigateCommand);
            NavService = navService;
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationRate accommodationRate in accommodationRateService.GetAll())
            {
                AccommodationReservation accommodationReservation = accommodationService.GetById(accommodationRate.ReservationId);
                User user = userService.GetById(accommodationRate.GuestId);


                foreach (GuestRate guestRate in guestRateService.GetAll())
                {
                    if (guestRate.ReservationId == accommodationRate.ReservationId && accommodationRate.HostId == host.Id)
                    {
                        AccommodationRateViewModel ar = new AccommodationRateViewModel(accommodationRate, accommodationReservation, user);
                        if(ar.AccommodationName.ToLower().Contains(menuViewModel.SearchHost.ToLower())) {
                            Accommodations.Add(ar);
                        }
                        break;

                    }
                }



            }
        }
    }
}
