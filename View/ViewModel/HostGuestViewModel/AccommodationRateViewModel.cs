using BookingApp.Model;
using BookingApp.View.GuestTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.View.ViewModel
{
    public class AccommodationRateViewModel : INotifyPropertyChanged
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

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
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

        private string guestUsername;
        public string GuestUsername
        {
            get { return guestUsername; }
            set
            {
                if (guestUsername != value)
                {
                    guestUsername = value;
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

        private string onePicture;
        public string OnePicture
        {
            get { return onePicture; }
            set
            {
                if (onePicture != value)
                {

                    onePicture = value;
                    OnPropertyChanged("OnePicture");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PathConverter PathConverter { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       

        public AccommodationRateViewModel() { }

        public AccommodationRateViewModel(AccommodationRate ar, AccommodationReservation ac, User us)
        {
            reservationId = ar.ReservationId;
            guestId = ar.GuestId;
            hostId = ar.HostId;
            cleanliness = ar.Cleanliness;
            correctness = ar.Correctness;
            additionalComment = ar.AdditionalComment;
            guestUsername = us.Username;
            accommodationName = ac.Name;
            PathConverter = new PathConverter();
            if (ar.Images.Count != 0)
                OnePicture = PathConverter.ConvertToRelativePath(ar.Images[0]);
            
            else
                OnePicture = "../../../Resources/Images/no_image.jpg";
            

        }

        public AccommodationRateViewModel(AccommodationRateViewModel ar)
        {
            reservationId = ar.ReservationId;
            guestId = ar.GuestId;
            hostId = ar.HostId;
            cleanliness = ar.Cleanliness;
            correctness = ar.Correctness;
            additionalComment = ar.AdditionalComment;
            onePicture = ar.OnePicture;
        }

        public AccommodationRate ToAccommodationRate()
        {
            AccommodationRate accommodationRate = new AccommodationRate(reservationId, guestId, hostId, cleanliness, correctness, additionalComment);
            accommodationRate.Images = images;
            return accommodationRate;
        }

    }
}
