using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.View.HostPages;
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
    public class PreviousRenovationDisplayPageViewModel: IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<RenovationViewModel> Renovations { get; set; }

    public RenovationService RenovationService { get; set; }
    public RelayCommand NavigateToScheduledPageCommand { get; set; }
    public RelayCommand NavigateToSchedulePageCommand { get; set; }

    public NavigationService NavService { get; set; }

     public User User;

    public MenuViewModel menuViewModel { get; set; }

    public HostService hostService { get; set; }

    public Host host { get; set; }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToSchedulePageCommand(object obj)
        {
            ScheduleRenovationPage page = new ScheduleRenovationPage(User, NavService);
            this.NavService.Navigate(page);
        }

        private void Execute_NavigateToScheduledPageCommand(object obj)
        {
            RenovationDisplayPage page = new RenovationDisplayPage(User, NavService);
            this.NavService.Navigate(page);
        }
        public PreviousRenovationDisplayPageViewModel(User user, NavigationService navService)
        {
            Renovations = new ObservableCollection<RenovationViewModel>();
            User = user;
            hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            host = hostService.GetByUsername(user.Username);
            hostService.BecomeSuperHost(host);
            menuViewModel = new MenuViewModel(host);
            NavigateToSchedulePageCommand = new RelayCommand(Execute_NavigateToSchedulePageCommand, CanExecute_NavigateCommand);
            NavigateToScheduledPageCommand = new RelayCommand(Execute_NavigateToScheduledPageCommand, CanExecute_NavigateCommand);
            NavService = navService;
            RenovationService = new RenovationService(Injector.Injector.CreateInstance<IRenovationRepository>());
            Update();
    }

    public void Update()
    {
        Renovations.Clear();
        DateTime today = DateTime.Today;
            foreach (Renovation renovation in RenovationService.GetAll())
        {
            if (renovation.HostId == User.Id && (renovation.StartDate <= today))
            {
                    RenovationViewModel rv = new RenovationViewModel(renovation);
                    if (rv.AccommodationName.ToLower().Contains(menuViewModel.SearchHost.ToLower()))
                    Renovations.Add(rv);
                }
        }
    }
}
}
