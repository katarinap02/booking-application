using BookingApp.Domain.Model.Rates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class GuestRateViewModel : INotifyPropertyChanged
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

        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }
            set
            {
                if (accommodationId != value)
                {

                    accommodationId = value;
                    OnPropertyChanged("AccommodationId");
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

        private int rulesFollowing;
        public int RulesFollowing
        {
            get { return rulesFollowing; }
            set
            {
                if (rulesFollowing != value)
                {

                    rulesFollowing = value;
                    OnPropertyChanged("RulesFollowing");
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
        public GuestRateViewModel() { }

        public GuestRateViewModel(GuestRate gr)
        {

            reservationId = gr.ReservationId;
            guestId = gr.UserId;
            accommodationId = gr.AcommodationId;
            cleanliness = gr.Cleanliness;
            rulesFollowing = gr.RulesFollowing;
            additionalComment = gr.AdditionalComment;

        }

        public GuestRate toGuestRate()
        {
            GuestRate guestRate = new GuestRate(reservationId, guestId, accommodationId, cleanliness, rulesFollowing, additionalComment);
            return guestRate;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
