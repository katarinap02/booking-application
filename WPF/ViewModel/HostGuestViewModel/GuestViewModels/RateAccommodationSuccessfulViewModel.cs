using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RateAccommodationSuccessfulViewModel
    {
        public AccommodationViewModel Accommodation { get; set; }
        public string HostUsername { get; set; }

        public RateAccommodationSuccessfulViewModel(AccommodationViewModel accommodation, string hostUsername)
        {
            Accommodation = accommodation;
            HostUsername = hostUsername;
        }
    }
}
