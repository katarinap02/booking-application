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

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for DelayPage.xaml
    /// </summary>
    public partial class DelayPage : Page, IObserver
    {

        public ObservableCollection<DelayRequestViewModel> Delays { get; set; }

        public DelayRequestService DelayRequestService { get; set; }

        public UserService UserService {  get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }

        public DelayRequestViewModel SelectedDelay { get; set; }

        public DelayRequestViewModel Delay { get; set; }
        public DelayPage()
        {
            InitializeComponent();
            DataContext = this;
            Delays = new ObservableCollection<DelayRequestViewModel>();
            DelayRequestService = new DelayRequestService();
            UserService = new UserService();
            AccommodationReservationService = new AccommodationReservationService();
            Delay = new DelayRequestViewModel();
            Update();
        }

        public void Update()
        {
            Delays.Clear();
            foreach (DelayRequest delay in DelayRequestService.GetAll())
            {
                DelayRequestViewModel delayRequestViewModel = new DelayRequestViewModel(delay);
                User user = UserService.GetById(delay.GuestId);
                delayRequestViewModel.GuestUsername = user.Username;
                AccommodationReservation reservation = AccommodationReservationService.GetById(delay.ReservationId);
                delayRequestViewModel.ReservationName = reservation.Name;
                delayRequestViewModel.StartLastDate = reservation.StartDate;
                delayRequestViewModel.EndLastDate = reservation.EndDate;
                delayRequestViewModel.Reserved = AccommodationReservationService.IsReserved(delay.StartDate, delay.EndDate);
                if(delayRequestViewModel.Status == RequestStatus.PENDING) {
                    Delays.Add(delayRequestViewModel);
                }

            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDelay != null)
            {
                Delay = SelectedDelay;
                Delay.Status = RequestStatus.APPROVED;
                AccommodationReservationService.UpdateReservation(Delay.ReservationId, Delay.StartDate, Delay.EndDate);
                DelayRequestService.Update(Delay.ToDelayRequest());
                Update();
            }
            
        }

        

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedDelay != null) {
                Delay = SelectedDelay;
                Delay.Status = RequestStatus.REJECTED;
                Delay.Comment = ExplanationTextBox.Text;
                DelayRequestService.Update(Delay.ToDelayRequest());
                Update();
            }
            
        }

        
    }
}
