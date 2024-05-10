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
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.ViewModel.Commands;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationCalendarViewModel
    {

        public CalendarConfigurator CalendarConfigurator { get; set; }

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public User User { get; set; }

        public Guest Guest { get; set; }

        public GuestService GuestService { get; set; }
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

        // KOMANDE

        public GuestICommand SelectDatesCommand { get; set; }
        public GuestICommand FinishReservationCommand { get; set; }
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

            SelectDatesCommand = new GuestICommand(OnSelectDates);
            FinishReservationCommand = new GuestICommand(OnFinishReservation);

            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());

            CalendarConfigurator = new CalendarConfigurator(ReservationCalendar);

            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Guest = GuestService.GetById(User.Id);
            GuestService.CalculateGuestStats(Guest);

        }

        private void OnFinishReservation()
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
                if (Guest.BonusPoints > 0)
                {
                    Guest.BonusPoints--;
                }

                Frame.Content = new ReservationSuccessfulPage(new AccommodationReservationViewModel(Reservation), SelectedAccommodation, SelectedDateRange, GuestNumber, User, Frame);



            }
        }

        private void OnSelectDates()
        {

            Page.PeopleNumberSection.IsEnabled = true;
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


            }
            else
            {
                Page.reserveButton.IsEnabled = true;

            }

            Mouse.Capture(null);
        }

       
    }
}
