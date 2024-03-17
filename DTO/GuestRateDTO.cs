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

namespace BookingApp.DTO
{
    public class GuestRateDTO : INotifyPropertyChanged
    {


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

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Cleanliness")
                {
                    if (Cleanliness < 1 || Cleanliness > 5 )
                        return "Cleanliness must be from 1 to 5";
                }
                else if (columnName == "RulesFollowing")
                {
                    if (RulesFollowing < 0 || RulesFollowing > 5)
                        return "RulesFollowing must be from 1 to 5";
                }
                
                

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Cleanliness", "RulesFollowing" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }




        public GuestRateDTO() { }

        public GuestRateDTO(GuestRate gr) {

            guestId = gr.UserId;
            accommodationId = gr.AcommodationId;
            cleanliness = gr.Cleanliness;
            rulesFollowing = gr.RulesFollowing;
            additionalComment = gr.AdditionalComment;

        }

        public GuestRate toGuestRate()
        {
            GuestRate guestRate = new GuestRate(guestId, accommodationId, cleanliness, rulesFollowing, additionalComment);
            return guestRate;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
