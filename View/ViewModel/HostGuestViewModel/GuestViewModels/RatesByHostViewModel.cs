using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.View.ViewModel
{
    public class RatesByHostViewModel : IObserver
    {
        public ObservableCollection<GuestRateViewModel> GuestRates { get; set; }

        public GuestRateService GuestRateService { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }

        public User User { get; set; }
        public Frame Frame { get; set; }

        public RatesByHostViewModel(User user, Frame frame)
        {
            User = user; 
            Frame = frame;
            GuestRates = new ObservableCollection<GuestRateViewModel>();
            GuestRateService = new GuestRateService();
            AccommodationRateService = new AccommodationRateService();
        }
        public void Update()
        {
            GuestRates.Clear();
            foreach (GuestRate rate in GuestRateService.GetAll())
                if (IsHostRated(rate) && rate.UserId == User.Id)
                    GuestRates.Add(new GuestRateViewModel(rate));
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
