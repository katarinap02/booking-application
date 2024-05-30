using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class RenovationDisplayPageViewModel: IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<RenovationViewModel> Renovations { get; set; }

        public RenovationService RenovationService { get; set; }

        public RenovationViewModel SelectedRenovation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public RelayCommand NavigateToSchedulePageCommand { get; set; }

        public RelayCommand NavigateToPreviousPageCommand { get; set; }

        public User User;

        public HostService hostService { get; set; }

        public Host host { get; set; }

        public MenuViewModel menuViewModel { get; set; }

        public NavigationService NavService { get; set; }

        public MyICommand DeleteCommand { get; set; }

        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

        private void Execute_NavigateToSchedulePageCommand(object obj)
        {
            ScheduleRenovationPage page = new ScheduleRenovationPage(User, NavService);
            this.NavService.Navigate(page);
        }
        private void Execute_NavigateToPreviousPageCommand(object obj)
        {
            PreviousRenovationDisplayPage page = new PreviousRenovationDisplayPage(User, NavService);
            this.NavService.Navigate(page);
        }
        public RenovationDisplayPageViewModel(User user, NavigationService navService) {
            Renovations = new ObservableCollection<RenovationViewModel>();
            User = user;
            NavService = navService;
            NavigateToSchedulePageCommand = new RelayCommand(Execute_NavigateToSchedulePageCommand, CanExecute_NavigateCommand);
            NavigateToPreviousPageCommand = new RelayCommand(Execute_NavigateToPreviousPageCommand, CanExecute_NavigateCommand);
            RenovationService = new RenovationService(Injector.Injector.CreateInstance<IRenovationRepository>());
            DeleteCommand = new MyICommand(DeleteReservation);
            hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            host = hostService.GetByUsername(user.Username);
            hostService.BecomeSuperHost(host);
            menuViewModel = new MenuViewModel(host);
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            Update();
        }

        public void Update()
        {
            Renovations.Clear();
            DateTime today = DateTime.Today;
            foreach (Renovation renovation in RenovationService.GetAll())
            {
                if(renovation.HostId == User.Id && (renovation.StartDate > today))
                {
                    RenovationViewModel rv = new RenovationViewModel(renovation);
                    if(rv.AccommodationName.ToLower().Contains(menuViewModel.SearchHost.ToLower())) 
                    Renovations.Add(rv);
                }
            }
        }

        public void DeleteReservation()
        {
            if(SelectedRenovation != null)
            {
                Accommodation SelectedAccommodation = FindAccommodation();
                SelectedDateRange = new CalendarDateRange(SelectedRenovation.StartDate, SelectedRenovation.EndDate);
                DateTime todayPlus5Days = DateTime.Today.AddDays(5);
                if (SelectedRenovation.StartDate > todayPlus5Days)
                {
                    DeleteUnabledDates(SelectedAccommodation);
                }
                RenovationService.CancelRenovation(SelectedRenovation.ToRenovation());
            }
            Update();
        }

        public Accommodation FindAccommodation()
        {
            foreach(Accommodation acc in AccommodationService.GetAll())
            {
                if(SelectedRenovation.AccommodationId == acc.Id)
                {
                    return acc;
                }
            }
            return null;

        }

        public void DeleteUnabledDates(Accommodation accommodation) { 
        foreach (CalendarDateRange dateRange in accommodation.UnavailableDates)
            {
                if (dateRange.Start == SelectedRenovation.StartDate && dateRange.End == SelectedRenovation.EndDate)
                {
                    accommodation.UnavailableDates.Remove(dateRange);
                    AccommodationService.Update(accommodation);
                    break;
                }
            }
        }
    }
}
