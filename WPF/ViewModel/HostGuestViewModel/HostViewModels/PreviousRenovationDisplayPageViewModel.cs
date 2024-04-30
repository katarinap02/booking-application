using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
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
    public class PreviousRenovationDisplayPageViewModel: IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<RenovationViewModel> Renovations { get; set; }

    public RenovationService RenovationService { get; set; }

    public User User;
    public PreviousRenovationDisplayPageViewModel(User user)
    {
        Renovations = new ObservableCollection<RenovationViewModel>();
        User = user;
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
                Renovations.Add(new RenovationViewModel(renovation));
            }
        }
    }
}
}
