using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.HostPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System.Windows.Navigation;
using System.Runtime.CompilerServices;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class HostWindowViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public MenuViewModel menuViewModel { get; set; }

        public Frame HostFrame { get; set; }

        public Menu LeftDock { get; set; }
        public User User { get; set; }

        public StackPanel RatingPanel { get; set; }

        public StackPanel RenovationPanel { get; set; }

        public RelayCommand NavigateToRegisterPageCommand { get; set; }

        public RelayCommand NavigateToHomePageCommand { get; set; }

        public RelayCommand NavigateToDelayPageCommand { get; set; }

        public RelayCommand NavigateToGuestRatingsPageCommand {  get; set; }

        public RelayCommand NavigateToRateGuestPageCommand {  get; set; }

        public RelayCommand OpenMenuCommand { get; set; }

        public RelayCommand OpenRatingCommand { get; set; }

        public RelayCommand OpenRenovationCommand { get; set; }

        public RelayCommand NavigateToPreviousPageCommand {  get; set; }

        public RelayCommand NavigateToScheduledPageCommand {  get; set; }

        public RelayCommand NavigateToSchedulePageCommand { get; set; }
        public NavigationService NavService { get; set; }


        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToRegisterPageCommand(object obj)
        {
            RegisterAccommodationPage page = new RegisterAccommodationPage(User);
            CloseMenu();
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToHomePageCommand(object obj)
        {
            CloseMenu();
            FirstPage page = new FirstPage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToDelayPageCommand(object obj)
        {
            CloseMenu();
            DelayPage page = new DelayPage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToRatingsPageCommand(object obj)
        {
            CloseMenu();
            RateDisplayPage page = new RateDisplayPage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToGuestRatePageCommand(object obj)
        {
            CloseMenu();
            GuestRatePage page = new GuestRatePage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            CloseMenu();
            PreviousRenovationDisplayPage page = new PreviousRenovationDisplayPage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToScheduledPageCommand(object obj)
        {
            CloseMenu();
            RenovationDisplayPage page = new RenovationDisplayPage(User);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToSchedulePageCommand(object obj)
        {
            CloseMenu();
            ScheduleRenovationPage page = new ScheduleRenovationPage(User);
            this.NavService.Navigate(page);
        }



        public HostWindowViewModel(User user, NavigationService navService)
        {
            menuViewModel = new MenuViewModel();
            NavigateToRegisterPageCommand = new RelayCommand(Execute_NavigateToRegisterPageCommand ,CanExecute_NavigateCommand);
            NavigateToHomePageCommand = new RelayCommand(Execute_NavigateToHomePageCommand, CanExecute_NavigateCommand);
            NavigateToDelayPageCommand = new RelayCommand(Execute_NavigateToDelayPageCommand, CanExecute_NavigateCommand);
            NavigateToGuestRatingsPageCommand = new RelayCommand(Execute_NavigateToRatingsPageCommand, CanExecute_NavigateCommand);
            NavigateToRateGuestPageCommand = new RelayCommand(Execute_NavigateToGuestRatePageCommand, CanExecute_NavigateCommand);
            NavigateToPreviousPageCommand = new RelayCommand(Execute_NavigateToPreviousPageCommand, CanExecute_NavigateCommand);
            NavigateToScheduledPageCommand = new RelayCommand(Execute_NavigateToScheduledPageCommand, CanExecute_NavigateCommand);
            NavigateToSchedulePageCommand = new RelayCommand(Execute_NavigateToSchedulePageCommand, CanExecute_NavigateCommand);
            this.OpenMenuCommand = new RelayCommand(
                                        execute => this.menuViewModel.IsMenuOpened = !this.menuViewModel.IsMenuOpened, CanExecute_NavigateCommand);
            this.OpenRatingCommand = new RelayCommand(
                                       execute => this.menuViewModel.IsRatingOpened = !this.menuViewModel.IsRatingOpened, CanExecute_NavigateCommand);
            this.OpenRenovationCommand = new RelayCommand(
                                       execute => this.menuViewModel.IsRenovationOpened = !this.menuViewModel.IsRenovationOpened, CanExecute_NavigateCommand);
            User = user;
            NavService = navService;
            Update();

        }
       

        public void Update()
        {
            FirstPage page = new FirstPage(User);
            this.NavService.Navigate(page);
        }

        private void CloseMenu()
        {
            menuViewModel.IsMenuOpened = false;
            menuViewModel.IsRatingOpened = false;
            menuViewModel.IsRenovationOpened = false;
        }

    }
}
