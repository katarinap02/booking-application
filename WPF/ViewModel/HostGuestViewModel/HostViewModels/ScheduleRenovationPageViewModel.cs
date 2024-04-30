using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using BookingApp.View;
using BookingApp.WPF.View.HostWindows;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
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

        public AccommodationViewModel SelectedAccommodation { get; set; }

        public RenovationViewModel Renovation { get; set; }

        public User User { get; set; }

        public MyICommand ScheduleCommand { get; set; }
        public ScheduleRenovationPageViewModel(User user)
        {
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            User = user;
            ScheduleCommand = new MyICommand(ScheduleWindow);
            Renovation = new RenovationViewModel();
            Update();
        }

        private void ScheduleWindow()
        {
            if(Condition()) { 
            ScheduleRenovationWindow window = new ScheduleRenovationWindow(SelectedAccommodation, User, Renovation);
            window.ShowDialog();
            }
        }

        private bool Condition()
        {
            if (SelectedAccommodation != null )
            return true;

            return false;
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
