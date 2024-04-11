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
    /// Interaction logic for RateAccommodationPage.xaml
    /// </summary>
    public partial class RateAccommodationPage : Page, IObserver
    {

        public ObservableCollection<AccommodationReservationViewModel> Reservations { get; set; }
        public User User { get; set; }

        public AccommodationReservationViewModel SelectedReservation {  get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }
        
        public Frame Frame {  get; set; }   

        public RateAccommodationPage(User user, AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, AccommodationRateService accommodationRateService, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.AccommodationReservationService = accommodationReservationService;
            this.Frame = frame;
            Reservations = new ObservableCollection<AccommodationReservationViewModel>();
            DataContext = this;
            this.AccommodationService = accommodationService;
            this.AccommodationRateService = accommodationRateService;
            Update();

        }

        public void Update()
        {
            Reservations.Clear();
            foreach (AccommodationReservation reservation in AccommodationReservationService.GetAll())
            {
                if (reservation.GuestId == User.Id && IsBeforeFiveDays(reservation))
                {
                    Reservations.Add(new AccommodationReservationViewModel(reservation));
                }
            }
        }

        private bool IsBeforeFiveDays(AccommodationReservation reservation)
        {
            int daysPassed = (DateTime.Now - reservation.EndDate).Days;
            if (daysPassed <= 5)
                return true;
            else
                return false;
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new RateAccommodationForm(User, SelectedReservation, AccommodationService, AccommodationRateService, Frame);
        }
    }
}
