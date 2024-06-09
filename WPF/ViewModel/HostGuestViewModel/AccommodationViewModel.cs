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
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;

using BookingApp.Repository;
using BookingApp.View.GuestPages;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{

    public class AccommodationViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> CountriesSearch { get; set; }

        public ObservableCollection<string> CitiesSearch {  get; set; } 

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

        private string countrySearch;
        public string CountrySearch
        {
            get
            {
                return countrySearch;
            }
            set
            {
                if (value != countrySearch)
                {
                    countrySearch = value ?? string.Empty;
                    LoadCitiesFromCSV();
                    OnPropertyChanged(nameof(CountrySearch));
                }
            }
        }

        private string citySearch;
        public string CitySearch
        {
            get
            {
                return citySearch;
            }
            set
            {
                if (value != citySearch)
                {
                    citySearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(CitySearch));
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

        private bool isCheckedHouse;
        public bool IsCheckedHouse
        {
            get { return isCheckedHouse; }
            set
            {
                if (isCheckedHouse != value)
                {
                    isCheckedHouse = value;
                    OnPropertyChanged("IsCheckedHouse");
                }
            }
        }

        private bool isLeastPopular;
        public bool IsLeastPopular
        {
            get { return isLeastPopular; }
            set
            {
                if (isLeastPopular != value)
                {
                    isLeastPopular = value;
                    OnPropertyChanged("IsLeastPopular");
                }
            }
        }

        private bool isMostPopular;
        public bool IsMostPopular
        {
            get { return isMostPopular; }
            set
            {
                if (isMostPopular != value)
                {
                    isMostPopular = value;
                    OnPropertyChanged("IsMostPopular");
                }
            }
        }

        private bool isCheckedCottage;
        public bool IsCheckedCottage
        {
            get { return isCheckedCottage; }
            set
            {
                if (isCheckedCottage != value)
                {
                    isCheckedCottage = value;
                    OnPropertyChanged("IsCheckedCottage");
                }
            }
        }

        private bool isCheckedApartment;
        public bool IsCheckedApartment
        {
            get { return isCheckedApartment; }
            set
            {
                if (isCheckedApartment != value)
                {
                    isCheckedApartment = value;
                    OnPropertyChanged("IsCheckedApartment");
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
                    OnPropertyChanged("MinReservationDays");
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

        public int NumberOfPictures;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationViewModel()
        {
            reservationDaysLimit = 1;
            isCheckedCottage = false;
            isCheckedCottage = false;
            isCheckedApartment = true;
            isLeastPopular = false;
            isMostPopular = false;
            CountriesSearch = new ObservableCollection<string>();
            CitiesSearch = new ObservableCollection<string>();

        }

        

        public string FirstPicture => "../" + OnePicture;


        private AccommodationRateService accommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        public string Rate => Round(accommodationRateService.GetAverageRate(Id)).ToString();

        

        
        private double Round(double v)
        {
            return Math.Round(v, 2);
        }


        public void InitializeAllLocations(String country = "")
        {
            if(country== "")
            CountriesSearch.Add("");
            else
                CountriesSearch.Add(country);
            LoadCountriesFromCSV();
        }
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
            NumberOfPictures = accommodation.Pictures.Count;
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

        

        private AccommodationType ConvertToType()
        { 
            if(isCheckedHouse)
            {
                return AccommodationType.HOUSE;
            }
            if(isCheckedCottage)
            {
                return AccommodationType.COTTAGE;
            }
            return AccommodationType.APARTMENT;
        }

        public Accommodation ToAccommodation()
        {
            if(type == AccommodationType.APARTMENT && (isCheckedCottage || isCheckedHouse || isCheckedApartment)) {
                type = ConvertToType();
            }

            Accommodation a = new Accommodation(name, countrySearch, citySearch, type, maxGuestNumber, minReservationDays, reservationDaysLimit, hostId);
            a.Id = id;
            a.UnavailableDates = unavailableDates;
            a.Pictures = picture;

            return a;

        }

        public Accommodation ToAccommodationWithoutSearch()
        {
            if (type == AccommodationType.APARTMENT && (isCheckedCottage || isCheckedHouse))
            {
                type = ConvertToType();
            }

            Accommodation a = new Accommodation(name, country, city, type, maxGuestNumber, minReservationDays, reservationDaysLimit, hostId);
            a.Id = id;
            a.UnavailableDates = unavailableDates;
            a.Pictures = picture;

            return a;

        }

        private void LoadCountriesFromCSV()
        {
            string csvFilePath = "../../../Resources/Data/european_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    CountriesSearch.Add(values[0]);
                }
            }
        }

        private void LoadCitiesFromCSV()
        {
            CitiesSearch.Clear();
            CitiesSearch.Add("");
            string csvFilePath = "../../../Resources/Data/european_cities_and_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[1].Equals(CountrySearch))
                        CitiesSearch.Add(values[0]);
                }
            }
           // CitySearch = CitiesSearch[0];
        }


    }


}
