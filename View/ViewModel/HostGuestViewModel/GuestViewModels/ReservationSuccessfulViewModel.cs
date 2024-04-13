using BookingApp.Model;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using BookingApp.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.ViewModel
{
    public class ReservationSuccessfulViewModel
    {
        public AccommodationService AccommodationService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public CalendarDateRange SelectedDateRange { get; set; }
        public User User { get; set; }

        public Frame Frame { get; set; }

        public int GuestNumber { get; set; }
        public int DayNumber { get; set; }

        public string DateRange { get; set; }

        public string Location { get; set; }
        public ReservationSuccessfulViewModel(AccommodationViewModel selectedAccommodation, User user, Frame frame, CalendarDateRange selectedDateRange, int guestNumber)
        {
            this.SelectedAccommodation = selectedAccommodation;
            this.SelectedDateRange = selectedDateRange;
            this.GuestNumber = guestNumber;
            this.User = user;
            this.Frame = frame;
            AccommodationService = new AccommodationService();
            AccommodationRateService = new AccommodationRateService();
            AccommodationReservationService = new AccommodationReservationService();

            Location = SelectedAccommodation.Name + ", " + SelectedAccommodation.City + ", " + SelectedAccommodation.Country;
            DateRange = SelectedDateRange.Start.ToString("MM/dd/yyyy") + " -> " + SelectedDateRange.End.ToString("MM/dd/yyyy");
            DayNumber = (SelectedDateRange.End - SelectedDateRange.Start).Days + 1;
        }

        public void HomePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new HomePage(User, Frame);
        }

        public void ProfilePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProfilePage(User, Frame);
        }

    }
}
