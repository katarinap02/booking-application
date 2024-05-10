using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationFinishViewModel : INotifyPropertyChanged
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public User User { get; set; }
     
        public Frame Frame { get; set; }
        public ReservationInfoPage ReservationInfoPage { get; set; }

        public AccommodationReservationViewModel Reservation { get; set; }

        // KOMANDE
        public GuestICommand ContinueCommand { get; set; }
        public GuestICommand BackCommand { get; set; }

        public NavigationService NavigationService { get; set; }

        private int dayNumber;
        public int DayNumber
        {
            get { return dayNumber; }
            set
            {
                if (dayNumber != value)
                {
                    dayNumber = value;
                    OnPropertyChanged("DayNumber");

                }

                ContinueCommand.RaiseCanExecuteChanged();
            }

        }

        


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReservationFinishViewModel(AccommodationViewModel selectedAccommodation, User user, Frame frame, ReservationInfoPage reservationInfoPage)
        {

            //AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            SelectedAccommodation = selectedAccommodation;
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            Reservation = new AccommodationReservationViewModel();
            ReservationInfoPage = reservationInfoPage;
            User = user;
            Frame = frame;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            ContinueCommand = new GuestICommand(OnContinue, CanContinue);
            BackCommand = new GuestICommand(OnBack);
            NavigationService = Frame.NavigationService;
            

        }

        private void OnBack()
        {
            NavigationService.GoBack();
        }

        private bool CanContinue()
        {
            DateTime start;
            DateTime end;
            if (string.IsNullOrEmpty(ReservationInfoPage.txtStartDate.Text))
                start = DateTime.MinValue;
            else
                start = Convert.ToDateTime(ReservationInfoPage.txtStartDate.Text);

            if (string.IsNullOrEmpty(ReservationInfoPage.txtEndDate.Text))
                end = DateTime.MinValue;
            else
                end = Convert.ToDateTime(ReservationInfoPage.txtEndDate.Text);
            
            if (ValidateDayNumber(DayNumber) && ValidateDateInputs(start, end))
                return true;
            else
                return false;
        }

        

        private void OnContinue()
        {
            // DayNumber = Convert.ToInt32(ReservationInfoPage.txtDayNumber.Text);
            DateTime start = Convert.ToDateTime(ReservationInfoPage.txtStartDate.Text);
            DateTime end = Convert.ToDateTime(ReservationInfoPage.txtEndDate.Text);


            Frame.Content = new CalendarPage(SelectedAccommodation, DayNumber, User, start, end, Frame);
        }

      

        private bool ValidateDayNumber(int dayNumber)
        {
            if (DayNumber < SelectedAccommodation.MinReservationDays)
            {

                return false;
            }
            else
            {

                return true;
            }
        }

        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if (start >= end)
            {

                return false;
            }
            else
            {

                return true;
            }


        }


    }
}
