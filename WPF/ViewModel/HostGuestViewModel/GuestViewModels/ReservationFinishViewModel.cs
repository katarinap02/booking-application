using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ReservationFinishViewModel
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public User User { get; set; }
        public int DayNumber { get; set; }
        public Frame Frame { get; set; }
        public ReservationInfoPage ReservationInfoPage { get; set; }

        public AccommodationReservationViewModel Reservation { get; set; }

        // KOMANDE
        public GuestICommand ContinueCommand { get; set; }
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
            ContinueCommand = new GuestICommand(OnContinue);

        }

        private void OnContinue()
        {
            DayNumber = Convert.ToInt32(ReservationInfoPage.txtDayNumber.Text);
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
