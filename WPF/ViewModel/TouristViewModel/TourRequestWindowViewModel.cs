using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.TouristWindows;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourRequestWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly TourRequestService _tourRequestService;
        private readonly TourParticipantService _tourParticipantService;
        private readonly TourReservationService _tourReservationService;
        private readonly RequestedTourParticipantService _requestedTourParticipantService;
        public List<TourParticipantViewModel> TourParticipantDTOs { get; set; }
        public List<TourParticipantViewModel> TourParticipantsListBox { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        public ICommand SelectTourRequestType { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public RelayCommand AddParticipantCommand { get; set; }

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        private ICommand _removeParticipantCommand;
        public ICommand RemoveParticipantCommand
        {
            get
            {
                if (_removeParticipantCommand == null)
                {
                    _removeParticipantCommand = new RelayCommand(
                        param => RemoveParticipant(param),
                        param => CanRemoveParticipant());
                }
                return _removeParticipantCommand;
            }
        }

        private ICommand _saveToCsvCommand;
        public ICommand SaveToCsvCommand
        {
            get
            {
                if (_saveToCsvCommand == null)
                {
                    _saveToCsvCommand = new RelayCommand(
                        param => SaveToCsv(),
                        param => true);
                }
                return _saveToCsvCommand;
            }
        }
        private void RemoveParticipant(object participant)
        {
            TourParticipantDTOs.Remove(participant as TourParticipantViewModel);
            TourParticipantsListBox.Remove(participant as TourParticipantViewModel);
            ParticipantsListBox.Clear();
            foreach (var p in TourParticipantsListBox)
            {
                ParticipantsListBox.Add(p);
            }
        }
        private bool CanRemoveParticipant()
        {
            return ParticipantsListBox.Count > 0;
        }

        private ObservableCollection<TourParticipantViewModel> _participantsListBox;
        public ObservableCollection<TourParticipantViewModel> ParticipantsListBox
        {
            get { return _participantsListBox; }
            set
            {
                _participantsListBox = value;
                OnPropertyChanged("ParticipantsListBox");
            }
        }
        private TourParticipantViewModel _selectedParticipant;
        public TourParticipantViewModel SelectedParticipant
        {
            get
            {
                return _selectedParticipant;
            }
            set
            {
                if (_selectedParticipant != value)
                {
                    _selectedParticipant = value;
                    OnPropertyChanged(nameof(SelectedParticipant));
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

        private string _country;

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                    LoadCitiesFromCSV();
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (_language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private TourRequestStatus _status;
        public TourRequestStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        private int _participantCount;
        public int ParticipantCount
        {
            get
            {
                return _participantCount;
            }
            set
            {
                if (_participantCount != value)
                {
                    _participantCount = value;
                    OnPropertyChanged(nameof(ParticipantCount));
                }
            }
        }

        private DateTime _selectedStartDate;
        public DateTime SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                if (_selectedStartDate != value)
                {
                    _selectedStartDate = value;
                    MinDateEnd = SelectedStartDate.AddDays(1);
                    IsEndDateEnabled = true;
                    SelectedEndDate = MinDateEnd;
                    OnPropertyChanged(nameof(MinDateEnd));
                    OnPropertyChanged(nameof(SelectedEndDate));
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }

        private DateTime _selectedEndDate;
        public DateTime SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                if (_selectedEndDate != value)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }

        private bool _isEndDateEnabled;
        public bool IsEndDateEnabled
        {
            get
            {
                return _isEndDateEnabled;
            }
            set
            {
                if (_isEndDateEnabled != value)
                {
                    _isEndDateEnabled = value;
                    OnPropertyChanged(nameof(IsEndDateEnabled));
                }
            }
        }

        private DateTime _minDateStart;
        public DateTime MinDateStart
        {
            get
            {
                return _minDateStart;
            }
            set
            {
                if (_minDateStart != value)
                {
                    _minDateStart = value;
                    OnPropertyChanged(nameof(MinDateStart));
                }
            }
        }

        private DateTime _minDateEnd;
        public DateTime MinDateEnd
        {
            get
            {
                return _minDateEnd;
            }
            set
            {
                if (_minDateEnd != value)
                {
                    _minDateEnd = value;
                    OnPropertyChanged(nameof(MinDateEnd));
                }
            }
        }

        private string _tourRequestType;

        public string TourRequestType
        {
            get
            {
                return _tourRequestType;
            }
            set
            {
                if (_tourRequestType != value)
                {
                    _tourRequestType = value;
                    OnPropertyChanged(nameof(TourRequestType));
                }
            }
        }

        private int _age;

        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private string _name;

        [Required(ErrorMessage = "Name is Required")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Validate(nameof(Name), value);
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _lastName;

        [Required(ErrorMessage = "Last name is Required")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                Validate(nameof(LastName), value);
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        public bool HasErrors => Errors.Count > 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }

            return Enumerable.Empty<string>();
        }

        private void ExecuteSelectTourRequestType(object parameter)
        {

            TourRequestType = parameter as string;
        }
        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            AddParticipantCommand.RaiseCanExecuteChanged();

        }
        private bool CanAddParticipant(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }

        private void ExecuteAddParticipant(object obj)
        {
            TourParticipantViewModel tourParticipantViewModel = _tourParticipantService.saveParticipantToDTO(Name, LastName, Age);
            TourParticipantDTOs.Add(tourParticipantViewModel);
            TourParticipantsListBox.Add(tourParticipantViewModel);

            SetupForNextParticipantInput();
        }
        private void SetupForNextParticipantInput()
        {
            ParticipantsListBox.Clear();
            foreach (var participant in TourParticipantsListBox)
            {
                ParticipantsListBox.Add(participant);
            }

            Name = string.Empty;
            LastName = string.Empty;
            Age = 1;
        }

        private void SaveToCsv()
        {
            if (ParticipantCount - 1 != ParticipantsListBox.Count)
            {
                Messenger.Default.Send(new NotificationMessage("Number of participants does not match the number of participants entered\nNote: Number of participants includes you"));
                return;
            }
            // saving participants
            List<int> participantIds = SaveParticipants();

            // saving request

            TourRequest request = new TourRequest(UserId, Description, Language, participantIds,
                                                  SelectedStartDate, SelectedEndDate, City, Country);
            _tourRequestService.SaveRequest(request);

            Messenger.Default.Send(new CloseWindowMessage());
        }

        private List<int> SaveParticipants()
        {
            TourParticipantDTOs.Add(ToTourParticipantViewModel(_tourReservationService.FindTouristById(UserId)));
            TourParticipantDTOs.Reverse();
            int TourRequestdId = _tourRequestService.NextRequestId();

            List<int> participantIds = new List<int>();
            foreach (TourParticipantViewModel tp in TourParticipantDTOs)
            {
                participantIds.Add(_requestedTourParticipantService.NextParticipantId());
                _requestedTourParticipantService.SaveRequestedTourParticipant(ToRequestedTourParticipant(tp), TourRequestdId);
            }
            return participantIds;
        }

        private RequestedTourParticipant ToRequestedTourParticipant(TourParticipantViewModel tpViewModel)
        {
            RequestedTourParticipant requestedTourParticipant = new RequestedTourParticipant();
            requestedTourParticipant.Name = tpViewModel.Name;
            requestedTourParticipant.LastName = tpViewModel.LastName;
            requestedTourParticipant.Years = tpViewModel.Years;
            return requestedTourParticipant;
        }
        public TourParticipantViewModel ToTourParticipantViewModel(Tourist tourist)
        {
            TourParticipantViewModel viewModel = new TourParticipantViewModel();
            viewModel.Name = tourist.Name;
            viewModel.LastName = tourist.LastName;
            viewModel.Years = tourist.Age;
            return viewModel;
        }

        public void InitializeTourRequestWindow()
        {
            LoadCountriesFromCSV();
            LoadLanguagesFromCSV();

            SelectedStartDate = DateTime.Now.AddDays(3);
            Age = 1;
            ParticipantCount = 1;
            MinDateStart = DateTime.Now.AddDays(3);
            IsEndDateEnabled = false;
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

        private void LoadCitiesFromCSV()
        {
            Cities.Clear();

            string csvFilePath = "../../../Resources/Data/european_cities_and_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[1].Equals(Country))
                        Cities.Add(values[0]);
                }
            }
            City = Cities[0];
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
                    Countries.Add(values[0]);
                }
            }
        }

        private void ExecuteCloseWindow(object obj)
        {
            Messenger.Default.Send(new CloseWindowMessage());

        }

        public TourRequestWindowViewModel()
        {
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            _tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            _requestedTourParticipantService = new RequestedTourParticipantService(Injector.Injector.CreateInstance<IRequestedTourParticipantRepository>());

            Countries = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();
            Cities = new ObservableCollection<string>();

            SelectTourRequestType = new RelayCommand(ExecuteSelectTourRequestType);
            AddParticipantCommand = new RelayCommand(ExecuteAddParticipant, CanAddParticipant);
            CloseWindowCommand = new RelayCommand(ExecuteCloseWindow);

            TourParticipantDTOs = new List<TourParticipantViewModel>();
            TourParticipantsListBox = new List<TourParticipantViewModel>();
            ParticipantsListBox = new ObservableCollection<TourParticipantViewModel>();
        }
    }
}
