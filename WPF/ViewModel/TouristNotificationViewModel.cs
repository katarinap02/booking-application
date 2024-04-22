using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel
{
    public class TouristNotificationViewModel : INotifyPropertyChanged
    {
        private readonly TouristNotificationService _touristNotificationService;
        private readonly TourService _tourService;
        public ObservableCollection<string> tourists { get; set; }
        public ObservableCollection<TouristNotificationViewModel> touristNotificationViewModels { get; set; }
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
        private int _touristId;
        public int TouristId
        {
            get
            {
                return _touristId;
            }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }
        private int _tourId;
        public int TourId
        {
            get
            {
                return _tourId;
            }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }
        private NotificationType _notificationType;
        public NotificationType NotificationType
        {
            get
            {
                return _notificationType;
            }
            set
            {
                if (value != _notificationType)
                {
                    _notificationType = value;
                    OnPropertyChanged(nameof(NotificationType));
                }
            }
        }
        private string _tourName;
        public string TourName
        {
            get
            {
                return _tourName;
            }
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }

        private string _guideName;
        public string GuideName
        {
            get
            {
                return _guideName;
            }
            set
            {
                if (value != _guideName)
                {
                    _guideName = value;
                    OnPropertyChanged(nameof(GuideName));
                }
            }
        }

        private int _currentCheckpoint;
        public int CurrentCheckpoint
        {
            get
            {
                return _currentCheckpoint;
            }
            set
            {
                if (_currentCheckpoint != value)
                {
                    _currentCheckpoint = value;
                    OnPropertyChanged(nameof(CurrentCheckpoint));
                }
            }
        }

        private string _currentCheckpointName;
        public string CurrentCheckpointName
        {
            get
            {
                return _currentCheckpointName;
            }
            set
            {
                if( value != _currentCheckpointName )
                {
                    _currentCheckpointName = value;
                    OnPropertyChanged(nameof(CurrentCheckpointName));
                }
            }
        }

        private TouristNotificationViewModel _selectedNotification;
        public TouristNotificationViewModel SelectedNotification
        {
            get
            {
                return _selectedNotification;
            }
            set
            {
                if (value != _selectedNotification)
                {
                    _selectedNotification = value;
                    OnPropertyChanged(nameof(SelectedNotification));
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
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeTouristNotificationWindow()
        {
            foreach (var notification in _touristNotificationService.GetMyNotifications(UserId))
            {
                touristNotificationViewModels.Add(notification);
            }
        }

        public void InitializeAddedTouristsWindow()
        {
            CurrentCheckpointName = _tourService.GetCheckpointsByTour(SelectedNotification.TourId)[CurrentCheckpoint];
            foreach (var tourist in _tourService.GetParticipantsThatJoinedNow(SelectedNotification.ToTouristNotification()))
            {
                tourists.Add(tourist);
            }

        }

        public void DetailsButton()
        {
            AddedTouristsNotificationWindow addedTouristsNotificationWindow = new AddedTouristsNotificationWindow(SelectedNotification);
            addedTouristsNotificationWindow.ShowDialog();
        }

        public TouristNotificationViewModel()
        {
            _touristNotificationService = new TouristNotificationService(Injector.Injector.CreateInstance<ITouristNotificationRepository>());
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());

            touristNotificationViewModels = new ObservableCollection<TouristNotificationViewModel>();
            tourists = new ObservableCollection<string>();
        }
        public TouristNotificationViewModel(TouristNotification touristNotification)
        {
            Id = touristNotification.Id;
            TouristId = touristNotification.TouristId;
            TourId = touristNotification.TourId;
            NotificationType = touristNotification.NotificationType;
            TourName = touristNotification.TourName;
            GuideName = touristNotification.GuideName;
            CurrentCheckpoint = touristNotification.CurrentCheckpoint;
        }

        public TouristNotification ToTouristNotification()
        {
            TouristNotification touristNotification = new TouristNotification(Id, TouristId, TourId, NotificationType, TourName, GuideName, CurrentCheckpoint);
            touristNotification.Id = Id;
            touristNotification.TourId = TourId;
            touristNotification.TouristId = TouristId;
            touristNotification.NotificationType = NotificationType;
            touristNotification.TourName = TourName;
            touristNotification.GuideName = GuideName;
            touristNotification.CurrentCheckpoint = CurrentCheckpoint;
            return touristNotification;
        }

        public override string ToString()
        {
            string type;
            switch (_notificationType)
            {
                case NotificationType.RateTour:
                    type = "Rate tour";
                    break;
                case NotificationType.JoinedTour:
                    type = "Joined tour";
                    break;
                case NotificationType.TourCanceled:
                    type = "Tour canceled";
                    break;
                case NotificationType.GuideQuit:
                    return $"Guide quit :: Guide - {GuideName}";
                default:
                    type = "";
                    break;
            }
            return $"{type} :: Tour - \"{_tourName}\"";
        }
    }
}
