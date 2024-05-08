using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RatesByHostViewModel : IObserver
    {
        public ObservableCollection<RateDetailsViewModel> GuestRates { get; set; }

        public GuestRateService GuestRateService { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }

        public User User { get; set; }
        public Frame Frame { get; set; }

        public RatesByHostViewModel(User user, Frame frame)
        {
            User = user;
            Frame = frame;
            GuestRates = new ObservableCollection<RateDetailsViewModel>();
            GuestRateService = new GuestRateService(Injector.Injector.CreateInstance<IGuestRateRepository>());
            AccommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        }
        public void Update()
        {
            GuestRates.Clear();
            foreach (GuestRate rate in GuestRateService.GetAll())
                if (IsHostRated(rate) && rate.UserId == User.Id)
                    GuestRates.Add(new RateDetailsViewModel(new GuestRateViewModel(rate)));
        }

        private bool IsHostRated(GuestRate rate)
        {
            foreach (AccommodationRate accommodatinRate in AccommodationRateService.GetAll())
                if (accommodatinRate.ReservationId == rate.ReservationId)
                    return true;
            return false;
        }
    }
}
