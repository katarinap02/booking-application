using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class ScheduleRenovationPageViewModel: IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationService accommodationService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public User User { get; set; }
        public ScheduleRenovationPageViewModel(User user)
        {
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            User = user;
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                if (accommodation.HostId == User.Id)
                {
                    Accommodations.Add(new AccommodationViewModel(accommodation));
                }

            }
        }
    }
}
