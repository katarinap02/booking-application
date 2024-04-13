using BookingApp.Model;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using BookingApp.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.ViewModel
{
    public class ShowRequestsViewModel
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
            DelayRequestService = new DelayRequestService();
            Requests = new ObservableCollection<DelayRequestViewModel>();
            RequestStatusBox = page.requestStatusBox;

        }

        public void Update()
        {
            Requests.Clear();

            switch (RequestStatusBox.SelectedItem)
            {
                case ComboBoxItem pendingItem when pendingItem.Content.ToString() == "Pending":
                    ShowPendingRequests(Requests);
                    break;
                case ComboBoxItem approvedItem when approvedItem.Content.ToString() == "Approved":
                    ShowApprovedRequests(Requests);
                    break;
                case ComboBoxItem rejectedItem when rejectedItem.Content.ToString() == "Rejected":
                    ShowRejectedRequests(Requests);
                    break;
            }
        }

        private void ShowRejectedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.REJECTED && request.GuestId == User.Id)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowApprovedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.APPROVED && request.GuestId == User.Id)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowPendingRequests(ObservableCollection<DelayRequestViewModel> requests)
        {

            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.PENDING && request.GuestId == User.Id)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        public void RequestStatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
