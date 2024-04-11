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
    /// Interaction logic for RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page, IObserver
    {
        public ObservableCollection<DelayRequestViewModel> Requests { get; set; }
        public DelayRequestService DelayRequestService { get; set; }
        public User User { get; set; }
        public Frame Frame { get; set; }  
        
        public RequestsPage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;
            DelayRequestService = new DelayRequestService();
            Requests = new ObservableCollection<DelayRequestViewModel>();
            DataContext = this;
            foreach (ComboBoxItem item in requestStatusBox.Items)
            {
                if (item.Content.ToString() == "Pending")
                {
                    requestStatusBox.SelectedItem = item;
                    break;
                }
            }

            //Update();
        }

        public void Update()
        {
            Requests.Clear();

            switch (requestStatusBox.SelectedItem)
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
                if (request.Status == RequestStatus.REJECTED)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowApprovedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {
           
            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.APPROVED)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowPendingRequests(ObservableCollection<DelayRequestViewModel> requests)
        {
           
            foreach (DelayRequest request in DelayRequestService.GetAll())
                if(request.Status == RequestStatus.PENDING) 
                Requests.Add(new DelayRequestViewModel(request));
        }

        private void RequestStatusBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
