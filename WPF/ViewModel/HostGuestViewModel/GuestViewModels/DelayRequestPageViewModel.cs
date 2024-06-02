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
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System.Windows.Navigation;
using System.Windows.Media.Animation;

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

        public User User { get; set; }
        public Frame Frame { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public DelayRequest DelayRequest { get; set; }
        public DelayRequestService DelayRequestService { get; set; }

        public int DayNumber { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public Calendar ReservationCalendar { get; set; }

        public Button reserveButton { get; set; }

        public NavigationService NavigationService { get; set; }

        // KOMANDE
        public GuestICommand SelectDatesCommand { get; set; }
        public GuestICommand BackCommand { get; set; }
        public DelayRequestPage Page { get; set; }
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
            SelectDatesCommand = new GuestICommand(OnSelectDates);
            BackCommand = new GuestICommand(OnBack);
            Page = page;
            
           
            User = user;
            Frame = frame;

            NavigationService = Frame.NavigationService;
        }

        private void OnBack()
        {
            NavigationService.GoBack();
        }

        private void OnSelectDates()
        {
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);
            DelayRequest request = AccommodationReservationService.DelayReservation(SelectedDateRange, DelayRequest, DelayRequestService, AccommodationService, SelectedReservation);
            DelayRequestViewModel viewModel = new DelayRequestViewModel(request);
            Frame.Content = new RequestSentPage(User, Frame, viewModel);
        }

        public void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            Calendar calendar = (Calendar)sender;
            int dayNumber = DayNumber;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if (selectedDatesCount != DayNumber)
            {
                Page.reserveButton.IsEnabled = false;

                Page.dayNumberValidator.Visibility = Visibility.Visible;
                var showHint = (Storyboard)Page.FindResource("ShowTextBlock");
                showHint.Begin(Page.dayNumberValidator);

            }
            else
            {
                Page.reserveButton.IsEnabled = true;

                var hideHint = (Storyboard)Page.FindResource("HideTextBlock");
                hideHint.Completed += (s, a) => Page.dayNumberValidator.Visibility = Visibility.Hidden;
                hideHint.Begin(Page.dayNumberValidator);


            }

            Mouse.Capture(null);
        }

     

    }
}
