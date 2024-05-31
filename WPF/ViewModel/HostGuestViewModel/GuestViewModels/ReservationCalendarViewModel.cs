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
using System.ComponentModel;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationCalendarViewModel : INotifyPropertyChanged
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

        

        public Calendar ReservationCalendar { get; set; }

        public CalendarPage Page { get; set; }

        // KOMANDE

        public GuestICommand SelectDatesCommand { get; set; }
        public GuestICommand FinishReservationCommand { get; set; }

        public GuestICommand BackCommand { get; set; }

        public GuestICommand BackToCalendarCommand { get; set; }

        public NavigationService NavigationService { get; set; }
        private int  guestNumber;
        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                if (guestNumber != value)
                {

                    guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                    
                }

                FinishReservationCommand.RaiseCanExecuteChanged();
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

       

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
            FinishReservationCommand = new GuestICommand(OnFinishReservation, CanFinishReservation);
            BackCommand = new GuestICommand(OnBack);
            BackToCalendarCommand = new GuestICommand(OnBackToCalendar);

            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());

            CalendarConfigurator = new CalendarConfigurator(ReservationCalendar);

            CalendarConfigurator.ConfigureCalendar(SelectedAccommodation, StartDate, EndDate, DayNumber);
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Guest = GuestService.GetById(User.Id);
            GuestService.CalculateGuestStats(Guest);
            NavigationService = Frame.NavigationService;
            GuestNumber = 1;



           // Page.finishReservation.IsEnabled = false;

        }

        private void OnBackToCalendar()
        {
            Page.CalendarSection.IsEnabled = true;
            Page.PeopleNumberSection.IsEnabled = false;
        }

        private void OnBack()
        {
            NavigationService.GoBack();
        }

        private bool CanFinishReservation()
        {
            ToggleGuestNumberValidator();
            if (GuestNumber > SelectedAccommodation.MaxGuestNumber || GuestNumber <= 0)
            {  
                return false;
            }
            else
            { 
                return true;
            }
        }

        private void ToggleGuestNumberValidator()
        {
            if (GuestNumber > SelectedAccommodation.MaxGuestNumber || GuestNumber <= 0)
            {
                
                if (Page.PeopleNumberSection.IsEnabled == true)
                    Page.guestNumberValidator.Visibility = Visibility.Visible;
              
            }
            else
            {
                Page.guestNumberValidator.Visibility = Visibility.Hidden;

                
            }
        }

        private void OnFinishReservation()
        {
            //GuestNumber = Convert.ToInt32(Page.txtGuestNumber.Text);

            
            Reservation = new AccommodationReservation(User.Id, SelectedAccommodation.Id, SelectedDateRange.Start, SelectedDateRange.End, GuestNumber, SelectedAccommodation.Name, SelectedAccommodation.City, SelectedAccommodation.Country, DateTime.Now);
            SelectedAccommodation.UnavailableDates.Add(SelectedDateRange);
            AccommodationService.Update(SelectedAccommodation.ToAccommodation());
            AccommodationReservationService.Add(Reservation);
            if (Guest.BonusPoints > 0)
            {
                Guest.BonusPoints--;
            }

            Frame.Content = new ReservationSuccessfulPage(new AccommodationReservationViewModel(Reservation), SelectedAccommodation, SelectedDateRange, GuestNumber, User, Frame);



            
        }

        private void OnSelectDates()
        {
           
            Page.CalendarSection.IsEnabled = false;
            Page.PeopleNumberSection.IsEnabled = true;
            SelectedDatesCollection selectedDates = ReservationCalendar.SelectedDates;
            SelectedDateRange = new CalendarDateRange(selectedDates[0], selectedDates[selectedDates.Count - 1]);
            UpdateHintContent();
            Page.langTextbox.TextChanged += ContentChanged;

        }


        private void ContentChanged(object sender, EventArgs e)
        {
            UpdateHintContent();
            // ViewModel.UpdateHintContent();
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

            }
            else
            {
                Page.reserveButton.IsEnabled = true;
                Page.dayNumberValidator.Visibility = Visibility.Hidden;

            }

            Mouse.Capture(null);
        }

        internal void UpdateHintContent()
        {
            if (Page.langTextbox.Text == "English")
            {
                Page.HintLabel.Content = "Problem with guest number input";
                Page.Hint.Text = "Number of guests cannot be larger than specified maximal number of guests.";
                Page.Hint.Visibility = Visibility.Collapsed;
            }

            if (Page.langTextbox.Text == "Srpski")
            {
                Page.HintLabel.Content = "Problem sa unosom broja gostiju";
                Page.Hint.Text = "Broj gostiju ne može biti veći od specificiranog broja gostiju.";
                Page.Hint.Visibility = Visibility.Collapsed;
            }

        }
    }
}
