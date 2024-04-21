using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BookingApp.Observer;
using BookingApp.WPF.ViewModel.HostGuestViewModel;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class FirstPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }

        public HostService hostService { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public Host host { get; set; }

        public HostViewModel hostViewModel { get; set; }

        public FirstPageViewModel(User user)
        {
            hostService = new HostService();
            host = hostService.GetByUsername(user.Username);
            hostService.BecomeSuperHost(host);
            hostViewModel = new HostViewModel(host);
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            accommodationRepository = new AccommodationRepository();
            Update();

        }



        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationRepository.GetAll())
            {
                if (accommodation.HostId == host.Id)
                {
                    Accommodations.Add(new AccommodationViewModel(accommodation));
                }


            }
        }
    }

}
