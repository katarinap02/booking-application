using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class StatisticYearsPageViewModel
    {
        public AccommodationViewModel AccommodationViewModel { get; set; }
        
        public User User { get; set; }
        public StatisticYearsPageViewModel(User user, AccommodationViewModel acc) {
            AccommodationViewModel = acc;
            User = user;

        }
    }
}
