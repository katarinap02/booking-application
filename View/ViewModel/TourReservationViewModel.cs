using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Xceed.Wpf.Toolkit;

namespace BookingApp.ViewModel
{
    public class TourReservationViewModel : INotifyPropertyChanged
    {
        private readonly TourParticipantService _tourParticipantService;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        private readonly TourService _touristService;

        public List<TourParticipantViewModel> TourParticipantDTOs { get; set; }
        public List<TourParticipantViewModel> TourParticipantsListBox { get; set; }

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private List<int> _participantIds = new List<int>();
        public List<int> ParticipantIds
        {
            get { return  _participantIds; }
            set
            {
                if(_participantIds != value)
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
                if(_userId != value)
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
                if(value != _participantsListBox)
                {
                    _participantsListBox = value;
                    OnPropertyChanged(nameof(ParticipantsListBox));
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private string _age;
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                if(_age != value)
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
                if(_selectedParticipant != value)
                {
                    _selectedParticipant = value;
                    OnPropertyChanged(nameof(SelectedParticipant));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetupForNextParticipantInput()
        {
            ParticipantsListBox = null;
            ParticipantsListBox = TourParticipantsListBox;

            Name = string.Empty;
            LastName = string.Empty;
            Age = "0";
        }

        public void AddParticipant()
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
            TourParticipantDTOs.Add(_tourReservationService.FindTouristById(UserId));
            TourParticipantDTOs.Reverse();
            foreach (TourParticipantViewModel tp in TourParticipantDTOs)
            {
                _tourParticipantService.saveParticipant(tp, reservationId);
            }
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
                //ako postoji vec rezervacija za tu turu
            {
                TourReservationViewModel reservation = _tourReservationService.FindReservationByTOuristIdAndTourId(UserId, SelectedTour.Id);
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
                _touristService.UpdateAvailablePlaces(SelectedTour, TourParticipantDTOs.Count);
            }
            catch (ArgumentNullException)
            {
                System.Windows.MessageBox.Show("something wrong happened");
            }
        }

        public TourReservationViewModel() { 
            _tourParticipantService = new TourParticipantService();
            _tourReservationService = new TourReservationService();
            _touristService = new TourService();
            _voucherService = new VoucherService();

            TourParticipantDTOs = new List<TourParticipantViewModel>();
            TourParticipantsListBox = new List<TourParticipantViewModel>();
        }

        public TourReservationViewModel(TourReservation tourReservation)
        {
            _id = tourReservation.Id;
            _participantIds = tourReservation.ParticipantIds;
            _tourId= tourReservation.TourId;
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
