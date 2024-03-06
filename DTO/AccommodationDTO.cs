using System;
using System.Collections.Generic;
using System.ComponentModel;
using BookingApp.Model;


namespace BookingApp.DTO
{
    public class AccommodationDTO : INotifyPropertyChanged
    {
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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private AccommodationType type;
        public AccommodationType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private int maxGuestNumber;
        public int MaxGuestNumber
        {
            get { return maxGuestNumber; }
            set
            {
                if (maxGuestNumber != value)
                {
                    maxGuestNumber = value;
                    OnPropertyChanged("MaxGuestNumber");
                }
            }
        }

        private int minReservationNumber;
        public int MinReservationNumber
        {
            get { return minReservationNumber; }
            set
            {
                if (minReservationNumber != value)
                {
                    minReservationNumber = value;
                    OnPropertyChanged("MinReservationNumber");
                }
            }
        }

        private int reservationDaysLimit;
        public int ReservationDaysLimit
        {
            get { return reservationDaysLimit; }
            set
            {
                if (reservationDaysLimit != value)
                {
                    reservationDaysLimit = value;
                    OnPropertyChanged("ReservationDaysLimit");
                }
            }
        }


        private List<string> images;
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

        public AccommodationDTO(Accommodation accommodation)
        {
            id = accommodation.Id;
            name = accommodation.Name;
            city = accommodation.City;
            country = accommodation.Country;
            type = (AccommodationType)accommodation.Type;
            MaxGuestNumber = accommodation.MaxGuestNumber;
            MinReservationNumber = accommodation.MinReservationNumber;
            ReservationDaysLimit = accommodation.ReservationDaysLimit;



        }


    }

    public enum AccommodationType { APARTMENT, HOUSE, COTTAGE }
}
