using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Repository;
using BookingApp.View.TouristWindows;
using BookingApp.WPF.View.TouristWindows;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourViewModel : INotifyPropertyChanged
    {
        public TourService _tourService { get; set; }
        public GuideRateService _guideRateService { get; set; }
        public UserService _userService { get; set; }

        public ObservableCollection<string> CountriesSearch { get; set; }
        public ObservableCollection<string> CitiesSearch { get; set; }
        public ObservableCollection<string> Languages { get; set; }

        public ICommand RateCommand { get; set; }
        public ObservableCollection<TourViewModel> Tours { get; set; }

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
                if (City != value)
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
                if (Country != value)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public string Location
        {
            get { return city+","+ country; }            
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int maxTourists;
        public int MaxTourists
        {
            get { return maxTourists; }
            set
            {
                if (maxTourists != value)
                {
                    maxTourists = value;
                    OnPropertyChanged("MaxTourists");
                }
            }
        }

        private List<string> checkpoints = new List<string>();
        public List<string> Checkpoints
        {
            get { return checkpoints; }
            set
            {
                if (checkpoints != value)
                {
                    checkpoints = value;
                    OnPropertyChanged("Checkpoints");
                }
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private float duration;
        public float Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }        

        private List<string> pictures = new List<string>();
        public List<string> Pictures
        {
            get { return pictures; }
            set
            {
                if (pictures != value)
                {
                    pictures = value;
                    OnPropertyChanged("Pictures");
                }
            }
        }

        private TourStatus status;
        public TourStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int groupId;
        public int GroupId
        {
            get { return groupId; }
            set
            {
                if (groupId != value)
                {
                    groupId = value;
                    OnPropertyChanged("GroupId");
                }
            }
        }

        private int currentCheckpoint;
        public int CurrentCheckpoint
        {
            get { return currentCheckpoint; }
            set
            {
                if (currentCheckpoint != value)
                {
                    currentCheckpoint = value;
                    OnPropertyChanged("CurrentCheckpoint");
                }
            }
        }

        private int availablePlaces;
        public int AvailablePlaces
        {
            get
            {
                return availablePlaces;
            }
            set
            {
                if (availablePlaces != value)
                {
                    availablePlaces = value;
                    OnPropertyChanged(nameof(AvailablePlaces));
                }
            }
        }

        private int guideId;
        public int GuideId
        {
            get { return guideId; }
            set
            {
                if (guideId != value)
                {
                    guideId = value;
                    OnPropertyChanged("GuideId");
                }
            }
        }

        // akos
        private string _durationSearch;
        public string DurationSearch
        {
            get
            {
                return _durationSearch;
            }
            set
            {
                if (value != _durationSearch)
                {
                    _durationSearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(DurationSearch));
                }
            }
        }

        private string _peopleSearch;
        public string PeopleSearch
        {
            get
            {
                return _peopleSearch;
            }
            set
            {
                if (value != _peopleSearch)
                {
                    _peopleSearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(PeopleSearch));
                }
            }
        }

        private string _citySearch;
        public string CitySearch
        {
            get
            {
                return _citySearch;
            }
            set
            {
                if (value != _citySearch)
                {
                    _citySearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(CitySearch));
                }
            }
        }

        private string _countrySearch;
        public string CountrySearch
        {
            get
            {
                return _countrySearch;
            }
            set
            {
                if (value != _countrySearch)
                {
                    _countrySearch = value ?? string.Empty;
                    LoadCitiesFromCSV();
                    OnPropertyChanged(nameof(CountrySearch));
                }
            }
        }

        private string _languageSearch;
        public string LanguageSearch
        {
            get
            {
                return _languageSearch;
            }
            set
            {
                if (value != _languageSearch)
                {
                    _languageSearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(LanguageSearch));
                }
            }
        }

        private int _maximumValuePeoples;
        public int MaximumValuePeoples
        {
            get
            {
                return _maximumValuePeoples;
            }
            set
            {
                if (value != _maximumValuePeoples)
                {
                    _maximumValuePeoples = value;
                    OnPropertyChanged(nameof(MaximumValuePeoples));
                }
            }
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int getUserId(string Username)
        {

            UserId = _userService.GetByUsername(Username).Id;
            return UserId;
        }


        public void initializeAllTours()
        {
            MaximumValuePeoples = _tourService.FindMaxNumberOfParticipants();
            Languages.Add("");
            CountriesSearch.Add("");
            LoadCountriesFromCSV();
            LoadLanguagesFromCSV();
        }

        private void LoadLanguagesFromCSV()
        {
            string csvFilePath = "../../../Resources/Data/languages.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    Languages.Add(values[0]);
                }
            }
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
            CitySearch = CitiesSearch[0];
        }

        //******************************* ZA ALLTOURS PAGE 
        // BITNO PREBACITI U NOVI FAJL, OVO JE DTO
        public void BookButton(TourViewModel selectedTour)
        {
            if (selectedTour.availablePlaces > 0)
            {
                TourNumberOfParticipantsWindow tourNumberOfParticipantsWindow = new TourNumberOfParticipantsWindow(selectedTour, UserId);
                tourNumberOfParticipantsWindow.ShowDialog();
            }
            else
            {
                TourNoAvailablePlacesWindow tourNoAvailablePlacesWindow = new TourNoAvailablePlacesWindow(selectedTour, UserId);
                tourNoAvailablePlacesWindow.ShowDialog();
            }

            if (Tours.Count != _tourService.ToursCount())
                RefreshAllToursDataGrid(true);
            else
                RefreshAllToursDataGrid(false);
        }

        public void DetailsButton(TourViewModel selectedTour, bool isMyTour)
        {
            TourDetailsWindow tourDetailsWindow = new TourDetailsWindow(selectedTour, isMyTour, _userService.GetById(UserId).Username);
            tourDetailsWindow.ShowDialog();
        }

        public void NotificationButton()
        {
            TouristNotificationWindow touristNotificationWindow = new TouristNotificationWindow(UserId);
            touristNotificationWindow.ShowDialog();
        }

        private List<TourViewModel>? Search()
        {
            DurationSearch = SetToEmptyIfNull(DurationSearch);
            CitySearch = SetToEmptyIfNull(CitySearch);
            CountrySearch = SetToEmptyIfNull(CountrySearch);
            LanguageSearch = SetToEmptyIfNull(LanguageSearch);
            PeopleSearch = SetToEmptyIfNull(PeopleSearch);


            DurationSearch = EmptyStringToZero(DurationSearch);
            PeopleSearch = EmptyStringToZero(PeopleSearch);


            Tour tour = new Tour("", CitySearch, CountrySearch, "", LanguageSearch, int.Parse(PeopleSearch),
                                new List<string>(), new DateTime(), float.Parse(DurationSearch), new List<string>());
            List<TourViewModel>? foundTours = ToTourViewModel(_tourService.SearchTours(tour));
            return foundTours;
        }

        public void SearchButton()
        {
            Tours.Clear();
            List<TourViewModel>? foundTours = Search();

            if (foundTours != null)
                foreach (TourViewModel t in foundTours)
                    Tours.Add(t);
        }

        public void ResetButton()
        {
            CountrySearch = string.Empty;
            CitySearch = string.Empty;
            LanguageSearch = string.Empty;
            DurationSearch = "0";
            PeopleSearch = "0";
        }

        private string SetToEmptyIfNull(string search)
        {
            if (search == null)
            {
                return string.Empty;
            }
            return search;
        }

        private string EmptyStringToZero(string text)
        {
            if (text == string.Empty)
            {
                return "0";
            }

            return text;
        }

        public void RefreshAllToursDataGrid(bool withSearch)
        {
            Tours.Clear();
            if (withSearch)
            {
                List<TourViewModel>? foundTours = Search();

                if (foundTours != null)
                    foreach (TourViewModel t in foundTours)
                        Tours.Add(t);
            }
            else
            {
                List<TourViewModel> allTours = ToTourViewModel(_tourService.GetAllTours());
                foreach (TourViewModel tour in allTours)
                    Tours.Add(tour);
            }
        }
        public List<TourViewModel> ToTourViewModel(List<Tour> Tours)
        {
            // creating list from Tour to TourViewModel
            List<TourViewModel> ToursViewModel = new List<TourViewModel>();
            foreach (Tour tour in Tours)
            {
                ToursViewModel.Add(new TourViewModel(tour));
            }
            return ToursViewModel;
        }
        //**********************************************

        // ******************* ZA MY TOURS PAGE

        public void RefreshMyTours()
        {
            Tours.Clear();
            List<TourViewModel> tours = ToTourViewModel(_tourService.FindMyTours(UserId));
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }
        public void RefreshEndedTours()
        {
            Tours.Clear();
            List<TourViewModel> tours = ToTourViewModel(_tourService.FindMyEndedTours(UserId));
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }

        public bool IsRated()
        {
            return _guideRateService.IsRated(SelectedTour.Id);
        }

        private void ExecuteRateCommand(object obj)
        {
            GuideRateWindow guideRateWindow = new GuideRateWindow(SelectedTour, UserId);
            guideRateWindow.ShowDialog();
        }

        private bool CanExecuteRateCommand(object obj)
        {
            if (IsRated())
            {
                MessageBox.Show("This tour is already rated");
                return false;
            }
            return true;
        }

        public TourViewModel()
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());

            _guideRateService = new GuideRateService(Injector.Injector.CreateInstance<IGuideRateRepository>());
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            Tours = new ObservableCollection<TourViewModel>();

            CountriesSearch = new ObservableCollection<string>();
            CitiesSearch = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();
            RateCommand = new RelayCommand(ExecuteRateCommand, CanExecuteRateCommand);
        }

        public TourViewModel(Tour tour)
        {
            name = tour.Name;
            id = tour.Id;
            city = tour.City;
            duration = tour.Duration;
            language = tour.Language;
            description = tour.Description;
            status = tour.Status;
            maxTourists = tour.MaxTourists;
            date = tour.Date;
            checkpoints = tour.Checkpoints;
            pictures = tour.Pictures;
            country = tour.Country;
            currentCheckpoint = tour.currentCheckpoint;
            availablePlaces = tour.AvailablePlaces;
            guideId = tour.GuideId;
        }

        public Tour ToTour()
        {
            Tour tour = new Tour(name, city, country, description, language, maxTourists, checkpoints, date, duration, pictures);
            tour.Id = id;
            tour.Status = status;
            tour.GroupId = groupId;
            tour.GuideId = guideId;
            tour.AvailablePlaces = availablePlaces;
            return tour;
        }

    }
}
