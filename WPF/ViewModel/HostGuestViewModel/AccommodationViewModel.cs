using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Observer;

using BookingApp.Repository;
using BookingApp.View.GuestPages;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{

    public class AccommodationViewModel : INotifyPropertyChanged
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

        private int minReservationDays;
        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if (minReservationDays != value)
                {

                    minReservationDays = value;
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




        private List<string> picture = new List<string>();
        public List<string> Picture
        {
            get { return picture; }
            set
            {
                if (picture != value)
                {
                    picture = value;
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


        private List<CalendarDateRange> unavailableDates = new List<CalendarDateRange>();
        public List<CalendarDateRange> UnavailableDates
        {
            get { return unavailableDates; }
            set
            {
                if (unavailableDates != value)
                {
                    unavailableDates = value;
                    OnPropertyChanged(nameof(UnavailableDates));
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

        private bool isSuperHost;
        public bool IsSuperHost
        {
            get { return isSuperHost; }
            set
            {
                if (isSuperHost != value)
                {
                    isSuperHost = value;
                    OnPropertyChanged("IsSuperHost");
                }
            }
        }

        public string ConvertToRelativePath(string inputPath)
        {

            string pattern = @"\\";


            string replacedPath = Regex.Replace(inputPath, pattern, "/");


            if (replacedPath.StartsWith("Resources/Images/"))
            {
                replacedPath = "../../" + replacedPath;
            }

            return replacedPath;
        }

        public string Location => City.ToString() + ", " + Country.ToString();

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationViewModel()
        {
            reservationDaysLimit = 1;

        }

        public string FirstPicture => "../" + OnePicture;


        private AccommodationRateService accommodationRateService = new AccommodationRateService();
        //   public string Rate => accommodationRateService.GetAverageRate(Id).ToString();

        public AccommodationViewModel(Accommodation accommodation)
        {
            id = accommodation.Id;
            name = accommodation.Name;
            city = accommodation.City;
            country = accommodation.Country;
            type = accommodation.Type;
            MaxGuestNumber = accommodation.MaxGuestNumber;
            MinReservationDays = accommodation.MinReservationDays;
            ReservationDaysLimit = accommodation.ReservationDaysLimit;
            UnavailableDates = accommodation.UnavailableDates;

            hostId = accommodation.HostId;

            if (accommodation.Pictures.Count != 0)
            {
                OnePicture = ConvertToRelativePath(accommodation.Pictures[0]);
            }
            else
            {
                OnePicture = "../../Resources/Images/no_image.jpg";
            }





        }

        public Accommodation ToAccommodation()
        {

            Accommodation a = new Accommodation(name, country, city, type, maxGuestNumber, minReservationDays, reservationDaysLimit, hostId);
            a.Id = id;
            a.UnavailableDates = unavailableDates;
            a.Pictures = picture;

            return a;

        }


    }


}
