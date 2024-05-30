using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ShowRequestsViewModel : IObserver
    {
        public ObservableCollection<DelayRequestViewModel> Requests { get; set; }
        public DelayRequestService DelayRequestService { get; set; }

        public User User { get; set; }

        public Frame Frame { get; set; }

        public ComboBox RequestStatusBox { get; set; }
        public ShowRequestsViewModel(User user, Frame frame, RequestsPage page)
        {
            User = user;
            Frame = frame;
            DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Requests = new ObservableCollection<DelayRequestViewModel>();
            RequestStatusBox = page.requestStatusBox;

        }

        public void Update()
        {
            Requests.Clear();

            switch (RequestStatusBox.SelectedItem)
            {
                case ComboBoxItem pendingItem when pendingItem.Content.ToString() == "Pending" || pendingItem.Content.ToString() == "Na čekanju":
                    ShowPendingRequests(Requests);
                    break;
                case ComboBoxItem approvedItem when approvedItem.Content.ToString() == "Approved" || approvedItem.Content.ToString() == "Prihvaćeni":
                    ShowApprovedRequests(Requests);
                    break;
                case ComboBoxItem rejectedItem when rejectedItem.Content.ToString() == "Rejected" || rejectedItem.Content.ToString() == "Odbijeni":
                    ShowRejectedRequests(Requests);
                    break;
            }
        }

        private void ShowRejectedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.REJECTED && IsUser(request.GuestId, User.Id))
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowApprovedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.APPROVED && IsUser(request.GuestId, User.Id))
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowPendingRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.PENDING && IsUser(request.GuestId, User.Id))
                    Requests.Add(new DelayRequestViewModel(request));
        }

        public void RequestStatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private bool IsUser(int guestId, int userId)
        {
            return guestId == userId;
        }
    }
}
