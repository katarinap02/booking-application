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
    public class DelayRequestViewModel : INotifyPropertyChanged
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

        public string DateRange => startDate.ToString("MM/dd/yyyy") + " -> " + endDate.ToString("MM/dd/yyyy");

        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        
    }
}
