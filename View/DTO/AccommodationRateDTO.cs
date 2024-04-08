﻿using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class AccommodationRateDTO : INotifyPropertyChanged
    {
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

        private int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (cleanliness != value)
                {

                    cleanliness = value;
                    OnPropertyChanged("Cleanliness");
                }
            }
        }

        private int correctness;
        public int Correctness
        {
            get { return correctness; }
            set
            {
                if (correctness != value)
                {

                    correctness = value;
                    OnPropertyChanged("Correctness");
                }
            }
        }

        private string additionalComment;
        public string AdditionalComment
        {
            get { return additionalComment; }
            set
            {
                if (additionalComment != value)
                {

                    additionalComment = value;
                    OnPropertyChanged("AdditionalComment");
                }
            }
        }

        private List<string> images = new List<string>();
        public List<string> Images
        {
            get { return images; }
            set
            {
                if (images != value)
                {
                    images = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationRateDTO()
        {

        }

        public AccommodationRateDTO(AccommodationRateDTO ar)
        {
            reservationId = ar.ReservationId;
            guestId = ar.GuestId;
            hostId = ar.HostId;
            cleanliness = ar.Cleanliness;
            correctness = ar.Correctness;
            additionalComment = ar.AdditionalComment;
        }

        public AccommodationRate ToAccommodationRate()
        {
            AccommodationRate accommodationRate = new AccommodationRate(reservationId, guestId, hostId, cleanliness, correctness, additionalComment);
            accommodationRate.Images = images;
            return accommodationRate;
        }

    }
}