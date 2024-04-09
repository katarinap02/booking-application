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
            requestStatusBox.Text = "Pending";
            Update();
        }

        public void Update()
        {
            
            String statusValue = requestStatusBox.Text;
            switch(statusValue)
            {
                case "Pending":
                    ShowPendingRequests(Requests);
                    break;
                case "Approved":
                    ShowApprovedRequests(Requests); 
                    break;
                case "Rejected":
                    ShowRejectedRequests(Requests);
                    break;

            }

        }

        private void ShowRejectedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {
            Requests.Clear();
            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.REJECTED)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowApprovedRequests(ObservableCollection<DelayRequestViewModel> requests)
        {
            Requests.Clear();
            foreach (DelayRequest request in DelayRequestService.GetAll())
                if (request.Status == RequestStatus.APPROVED)
                    Requests.Add(new DelayRequestViewModel(request));
        }

        private void ShowPendingRequests(ObservableCollection<DelayRequestViewModel> requests)
        {
            Requests.Clear();
            foreach (DelayRequest request in DelayRequestService.GetAll())
                if(request.Status == RequestStatus.PENDING) 
                Requests.Add(new DelayRequestViewModel(request));
        }
    }
}
