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

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class DelayRequestPageViewModel
    {
        public CalendarConfigurator CalendarConfigurator { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CalendarDateRange SelectedDateRange { get; set; }

        public AccommodationService AccommodationService { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }


        public AccommodationReservationService AccommodationReservationService { get; set; }

        public DelayRequest DelayRequest { get; set; }
        public DelayRequestService DelayRequestService { get; set; }

        public int DayNumber { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public Calendar ReservationCalendar { get; set; }

        public Button reserveButton { get; set; }

        public DelayRequestPageViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation, DelayRequestPage page)
        {
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            SelectedReservation = selectedReservation;
            StartDate = DateTime.Now;
            EndDate = DateTime.MaxValue;
            DayNumber = (SelectedReservation.EndDate - SelectedReservation.StartDate).Days + 1;
            SelectedAccommodation = new AccommodationViewModel(AccommodationService.GetById(SelectedReservation.AccommodationId));
            ReservationCalendar = page.ReservationCalendar;
            reserveButton = page.reserveButton;
            DelayRequest = new DelayRequest();
            DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
            CalendarConfigurator = new CalendarConfigurator(ReservationCalendar);
            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
        }



        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
                reserveButton.IsEnabled = false;
            else
                reserveButton.IsEnabled = true;

            Mouse.Capture(null);
        }

        public void SelectDate_Click(object sender, RoutedEventArgs e)
        {

            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);
            AccommodationReservationService.DelayReservation(SelectedDateRange, DelayRequest, DelayRequestService, AccommodationService, SelectedReservation);


        }

    }
}
