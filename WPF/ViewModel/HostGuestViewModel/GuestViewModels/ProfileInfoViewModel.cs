using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;


namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ProfileInfoViewModel : IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public Guest Guest { get; set; }


        public GuestService GuestService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public Frame Frame { get; set; }
       // public string Status { get; set; }
        public int TotalReservations { get; set; }
        public int TotalYearReservations { get; set; }

        // KOMANDE
        public GuestICommand<object> DelayCommand { get; set; }
        public GuestICommand<object> CancelCommand { get; set; }
        public GuestICommand<object> DetailsCommand { get; set; }

        public GuestICommand CreateReportCommand { get; set; }
        public ProfileInfoViewModel(User user, Frame frame, ProfileInfo page)
        {
            Frame = frame;
            User = user;
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
           // Status = "guest";
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Guest = GuestService.GetById(User.Id);
            GuestService.CalculateGuestStats(Guest);
            TotalReservations = GetTotalReservations(AccommodationReservationService);
            DelayCommand = new GuestICommand<object>(OnDelay, CanDelay);
            CancelCommand = new GuestICommand<object>(OnCancel, CanCancel);
            DetailsCommand = new GuestICommand<object>(OnDetails);
            CreateReportCommand = new GuestICommand(OnCreateReport);
            CancelCommand.RaiseCanExecuteChanged();
            DelayCommand.RaiseCanExecuteChanged();
          

        }

        private void OnDetails(object sender)
        {
            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new ReservationDetailsPage(User, Frame, SelectedReservation);
        }

        private void OnCreateReport()
        {
            Frame.Content = new ReservationReportPage(User, Frame);
        }

        private bool CanDelay(object sender)
        {
            Button button = sender as Button;
            AccommodationReservationViewModel reservation = button.DataContext as AccommodationReservationViewModel;
            if (DateTime.Now > reservation.StartDate)
                return false;
            else
                return true;
        }

        private bool CanCancel(object sender)
        {
            Button button = sender as Button;
            AccommodationReservationViewModel reservation = button.DataContext as AccommodationReservationViewModel;
            int daysBefore = (reservation.StartDate - DateTime.Today).Days;
            int dayLimit = AccommodationService.GetById(reservation.AccommodationId).ReservationDaysLimit;
            if (daysBefore < dayLimit)
                return false;
            else
                return true;
        }

        private void OnCancel(object sender)
        {
            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new CancelReservationPage(SelectedReservation, User, Frame);
        }

        private void OnDelay(object sender)
        {
            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new DelayRequestPage(SelectedReservation, User, Frame);
        }

        private int GetTotalReservations(AccommodationReservationService accommodationReservationService)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                    number++;
            }

            return number;
        }

        public void Update()
        {
            Reservations.Clear();
            List<AccommodationReservationViewModel> tmpReservations = new List<AccommodationReservationViewModel>(); 
            tmpReservations = SortReservations();
            foreach (AccommodationReservationViewModel reservation in tmpReservations)
            {
                
                   Reservations.Add(reservation);
                
            }

        }

        private List<AccommodationReservationViewModel> SortReservations()
        {
            List<AccommodationReservationViewModel> tmpReservations = new List<AccommodationReservationViewModel>();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                {
                    tmpReservations.Add(new AccommodationReservationViewModel(reservation));
                }
            }

            return tmpReservations.OrderByDescending(reservation => reservation.StartDate).ToList();
        }
    }
}
