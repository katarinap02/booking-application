using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.View.TouristWindows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit;

namespace BookingApp.WPF.ViewModel
{
    public class TourReservationViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly TourParticipantService _tourParticipantService;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        private readonly TourService _tourService;

        public List<TourParticipantViewModel> TourParticipantDTOs { get; set; }
        public List<TourParticipantViewModel> TourParticipantsListBox { get; set; }

        public RelayCommand AddParticipantCommand {  get; set; }

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private List<int> _participantIds = new List<int>();
        public List<int> ParticipantIds
        {
            get { return _participantIds; }
            set
            {
                if (_participantIds != value)
                {
                    _participantIds = value;
                    OnPropertyChanged(nameof(ParticipantIds));
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private int _touristId;
        public int TouristId
        {
            get { return _touristId; }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }

        private int _startCheckpoint;

        public int StartCheckpoint
        {
            get { return _startCheckpoint; }
            set
            {
                if (_startCheckpoint != value)
                {
                    _startCheckpoint = value;
                    OnPropertyChanged(nameof(StartCheckpoint));
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

        private int participantCount;
        public string ParticipantCount
        {
            get
            {
                return participantCount.ToString();
            }
            set
            {
                if (value != participantCount.ToString())
                {
                    participantCount = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(participantCount));
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

        private List<TourParticipantViewModel> _participantsListBox = new List<TourParticipantViewModel>();
        public List<TourParticipantViewModel> ParticipantsListBox
        {
            get
            {
                return _participantsListBox;
            }
            set
            {
                if (value != _participantsListBox)
                {
                    _participantsListBox = value;
                    OnPropertyChanged(nameof(ParticipantsListBox));
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

        private string _age;

        [Required(ErrorMessage = "Age is Required")]
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                Validate(nameof(Age), value);
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
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

        public bool HasErrors => Errors.Count > 0;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }

            return Enumerable.Empty<string>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void SetupForNextParticipantInput()
        {
            ParticipantsListBox = null;
            ParticipantsListBox = TourParticipantsListBox;

            Name = string.Empty;
            LastName = string.Empty;
            Age = "0";
        }

        private void ExecuteAddParticipant(object obj)
        {
            if (!AllFieldsFilled(Age, Name, LastName))
            {
                System.Windows.MessageBox.Show("All fields must be filled");
            }
            else
            {
                TourParticipantViewModel tourParticipantViewModel = _tourParticipantService.saveParticipantToDTO(Name, LastName, Age);
                TourParticipantDTOs.Add(tourParticipantViewModel);

                TourParticipantsListBox.Add(tourParticipantViewModel);

                SetupForNextParticipantInput();
            }
        }

        private bool CanAddParticipant(object obj)
        {
            return Validator.TryValidateObject(this, new ValidationContext(this), null);
        }

        private bool AllFieldsFilled(string years, string name, string lastName)
        {
            if (years == string.Empty || name == string.Empty || lastName == string.Empty)
            {
                return false;
            }

            return true;
        }

        public void RemoveParticipant()
        {
            TourParticipantDTOs.Remove(SelectedParticipant);

            TourParticipantsListBox.Remove(SelectedParticipant);
            ParticipantsListBox = null;
            ParticipantsListBox = TourParticipantsListBox;
        }
        private void saveParticipants(bool hasAlreadyReserved, TourReservationViewModel reservation)
        {
            if (hasAlreadyReserved)
            {
                foreach (TourParticipantViewModel tp in TourParticipantDTOs)
                {
                    _tourReservationService.addParticipant(tp, reservation);
                }
                return;
            }
            int reservationId = _tourReservationService.NextReservationId();
            TourParticipantDTOs.Add(ToTourParticipantViewModel(_tourReservationService.FindTouristById(UserId)));
            TourParticipantDTOs.Reverse();
            foreach (TourParticipantViewModel tp in TourParticipantDTOs)
            {
                _tourParticipantService.saveParticipant(tp, reservationId);
            }
        }
        public TourParticipantViewModel ToTourParticipantViewModel(Tourist tourist)
        {
            TourParticipantViewModel viewModel = new TourParticipantViewModel();
            viewModel.Name = tourist.Name;
            viewModel.LastName = tourist.LastName;
            viewModel.Years = tourist.Age;
            return viewModel;
        }

        private void saveReservation()
        {
            _tourReservationService.saveReservation(SelectedTour, UserId);
        }

        public bool Book()
        {
            // trebam da uracunam i korisnika
            if (participantCount - 1 < TourParticipantDTOs.Count)
            {
                System.Windows.MessageBox.Show("Too many participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else if (participantCount - 1 > TourParticipantDTOs.Count)
            {
                System.Windows.MessageBox.Show("Too less participants in the list of participants", "Participants error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
            {
                //ako postoji vec rezervacija za tu turu
                TourReservationViewModel reservation = ToTourReservationViewModel(_tourReservationService.FindReservationByTOuristIdAndTourId(UserId, SelectedTour.Id));
                // ovo znaci da vec ima rezervacija
                if (reservation != null)
                {
                    saveParticipants(true, reservation);
                }
                else
                {
                    saveParticipants(false, reservation);
                    saveReservation();
                }
                ReduceNumberOfAvailablePlaces();
                // ovo za vaucere
                if (_voucherService.HasVoucher(UserId))
                {
                    VoucherWindow voucherWindom = new VoucherWindow(UserId);
                    voucherWindom.ShowDialog();
                }
                System.Windows.MessageBox.Show("Tour " + "\"" + SelectedTour.Name + "\"" + " booked!");
                return true;
            }
        }

        private void ReduceNumberOfAvailablePlaces()
        {
            try
            {
                _tourService.UpdateAvailablePlaces(SelectedTour, TourParticipantDTOs.Count);
            }
            catch (ArgumentNullException)
            {
                System.Windows.MessageBox.Show("something wrong happened");
            }
            TourParticipantDTOs.Clear();
        }
        private TourReservationViewModel ToTourReservationViewModel(TourReservation reservation)
        {
            TourReservationViewModel tourReservationViewModel = new TourReservationViewModel();
            if(reservation != null)
            {
                tourReservationViewModel.Id = reservation.Id;
                tourReservationViewModel.TourId = reservation.TourId;
                tourReservationViewModel.TouristId = reservation.TouristId;
                tourReservationViewModel.ParticipantIds = reservation.ParticipantIds;
                return tourReservationViewModel;
            }
            return null;
        }

        public void Close()
        {

        }

        public TourReservationViewModel()
        {
            _tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            _voucherService = new VoucherService(Injector.Injector.CreateInstance<IVoucherRepository>());

            AddParticipantCommand = new RelayCommand(ExecuteAddParticipant, CanAddParticipant);

            TourParticipantDTOs = new List<TourParticipantViewModel>();
            TourParticipantsListBox = new List<TourParticipantViewModel>();
        }

        public TourReservationViewModel(TourReservation tourReservation)
        {
            _id = tourReservation.Id;
            _participantIds = tourReservation.ParticipantIds;
            _tourId = tourReservation.TourId;
            _startCheckpoint = tourReservation.StartCheckpoint;
        }

        public TourReservation ToTourReservation()
        {
            TourReservation tourReservation = new TourReservation(Id, _tourId, _touristId, _startCheckpoint);
            tourReservation.Id = _id;
            tourReservation.TourId = _tourId;
            tourReservation.TouristId = _touristId;
            tourReservation.StartCheckpoint = _startCheckpoint;
            tourReservation.ParticipantIds = _participantIds;
            return tourReservation;
        }
    }
}
