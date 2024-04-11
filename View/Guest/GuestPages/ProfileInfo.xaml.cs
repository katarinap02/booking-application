using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfileInfo.xaml
    /// </summary>
    public partial class ProfileInfo : Page, IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public AccommodationService AccommodationService { get; set; }
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public Frame Frame {  get; set; }   
        public ProfileInfo(AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, User user, Frame frame)
        {
            InitializeComponent();
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            this.User = user;
            this.Frame = frame;
            this.AccommodationReservationService = accommodationReservationService;
            this.AccommodationService = accommodationService;
            DataContext = this;
            statusLabel.Content = "guest";
            totalReservationsLabel.Content = GetTotalReservations(AccommodationReservationService);
            totalReservationsYearLabel.Content = GetTotalReservationsYear(AccommodationReservationService);
            Update();
        }

        private string GetTotalReservationsYear(AccommodationReservationService accommodationReservationService)
        {
            int number = 0;
            foreach (AccommodationReservation reservation in accommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id && reservation.StartDate.Year == DateTime.Now.Year)
                    number++;
            }

            return number.ToString();
        }

        private string GetTotalReservations(AccommodationReservationService accommodationReservationService)
        {
            int number = 0;
            foreach(AccommodationReservation reservation in accommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                    number++;
            }

            return number.ToString();
        }

        public void Update()
        {
            Reservations.Clear();

            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id)
                {
                    Reservations.Add(new AccommodationReservationViewModel(reservation));
                }
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e) { 

            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new CancelReservationPage(AccommodationReservationService, AccommodationService, SelectedReservation, User, Frame);

        
        
        
        }

        public void Delay_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new DelayRequestPage(AccommodationReservationService, AccommodationService, SelectedReservation, User, Frame);

        }
    }
}
