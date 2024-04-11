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
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using static System.Net.Mime.MediaTypeNames;


namespace BookingApp.View.ViewModel
{
   
    public class AccommodationViewModel : INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }


        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }


        public HostService HostService { get; set; }
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



        public string Location => City + ", " + Country;
        public string Error => null;


        private Regex _NumberRegex = new Regex("^[0-9]+$");
        private Regex _ImageRegex = new Regex("^(?:https://(?:[^,]*,)?)*https://(?:[^,]+)?(?:,(?=https://)[^,]*)*$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                }

                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";

                }
                else if (columnName == "MinReservationDays")
                {
                    if (MinReservationDays < 0)
                        return "Min reservation days must be greater than 0";
                }
                else if (columnName == "MaxGuestNumber")
                {
                    if (MaxGuestNumber < 0)
                        return "Max guest number must be greater than 0";
                }
                else if (columnName == "ReservationDaysLimit")
                {

                    if (ReservationDaysLimit < 0)
                        return "Reservation days limit must be greater than 0";
                }


                return null;
            }
        }

        private readonly string[] _validatedProperties = { "City", "Name", "Country", "MinReservationDays", "MaxGuestNumbe", "ReservationDaysLimit" };

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

        public User User { get; set; }

        public Frame Frame { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public AccommodationsPage AccommodationsPage { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationViewModel()
        {
            reservationDaysLimit = 1;
          
        }
        public AccommodationViewModel(User user, Frame frame, AccommodationsPage accommodationsPage)
        {
            reservationDaysLimit = 1;
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            AccommodationService = new AccommodationService();
            AccommodationService.Subscribe(this);
            User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            
            Frame = frame;
            AccommodationReservationService = new AccommodationReservationService();
            HostService = new HostService();
            AccommodationsPage = accommodationsPage;
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
            type = GetAccommodationType();

            Accommodation a = new Accommodation(name, country, city, type, maxGuestNumber, minReservationDays, reservationDaysLimit, hostId);
            a.Id = id;
            a.UnavailableDates = unavailableDates;
            a.Pictures = picture;





            return a;

        }

        public AccommodationType GetAccommodationType()
        {
            AccommodationType type = AccommodationType.APARTMENT;

            if (isCheckedHouse)
            { type = AccommodationType.HOUSE; }
            else if (isCheckedCottage)
            {
                type = AccommodationType.COTTAGE;
            }

            return type;
        }

        public void Update()
        {
            Accommodations.Clear();
            List<AccommodationViewModel> superHostAccommodations = new List<AccommodationViewModel>();
            List<AccommodationViewModel> nonSuperHostAccommodations = new List<AccommodationViewModel>();

            SeparateAccommodations(AccommodationService, superHostAccommodations, nonSuperHostAccommodations);


            foreach (AccommodationViewModel superHostAccommodation in superHostAccommodations)
                Accommodations.Add(superHostAccommodation);

            foreach (AccommodationViewModel nonSuperHostAccommodation in nonSuperHostAccommodations)
                Accommodations.Add(nonSuperHostAccommodation);
        }
        public void SeparateAccommodations(AccommodationService accommodationService, List<AccommodationViewModel> superHostAccommodations, List<AccommodationViewModel> nonSuperHostAccommodations)
        {
            foreach (Accommodation accommodation in AccommodationService.GetAll())
            {

                AccommodationViewModel accommodationDTO = new AccommodationViewModel(accommodation);
                Host host = HostService.GetById(accommodation.HostId);
                HostService.BecomeSuperHost(host);
                accommodationDTO.IsSuperHost = host.IsSuperHost;

                if (accommodationDTO.IsSuperHost)
                    superHostAccommodations.Add(accommodationDTO);
                else
                    nonSuperHostAccommodations.Add(accommodationDTO);

                //MessageBox.Show(Accommodations[0].Type.ToString());
            }
        }

        public void ReserveButton_Click(object sender, RoutedEventArgs e)
        {


            Button button = sender as Button;
            SelectedAccommodation = button.DataContext as AccommodationViewModel;
            Frame.Content = new ReservationInfoPage(AccommodationService, SelectedAccommodation, AccommodationReservationService, User, Frame);




        }


        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> queries = new List<string>();
            queries.Add(AccommodationsPage.txtSearchName.Text); //nameQuery
            queries.Add(AccommodationsPage.txtSearchCity.Text); //cityQuery
            queries.Add(AccommodationsPage.txtSearchCountry.Text); //countryQuery
            queries.Add(AccommodationsPage.txtSearchType.Text); //typeQuery
            queries.Add(AccommodationsPage.txtSearchGuestNumber.Text); //guestQuery
            queries.Add(AccommodationsPage.txtSearchReservationDays.Text); //reservationQuery

            AccommodationsPage.AccommodationListBox.ItemsSource = SearchAccommodations(queries);




        }

        private List<AccommodationViewModel> SearchAccommodations(List<string> queries)
        {


            ObservableCollection<AccommodationViewModel> totalAccommodations = new ObservableCollection<AccommodationViewModel>();
            foreach (Accommodation accommodation in AccommodationService.GetAll())
                totalAccommodations.Add(new AccommodationViewModel(accommodation));

            List<AccommodationViewModel> searchResults = FilterAccommodations(totalAccommodations, queries);


            int totalItems = searchResults.Count;
            List<AccommodationViewModel> results = new List<AccommodationViewModel>();
            foreach (AccommodationViewModel accommodation in searchResults)
                results.Add(accommodation);

            return results;




        }

        private List<AccommodationViewModel> FilterAccommodations(ObservableCollection<AccommodationViewModel> totalAccommodations, List<string> queries)
        {
            List<AccommodationViewModel> filteredAccommodations = totalAccommodations.Where(accommodation => (string.IsNullOrEmpty(queries[0]) || accommodation.Name.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || accommodation.City.ToUpper().Contains(queries[1].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[2]) || accommodation.Country.ToUpper().Contains(queries[2].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[3]) || accommodation.Type.ToString().ToUpper().Contains(queries[3].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[4]) || Convert.ToInt32(queries[4]) <= accommodation.MaxGuestNumber) &&
                                                                           (string.IsNullOrEmpty(queries[5]) || Convert.ToInt32(queries[5]) >= accommodation.MinReservationDays)
                                                                           ).ToList();

            return filteredAccommodations;
        }
    }


}
