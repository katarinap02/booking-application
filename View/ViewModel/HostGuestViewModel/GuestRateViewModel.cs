using BookingApp.Model;
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

        private bool isChecked2;
        public bool IsChecked2
        {
            get { return isChecked2; }
            set
            {
                if (isChecked2 != value)
                {

                    isChecked2 = value;
                    OnPropertyChanged("IsChecked2");
                }
            }
        }

        private bool isChecked3;
        public bool IsChecked3
        {
            get { return isChecked3; }
            set
            {
                if (isChecked3 != value)
                {

                    isChecked3 = value;
                    OnPropertyChanged("IsChecked3");
                }
            }
        }
        private bool isChecked4;
        public bool IsChecked4
        {
            get { return isChecked4; }
            set
            {
                if (isChecked4 != value)
                {

                    isChecked4 = value;
                    OnPropertyChanged("IsChecked4");
                }
            }
        }
        private bool isChecked5;
        public bool IsChecked5
        {
            get { return isChecked5; }
            set
            {
                if (isChecked5 != value)
                {

                    isChecked5 = value;
                    OnPropertyChanged("IsChecked5");
                }
            }
        }

        private bool isCheckedRules2;
        public bool IsCheckedRules2
        {
            get { return isCheckedRules2; }
            set
            {
                if (isCheckedRules2 != value)
                {

                    isCheckedRules2 = value;
                    OnPropertyChanged("IsCheckedRules2");
                }
            }
        }

        private bool isCheckedRules3;
        public bool IsCheckedRules3
        {
            get { return isCheckedRules3; }
            set
            {
                if (isCheckedRules3 != value)
                {

                    isCheckedRules3 = value;
                    OnPropertyChanged("IsCheckedRules3");
                }
            }
        }
        private bool isCheckedRules4;
        public bool IsCheckedRules4
        {
            get { return isCheckedRules4; }
            set
            {
                if (isCheckedRules4 != value)
                {

                    isCheckedRules4 = value;
                    OnPropertyChanged("IsCheckedRules4");
                }
            }
        }
        private bool isCheckedRules5;
        public bool IsCheckedRules5
        {
            get { return isCheckedRules5; }
            set
            {
                if (isCheckedRules5 != value)
                {

                    isCheckedRules5 = value;
                    OnPropertyChanged("IsCheckedRules5");
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

        public int ConvertToIntChecked(bool Check2,  bool Check3, bool Check4, bool Check5)
        {
            if (Check2) {  return 2;}
            if (Check3) { return 3; }
            if (Check4) { return 4;}
            if (Check5) { return 5;}
            return 1;
        }

        public GuestRate toGuestRate()
        {
            if(cleanliness == 0 || rulesFollowing == 0) {
                cleanliness = ConvertToIntChecked(isChecked2, isChecked3, isChecked4, isChecked5);
            rulesFollowing = ConvertToIntChecked(isCheckedRules2, isCheckedRules3, isCheckedRules4, isCheckedRules5);
            }
            
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
