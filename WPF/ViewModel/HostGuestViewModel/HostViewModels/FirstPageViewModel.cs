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
using BookingApp.WPF.View.HostPages;
using GalaSoft.MvvmLight.Command;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using System.Windows.Threading;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class FirstPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }

        public AccommodationViewModel MostPopular { get; set; }

        public AccommodationViewModel LeastPopular { get; set; }
        public AccommodationService accommodationService { get; set; }

        public AccommodationReservationService accommodationReservationService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public HostService hostService { get; set; }

        public Host host { get; set; }

        public RelayCommand NavigateToRateGuestPageCommand { get; set; }

        public RelayCommand NavigateToDelayPageCommand { get; set; }

        public MyICommand<AccommodationViewModel> NavigateToStatisticPageCommand { get; set; }

        public RelayCommand NavigateToForumPageCommand { get; set; }

        public MyICommand<AccommodationViewModel> CloseAccommodation { get; set; }

        public MyICommand<AccommodationViewModel> ChangedPictureCommand { get; set; }

        public MyICommand<AccommodationViewModel> RegisterCommand { get; set; }
        public User User { get; set; }

        public NavigationService NavService { get; set; }

        public HostViewModel hostViewModel { get; set; }

        public MenuViewModel menuViewModel { get; set; }

        public bool IsDemo {  get; set; }


        private void Execute_NavigateToGuestRatePageCommand(object obj)
        {
            CloseMenu();
            GuestRatePage page = new GuestRatePage(User, NavService);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToDelayPageCommand(object obj)
        {
            CloseMenu();
            DelayPage page = new DelayPage(User, NavService);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToStatisticPageCommand(AccommodationViewModel acc)
        {
            CloseMenu();
            StatisticYearsPage page = new StatisticYearsPage(User, acc, NavService);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToRegisterPageCommand(AccommodationViewModel acc)
        {
            CloseMenu();
            RegisterAccommodationPage page = new RegisterAccommodationPage(User, acc, NavService);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToForumPageCommand(object obj)
        {
            CloseMenu();
            ForumPage page = new ForumPage(User, NavService);
            this.NavService.Navigate(page);
        }

        private void CloseFunction(AccommodationViewModel acc)
        {
            accommodationService.CloseAccommodation(acc.Id);
            Update();
        }
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        public FirstPageViewModel(User user, NavigationService navService, bool demo)

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
                NavigateToStatisticPageCommand = new MyICommand<AccommodationViewModel>(Execute_NavigateToStatisticPageCommand);
                ChangedPictureCommand = new MyICommand<AccommodationViewModel>(ChangePicture);
            NavigateToForumPageCommand = new RelayCommand(Execute_NavigateToForumPageCommand, CanExecute_NavigateCommand);
            RegisterCommand = new MyICommand<AccommodationViewModel>(Execute_NavigateToRegisterPageCommand);
                accommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
                MostPopular = new AccommodationViewModel(accommodationReservationService.GetMostPopularLocation(host.Id));
                LeastPopular = new AccommodationViewModel(accommodationReservationService.GetLeastPopularLocation(host.Id));
                CloseAccommodation = new MyICommand<AccommodationViewModel>(CloseFunction);
            IsDemo = demo;
            Update();

         }

        private void ChangePicture(AccommodationViewModel acc)
        {
            
            if (acc.NumberOfPictures > 1) {
                accommodationService.ChangeListOrder(acc);
            }
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {

                if (accommodation.HostId == host.Id && !accommodation.ClosedAccommodation && accommodation.Name.ToLower().Contains(menuViewModel.SearchHost.ToLower())) 
                    {
                    AccommodationViewModel ac = new AccommodationViewModel(accommodation);
                    doPopular(ac);
                    Accommodations.Add(ac);
                    }

                }
            

        }

        private void doPopular(AccommodationViewModel acc)
        {
            if(acc.City.Equals(MostPopular.City))
            {
                acc.IsMostPopular = true;
            }
            if(acc.City.Equals(LeastPopular.City))
            {
                acc.IsLeastPopular = true;
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
