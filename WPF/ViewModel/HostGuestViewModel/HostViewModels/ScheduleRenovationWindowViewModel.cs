using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.WPF.View.HostWindows;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Calendar = System.Windows.Controls.Calendar;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class ScheduleRenovationWindowViewModel
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public User User { get; set; }

        public RenovationViewModel Renovation { get; set; }

        public ObservableCollection<DateTime> SelectedDates { get; set; }

        public MyICommand RenovationCommand { get; set; }

        public RenovationService RenovationService {get; set;}

        public AccommodationService AccommodationService { get; set;}

        public CalendarDateRange SelectedDateRange { get; set; }

        public CalendarConfigurator CalendarConfigurator { get; set; }

        bool isHost = true;

        public ScheduleRenovationWindowViewModel(AccommodationViewModel selectedAccommodation, User user, RenovationViewModel renovation, Calendar calendar)
        {
            User = user;
            SelectedAccommodation = selectedAccommodation;
            Renovation = renovation;
            User = user;
            SelectedAccommodation = selectedAccommodation;
            Renovation.HostId = user.Id;
            Renovation.AccommodationId = selectedAccommodation.Id;
            SelectedDates = new ObservableCollection<DateTime>();
            RenovationCommand = new MyICommand(AddRenovation);
            RenovationService = new RenovationService(Injector.Injector.CreateInstance<IRenovationRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            CalendarConfigurator = new CalendarConfigurator(calendar);

            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, Renovation.StartDateRange, Renovation.EndDateRange, Renovation.Duration, isHost);
        }

        private void AddRenovation()
        {
            SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
            AccommodationService.Update(SelectedAccommodation.ToAccommodation());
            RenovationService.Add(Renovation.ToRenovation());
            
        }

        

        public void WriteFirstAndLast(System.Windows.Controls.Calendar calendar)
        {
            Renovation.StartDate = calendar.SelectedDates.FirstOrDefault();
            Renovation.EndDate = calendar.SelectedDates.LastOrDefault();
            SelectedDatesCollection selectedDates = calendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);
        }
    }
}
