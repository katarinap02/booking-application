using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class ReservationCancellationViewModel : IObserver
    {
        public ObservableCollection<string> Notifications { get; set; }

        public ReservationCancellationService ReservationCancellationService { get; set; }

        public UserService UserService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public User User { get; set; }
        public ReservationCancellationViewModel(User user)
        {
            Notifications = new ObservableCollection<string>();
            ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
            UserService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            User = user;
            Update();
        }
        public void Update()
        {
            Notifications.Clear();
            List<ReservationCancellation> cancellations = ReservationCancellationService.GetAll().OrderByDescending(c => c.CancellationDate).ToList();
            foreach (ReservationCancellation cancellation in cancellations)
            {
                if (cancellation.HostId == User.Id)
                    Notifications.Add(CreateCancellationNotification(cancellation));
            }
        }

        private string CreateCancellationNotification(ReservationCancellation cancellation)
        {
            string guestUsername = UserService.GetById(cancellation.GuestId).Username;
            string message = guestUsername + " has cancelled a reservation.\n";
            string dateRange = cancellation.StartDate.ToString("MM/dd/yyyy") + " -> " + cancellation.EndDate.ToString("MM/dd/yyyy");
            message += "Reservation: " + dateRange + "\n";
            message += "Time: " + cancellation.CancellationDate.ToString();
            return message;
        }
    }
}
