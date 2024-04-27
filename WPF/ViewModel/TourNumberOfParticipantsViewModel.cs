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

        public void InitializeNumberOfParticipantsWindow()
        {
            AvailablePlacesColor = Brushes.Green;
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

        public TourNumberOfParticipantsViewModel()
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Tours = new ObservableCollection<TourViewModel>();
        }
    }
}
