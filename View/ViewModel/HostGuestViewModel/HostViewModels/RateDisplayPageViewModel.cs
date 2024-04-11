using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.View.ViewModel.HostGuestViewModel.HostViewModels
{
    public class RateDisplayPageViewModel : INotifyPropertyChanged ,IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationRateViewModel> Accommodations { get; set; }

        public AccommodationReservationService accommodationService { get; set; }

        public UserService userService { get; set; }

        public AccommodationRateService accommodationRateService { get; set; }

        public GuestRateService guestRateService
        {
            get; set;
        }


        public RateDisplayPageViewModel() {
            Accommodations = new ObservableCollection<AccommodationRateViewModel>();
            accommodationService = new AccommodationReservationService();
            userService = new UserService();
            accommodationRateService = new AccommodationRateService();
            guestRateService = new GuestRateService();
            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationRate accommodationRate in accommodationRateService.GetAll())
            {
                AccommodationReservation accommodationReservation = accommodationService.GetById(accommodationRate.ReservationId);
                User user = userService.GetById(accommodationRate.GuestId);


                foreach (GuestRate guestRate in guestRateService.GetAll())
                {
                    if (guestRate.ReservationId == accommodationRate.ReservationId)
                    {
                        Accommodations.Add(new AccommodationRateViewModel(accommodationRate, accommodationReservation, user));
                        break;
                    }
                }



            }
        }
    }
}
