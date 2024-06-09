using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.TouristWindows;
using BookingApp.WPF.View.TouristWindows;
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
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourNumberOfParticipantsViewModel : INotifyPropertyChanged
    {
        public TourService _tourService { get; set; }

        public ObservableCollection<TourViewModel> Tours { get; set; }
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => CloseWindow());
                }
                return _closeCommand;
            }
        }

        private void CloseWindow()
        {
            Messenger.Default.Send(new CloseWindowMessage());
        }
        private ICommand _confirmCommand;
        public ICommand ConfirmCommand
        {
            get
            {
                if (_confirmCommand == null)
                {
                    _confirmCommand = new RelayCommand(param => Confirm());
                }
                return _confirmCommand;
            }
        }

        private void Confirm()
        {
            ConfirmNumberOfParticipants();
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

        public void ConfirmNumberOfParticipants()
        {
            if (InsertedNumberOfParticipants > SelectedTour.AvailablePlaces)
            {
                MessageBoxWindow mb = new MessageBoxWindow("Not enough places for the reservation");
                mb.ShowDialog();
                return;
            }
            if (InsertedNumberOfParticipants == 0)
                InsertedNumberOfParticipants = 1;
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, InsertedNumberOfParticipants, UserId);
            tourReservationWindow.ShowDialog();
            Messenger.Default.Send(new CloseWindowMessage());
        }

        public TourNumberOfParticipantsViewModel(TourViewModel selectedTour, int userId)
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Tours = new ObservableCollection<TourViewModel>();

            SelectedTour = selectedTour;
            AvailablePlaces = selectedTour.AvailablePlaces;
            UserId = userId;
            AvailablePlacesColor = Brushes.Green;
        }
    }
}
