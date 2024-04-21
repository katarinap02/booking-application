using BookingApp.Model;
using BookingApp.View.GuestPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationCalendarViewModel
    {

        public CalendarConfigurator CalendarConfigurator { get; set; }

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public User User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationReservation Reservation { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public Frame Frame { get; set; }
        public int DayNumber { get; set; }

        public int GuestNumber { get; set; }

        public Calendar ReservationCalendar { get; set; }

        public CalendarPage Page { get; set; }
        public ReservationCalendarViewModel(AccommodationViewModel selectedAccommodation, int dayNumber, User user, DateTime start, DateTime end, Frame frame, CalendarPage page)
        {

            SelectedAccommodation = selectedAccommodation;
            User = user;
            StartDate = start;
            EndDate = end;
            DayNumber = dayNumber;
            Frame = frame;
            ReservationCalendar = page.ReservationCalendar;
            Page = page;
            page.reserveButton.IsEnabled = false;
            page.PeopleNumberSection.IsEnabled = false;
            Reservation = new AccommodationReservation();

            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();

            CalendarConfigurator = new CalendarConfigurator(ReservationCalendar);

            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);

        }



        public void SelectDate_Click(object sender, RoutedEventArgs e)
        {

            Page.PeopleNumberSection.IsEnabled = true;
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);




        }

        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                Page.reserveButton.IsEnabled = false;


            }
            else
            {
                Page.reserveButton.IsEnabled = true;

            }

            Mouse.Capture(null);
        }

        public void Reserve_Click(object sender, RoutedEventArgs e)
        {
            GuestNumber = Convert.ToInt32(Page.txtGuestNumber.Text);
            if (GuestNumber > SelectedAccommodation.MaxGuestNumber)
            {
                // finishReservation.IsEnabled = false;

            }
            else
            {
                Page.finishReservation.IsEnabled = true;
                Reservation = new AccommodationReservation(User.Id, SelectedAccommodation.Id, SelectedDateRange.Start, SelectedDateRange.End, GuestNumber, SelectedAccommodation.Name, SelectedAccommodation.City, SelectedAccommodation.Country);
                SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
                AccommodationService.Update(SelectedAccommodation.ToAccommodation());
                AccommodationReservationService.Add(Reservation);
                Frame.Content = new ReservationSuccessfulPage(new AccommodationReservationViewModel(Reservation), SelectedAccommodation, SelectedDateRange, GuestNumber, User, Frame);



            }

        }
    }
}
