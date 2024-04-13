using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.ViewModel
{
    public class NotificationViewModel : IObserver
    {
        public ObservableCollection<string> Notifications { get; set; }
        public DelayRequestService DelayRequestService { get; set; }
        public HostService HostService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public User User { get; set; }


        public NotificationViewModel(User user) { 
            User = user;
            Notifications = new ObservableCollection<string>();
            DelayRequestService = new DelayRequestService();
            HostService = new HostService();
            AccommodationReservationService = new AccommodationReservationService();
        }
        public void Update()
        {
           Notifications.Clear();
            List<DelayRequest> delayRequests = DelayRequestService.GetAll().OrderByDescending(d => d.RepliedDate).ToList();
           foreach(DelayRequest request in delayRequests)
           {

                if(request.Status == RequestStatus.APPROVED && request.GuestId == User.Id)
                {
                    Notifications.Add(CreateApprovedNotification(request));
                    
                }
                if (request.Status == RequestStatus.REJECTED && request.GuestId == User.Id)
                {
                    Notifications.Add(CreateRejectedNotification(request));
                }

            }
        }

        private string CreateRejectedNotification(DelayRequest request)
        {
            string hostUsername = HostService.GetById(request.HostId).Username;
            string message = hostUsername + " has rejected your request.\n";
            AccommodationReservation reservation = AccommodationReservationService.GetById(request.ReservationId);
            string dateRange = reservation.StartDate.ToString("MM/dd/yyyy") + " -> " + reservation.EndDate.ToString("MM/dd/yyyy");
            message += "Reservation: " + dateRange + "\n";
            message += "Time: " + request.RepliedDate.ToString();
            return message;
        }

        private string CreateApprovedNotification(DelayRequest request)
        {
            string hostUsername = HostService.GetById(request.HostId).Username;
            string message = hostUsername + " has approved your request.\n";
            AccommodationReservation reservation = AccommodationReservationService.GetById(request.ReservationId);
            string dateRange = reservation.StartDate.ToString("MM/dd/yyyy") + " -> " + reservation.EndDate.ToString("MM/dd/yyyy");
            message += "Reservation: " + dateRange + "\n";
            message += "Time: " + request.RepliedDate.ToString();
            return message;
        }
    }
}
