using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.DTO
{
    public class AccommodationReservationDTO : INotifyPropertyChanged
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

        public AccommodationReservationDTO() { }

        public AccommodationReservationDTO(AccommodationReservation ac)
        {
            guestId = ac.GuestId;
            accommodationId = ac.AccommodationId;
            startDate = ac.StartDate;
            endDate = ac.EndDate;




        }

      //  public Accommodation ToAccommodation()
       // {

         //   AccommodationReservation a = new AccommodationReservation(guestId, accommodationId, startDate, endDate);
          //  return a;

      //  }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
