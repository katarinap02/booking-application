using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.GuestPages;

namespace BookingApp.View.ViewModel
{
    public class DelayRequestViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler PropertyChanged;

        

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private int guestId;
        public int GuestId
        {
            get { return guestId; }
            set
            {
                if (guestId != value)
                {
                    guestId = value;
                    OnPropertyChanged("GuestId");
                }
            }
        }

        private string guestUserame;
        public string GuestUsername
        {
            get { return guestUserame; }
            set
            {
                if (guestUserame != value)
                {
                    guestUserame = value;
                    OnPropertyChanged("GuestUsername");
                }
            }
        }

        private int hostId;
        public int HostId
        {
            get { return hostId; }
            set
            {
                if (hostId != value)
                {
                    hostId = value;
                    OnPropertyChanged("HostId");
                }
            }
        }

        private int reservationId;
        public int ReservationId
        {
            get { return reservationId; }
            set
            {
                if (reservationId != value)
                {
                    reservationId = value;
                    OnPropertyChanged("ReservationId");
                }
            }
        }

        private string reservationName;
        public string ReservationName
        {
            get { return reservationName; }
            set
            {
                if (reservationName != value)
                {
                    reservationName = value;
                    OnPropertyChanged("ReservationName");
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {

                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {

                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        private DateTime startLastDate;
        public DateTime StartLastDate
        {
            get { return startLastDate; }
            set
            {
                if (startLastDate != value)
                {

                    startLastDate = value;
                    OnPropertyChanged("StartLastDate");
                }
            }
        }

        private DateTime endLastDate;
        public DateTime EndLastDate
        {
            get { return endLastDate; }
            set
            {
                if (endLastDate != value)
                {

                    endLastDate = value;
                    OnPropertyChanged("EndLastDate");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {

                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private RequestStatus status;
        public RequestStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }


        private bool reserved;
        public bool Reserved
        {
            get { return reserved; }
            set
            {
                if (reserved != value)
                {
                    reserved = value;
                    OnPropertyChanged("Reserved");
                }
            }
        }

        private DateTime repliedDate;
        public DateTime RepliedDate
        {
            get { return repliedDate; }
            set
            {
                if (repliedDate != value)
                {

                    repliedDate = value;
                    OnPropertyChanged("RepliedDate");
                }
            }
        }

        private AccommodationReservationService AccommodationReservationService = new AccommodationReservationService();
        private AccommodationService AccommodationService = new AccommodationService();

        public string AccommodationName => GetAccommodationName(AccommodationReservationService, AccommodationService, ReservationId);

        private static string GetAccommodationName(AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, int reservationId)
        {
            int accommodationId = accommodationReservationService.GetById(reservationId).AccommodationId;
            string accommodationName = accommodationService.GetById(accommodationId).Name;
            return accommodationName;
        }

        public string DateRange => startDate.ToString() + "-" + endDate.ToString();

        public ObservableCollection<DelayRequestViewModel> Requests { get; set; }
        public DelayRequestService DelayRequestService { get; set; }
        public User User { get; set; }
        public Frame Frame { get; set; }

        public DelayRequestViewModel SelectedRequest { get; set; }

        public HostService HostService { get; set; }
        public string HostUsername {  get; set; }

        public string OldDateRange { get; set; }
        public string NewDateRange { get; set; }   
        public int NumberOfPeople { get; set; }
        public int NumberOfDays { get; set; }

        public string RequestHeader { get; set; }
        public AccommodationViewModel Accommodation { get; set; }

        public ComboBox RequestStatusBox { get; set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DelayRequestViewModel(User user, Frame frame, DelayRequestViewModel selectedRequest)
        {
            User = user;
            Frame = frame;
            SelectedRequest = selectedRequest;
            DelayRequestService = new DelayRequestService();
            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();
            HostService = new HostService();
            AccommodationReservation reservation = AccommodationReservationService.GetById(SelectedRequest.ReservationId);
            Accommodation = new AccommodationViewModel(AccommodationService.GetById(reservation.AccommodationId));
            HostUsername = HostService.GetById(SelectedRequest.HostId).Username;
            OldDateRange = SelectedRequest.EndLastDate.ToString("MM/dd/yyyy") + " - " + SelectedRequest.StartLastDate.ToString("MM/dd/yyyy");
            NewDateRange = SelectedRequest.EndDate.ToString("MM/dd/yyyy") + " - " + SelectedRequest.StartDate.ToString("MM/dd/yyyy");
            NumberOfPeople = reservation.NumberOfPeople;
            Comment = SelectedRequest.Comment;
            NumberOfDays = (reservation.EndDate - reservation.StartDate).Days + 1;
            RequestHeader = CreateRequestHeader(SelectedRequest);



        }

        private string? CreateRequestHeader(DelayRequestViewModel selectedRequest)
        {
            if (selectedRequest.Status == RequestStatus.PENDING)
            {
                Comment = "Waiting for host's response...";
                return "Your request is pending.";
            }
                
            else if (selectedRequest.Status == RequestStatus.APPROVED)
                return "Your request is approved.";
            else
                return "Your request is rejected";
        }

        public DelayRequestViewModel(User user, Frame frame, RequestsPage page)
        {
            User = user;
            Frame = frame;
            DelayRequestService = new DelayRequestService();
            Requests = new ObservableCollection<DelayRequestViewModel>();
            RequestStatusBox = page.requestStatusBox;
           



        }

        public DelayRequestViewModel(DelayRequest dr)
        {
            id = dr.Id;
            guestId = dr.GuestId;
            hostId = dr.HostId;
            reservationId = dr.ReservationId;
            startDate = dr.StartDate;
            endDate = dr.EndDate;
            comment = dr.Comment;
            status = dr.Status;
            repliedDate = dr.RepliedDate;
        }

        public DelayRequestViewModel()
        {
        }

        public DelayRequest ToDelayRequest()
        {
            DelayRequest request = new DelayRequest(guestId, hostId, reservationId, startDate, endDate, status, comment, repliedDate);
            request.Id = id;
            return request;
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
