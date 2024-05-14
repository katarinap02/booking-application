using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AnywhereAnytimeContinueViewModel
    {

        public User User { get; set; }
        public Frame Frame { get; set; }

        public int DayNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }

        public string DateRange => StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");

        public AnywhereAnytimeContinueViewModel(User user, Frame frame, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
            User = user;
            Frame = frame;
            DayNumber = dayNumber;
            StartDate = startDate;
            GuestNumber = guestNumber;
            EndDate = endDate;
            
        }
    }
}
