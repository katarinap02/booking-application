using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class ScheduleRenovationWindowViewModel
    {
        public AccommodationViewModel SelectedAccommodation {  get; set; }

        public User User { get; set; }

        public  DateTime StartDateRange { get; set; }

        public DateTime EndDateRange { get; set; }   

        public int Duration { get; set; }

        public RenovationViewModel Renovation { get; set; }

        public ScheduleRenovationWindowViewModel(AccommodationViewModel selectedAccommodation, User user, RenovationViewModel renovation)
        {
            User = user;
            SelectedAccommodation = selectedAccommodation;
            Renovation = renovation;
        }
    }
}
