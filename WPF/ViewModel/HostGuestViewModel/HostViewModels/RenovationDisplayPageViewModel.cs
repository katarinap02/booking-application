using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        public User User;

        public MyICommand DeleteCommand { get; set; }
        public RenovationDisplayPageViewModel(User user) {
            Renovations = new ObservableCollection<RenovationViewModel>();
            User = user;
            RenovationService = new RenovationService(Injector.Injector.CreateInstance<IRenovationRepository>());
            DeleteCommand = new MyICommand(DeleteReservation);
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
                    Renovations.Add(new RenovationViewModel(renovation));
                }
            }
        }

        public void DeleteReservation()
        {
            if(SelectedRenovation != null)
            {
                Accommodation SelectedAccommodation = FindAccommodation();
                SelectedDateRange = new CalendarDateRange(SelectedRenovation.StartDate, SelectedRenovation.EndDate);
                DeleteUnabledDates(SelectedAccommodation);
                RenovationService.Delete(SelectedRenovation.ToRenovation());
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
