using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel
{
    public class TourNumberOfParticipantsViewModel : INotifyPropertyChanged
    {
        public TourService _tourService { get; set; }

        public ObservableCollection<TourViewModel> Tours { get; set; }

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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshToursByCity()
        {
            Tours.Clear();
            List<TourViewModel> tours = ToTourViewModel(_tourService.GetTourByCityWithAvailablePlaces(SelectedTour.City));
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
                AvailablePlaces = _tourService.FindMaxNumberOfParticipants(ToTour(Tours.ToList()));
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

        public List<Tour> ToTour(List<TourViewModel> toursViewModel)
        {
            List<Tour> tours = new List<Tour>();
            foreach (TourViewModel tourViewModel in toursViewModel)
            {
                tours.Add(tourViewModel.ToTour());
            }
            return tours;
        }

        public TourNumberOfParticipantsViewModel()
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Tours = new ObservableCollection<TourViewModel>();
        }
    }
}
