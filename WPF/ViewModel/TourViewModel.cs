using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.TouristWindows;

namespace BookingApp.WPF.ViewModel
{
    public class TourViewModel : INotifyPropertyChanged
    {
        private readonly TouristService _touristService;
        private readonly GuideRateService _guideRateService;
        private readonly UserService _userService;

        public ObservableCollection<TourViewModel> Tours { get; set; }
        public ObservableCollection<Checkpoint> CheckpointWithColors { get; set; }

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

        private Visibility _pdfPanel;
        public Visibility PdfPanel
        {
            get
            {
                return _pdfPanel;
            }
            set
            {
                if (_pdfPanel != value)
                {
                    _pdfPanel = value;
                    OnPropertyChanged(nameof(PdfPanel));
                }
            }
        }

        private SolidColorBrush _availablePlacesColor;
        public SolidColorBrush AvailablePlacesColor
        {
            get
            {
                return _availablePlacesColor;
            }
            set
            {
                if (value != _availablePlacesColor)
                {
                    _availablePlacesColor = value;
                    OnPropertyChanged(nameof(AvailablePlacesColor));
                }
            }
        }

        private HorizontalAlignment _closeButtonAlignmentNumberOfParticipants;
        public HorizontalAlignment CloseButtonalignmentNumberOfParticipants
        {
            get
            {
                return _closeButtonAlignmentNumberOfParticipants;
            }
            set
            {
                if (_closeButtonAlignmentNumberOfParticipants != value)
                {
                    _closeButtonAlignmentNumberOfParticipants = value;
                    OnPropertyChanged(nameof(CloseButtonalignmentNumberOfParticipants));
                }
            }
        }

        private Visibility _confirmButtonVisibilityNumberOfParticipants;
        public Visibility ConfirmButtonVisibilityNumberOfParticipants
        {
            get
            {
                return _confirmButtonVisibilityNumberOfParticipants;
            }
            set
            {
                if (_confirmButtonVisibilityNumberOfParticipants != value)
                {
                    _confirmButtonVisibilityNumberOfParticipants = value;
                    OnPropertyChanged(nameof(ConfirmButtonVisibilityNumberOfParticipants));
                }
            }
        }

        private int _insertedNumberOfParticipants;
        public int InsertedNumberOfParticipants
        {
            get
            {
                return _insertedNumberOfParticipants;
            }
            set
            {
                if (value != _insertedNumberOfParticipants)
                {
                    _insertedNumberOfParticipants = value;
                    OnPropertyChanged(nameof(InsertedNumberOfParticipants));
                }
            }
        }

        private Visibility _dataTabVisibility;
        public Visibility DataTabVisibility
        {
            get
            {
                return _dataTabVisibility;
            }
            set
            {
                if (_dataTabVisibility != value)
                {
                    _dataTabVisibility = value;
                    OnPropertyChanged(nameof(DataTabVisibility));
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
            MaximumValuePeoples = _touristService.FindMaxNumberOfParticipants();
        }

        //******************************* ZA ALLTOURS PAGE
        public void BookButton(TourViewModel selectedTour)
        {
            TourNumberOfParticipantsWindow tourNumberOfParticipantsWindow = new TourNumberOfParticipantsWindow(selectedTour, UserId);
            tourNumberOfParticipantsWindow.ShowDialog();

            if (Tours.Count != _touristService.ToursCount())
                RefreshAllToursDataGrid(true);
            else
                RefreshAllToursDataGrid(false);
        }

        public void DetailsButton(TourViewModel selectedTour, bool isMyTour)
        {
            TourDetailsWindow tourDetailsWindow = new TourDetailsWindow(selectedTour, isMyTour);
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
            List<TourViewModel>? foundTours = _touristService.SearchTours(tour);
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
                List<TourViewModel> allTours = _touristService.GetAllTours();
                foreach (TourViewModel tour in allTours)
                    Tours.Add(tour);
            }

        }

        //**********************************************

        // ******************* ZA MY TOURS PAGE

        public void RefreshMyTours()
        {
            Tours.Clear();
            List<TourViewModel> tours = _touristService.FindMyTours(UserId);
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }
        public void RefreshEndedTours()
        {
            Tours.Clear();
            List<TourViewModel> tours = _touristService.FindMyEndedTours(UserId);
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }

        public bool IsRated()
        {
            return _guideRateService.IsRated(SelectedTour.Id);
        }



        //*********************** TOUR DETAILS

        public void TourDetailsWindowInitialization(bool IsMyTour)
        {

            SolidColorBrush activeColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#56707a"));
            SolidColorBrush inactiveColor = Brushes.Gray;
            CheckpointWithColors.Clear();
            foreach (var checkpoint in SelectedTour.Checkpoints)
            {
                CheckpointWithColors.Add(new Checkpoint { Name = checkpoint, IndicatorColor = inactiveColor });
            }

            PdfPanel = IsMyTour ? Visibility.Visible : PdfPanel;

            int checkpointIndex = SelectedTour.CurrentCheckpoint;
            for (int i = 0; i < SelectedTour.Checkpoints.Count; i++)
            {
                if (i == checkpointIndex)
                {
                    CheckpointWithColors[i].IndicatorColor = activeColor;
                }
                else
                {
                    CheckpointWithColors[i].IndicatorColor = inactiveColor;
                }
            }
            // images
            if (SelectedTour.Pictures != null)
            {
                for (int i = 0; i < SelectedTour.Pictures.Count; i++)
                {
                    SelectedTour.Pictures[i] = "../../" + SelectedTour.Pictures[i];
                }
            }
        }

        //********************TourNumberOfParticipantsWindow
        public void RefreshToursByCity()
        {
            Tours.Clear();
            List<TourViewModel> tours = _touristService.GetTourByCityWithAvailablePlaces(SelectedTour.City);
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }

        public (int, int) InitializeNumberOfParticipantsWindow()
        {
            if (SelectedTour.AvailablePlaces == 0)
            {
                AvailablePlacesColor = Brushes.Red;
                if (Tours.Count > 0)
                {
                    DataTabVisibility = Visibility.Visible;
                }
                CloseButtonalignmentNumberOfParticipants = HorizontalAlignment.Center;
                ConfirmButtonVisibilityNumberOfParticipants = Visibility.Collapsed;
                MessageBox.Show("No more places for the selected tour, please select another one!");
                AvailablePlaces = _touristService.FindMaxNumberOfParticipants(Tours.ToList());
                // returnujemo big window size, widht, height
                return (800, 600);
            }
            AvailablePlacesColor = Brushes.Green;

            DataTabVisibility = Visibility.Collapsed;
            CloseButtonalignmentNumberOfParticipants = HorizontalAlignment.Left;
            ConfirmButtonVisibilityNumberOfParticipants = Visibility.Visible;
            // small
            return (600, 220);
        }
        public void ConfirmNumberOfParticipants()
        {
            if (InsertedNumberOfParticipants > SelectedTour.AvailablePlaces)
            {
                MessageBox.Show("Not enough places for the reservation");
                return;
            }
            if (InsertedNumberOfParticipants == 0)
                InsertedNumberOfParticipants = 1;
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants, UserId);
            tourReservationWindow.ShowDialog();
        }

        public void BookNumberOfParticipants()
        {
            if (InsertedNumberOfParticipants == 0)
                InsertedNumberOfParticipants = 1;
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants, UserId);
            tourReservationWindow.ShowDialog();
        }

        public TourViewModel()
        {
            _touristService = new TouristService();
            _guideRateService = new GuideRateService();
            _userService = new UserService();
            Tours = new ObservableCollection<TourViewModel>();
            CheckpointWithColors = new ObservableCollection<Checkpoint>();
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

    //************ OVO JE ZA TOUR DETAILS
    public class Checkpoint : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Brush indicatorColor;
        public Brush IndicatorColor
        {
            get { return indicatorColor; }
            set { indicatorColor = value; NotifyPropertyChanged(nameof(IndicatorColor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
