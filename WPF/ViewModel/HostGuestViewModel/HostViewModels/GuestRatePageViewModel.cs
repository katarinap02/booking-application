using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class GuestRatePageViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationReservationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public HostService hostService { get; set; }

        public Host host { get; set; }
        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public GuestRateViewModel guestRateViewModel { get; set; }

        public GuestRateRepository guestRateRepository { get; set; }

        public MyICommand SaveCommand { get; set; }

        public GuestRatePageViewModel(User user)
        {
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            guestRateRepository = new GuestRateRepository();
            guestRateViewModel = new GuestRateViewModel();
            hostService = new HostService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            host = hostService.GetByUsername(user.Username);
            SaveCommand = new MyICommand(Save_Click);
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
            {
                if (accommodationRepository.GetById(accommodation.AccommodationId).HostId == host.Id)
                    Accommodations.Add(new AccommodationReservationViewModel(accommodation));

            }
        }

        private void Save_Click()
        {
            if (SelectedAccommodation != null)
            {
                guestRateViewModel.ReservationId = SelectedAccommodation.Id;
                guestRateViewModel.GuestId = SelectedAccommodation.GuestId;
                guestRateViewModel.AccommodationId = SelectedAccommodation.AccommodationId;
                guestRateRepository.Add(guestRateViewModel.toGuestRate());
                MessageBox.Show("Guest rate added.");
            }
            Update();

        }
    }
}
