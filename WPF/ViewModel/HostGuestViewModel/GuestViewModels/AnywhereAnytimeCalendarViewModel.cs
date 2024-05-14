using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{

    public class AnywhereAnytimeCalendarViewModel
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public CalendarDateRange SelectedDateRange { get; set; }
        public int DayNumber { get; set; }

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }

        public CalendarConfigurator CalendarConfigurator { get; set; }

        public Guest Guest { get; set; }

        public GuestService GuestService { get; set; }

        public Calendar ReservationCalendar { get; set; }
        public AnywhereAnytimeCalendarPage Page { get; set; }

        // KOMANDE
        public GuestICommand SelectDatesCommand { get; set; }
        public AnywhereAnytimeCalendarViewModel(User user, Frame frame, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate, AnywhereAnytimeCalendarPage page, AccommodationViewModel selectedAccommodation)
        {
            
            User = user;
            Frame = frame;
            DayNumber = dayNumber;
            StartDate = startDate;
            EndDate = endDate;
            Page = page;
            GuestNumber = guestNumber;
            SelectedAccommodation = selectedAccommodation;
            ReservationCalendar = Page.ReservationCalendar;
            SelectedDateRange = new CalendarDateRange(StartDate, EndDate);
            CalendarConfigurator = new CalendarConfigurator(ReservationCalendar);
            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Guest = GuestService.GetById(User.Id);
            GuestService.CalculateGuestStats(Guest);

            SelectDatesCommand = new GuestICommand(OnSelectDates);
        }

        private void OnSelectDates()
        {

            Page.CalendarSection.IsEnabled = false;
          
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);
            Page.HintLabel.Content = "Problem with guest number input";
            Page.Hint.Text = "Number of guests cannot be larger than specified maximal number of guests.";
            Page.Hint.Visibility = Visibility.Collapsed;

        }



        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                Page.reserveButton.IsEnabled = false;
                //  Page.dayNumberValidator.Visibility = Visibility.Visible;

            }
            else
            {
                Page.reserveButton.IsEnabled = true;
                // Page.dayNumberValidator.Visibility = Visibility.Hidden;

            }

            Mouse.Capture(null);
        }

    }
}
