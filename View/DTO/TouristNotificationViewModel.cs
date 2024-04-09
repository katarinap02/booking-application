using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TouristNotificationViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id { 
            get { return _id; }
            set
            {
                if( _id != value )
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
                if( _touristId != value )
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
                if( _touristId != value )
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(_touristId));
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
                if( value != _notificationType )
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
                if(value != _tourName)
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
                if(value != _guideName)
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
                if( _currentCheckpoint != value )
                {
                    _currentCheckpoint = value;
                    OnPropertyChanged(nameof(CurrentCheckpoint));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TouristNotificationViewModel() { }
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
            string type = "";
            if(_notificationType == NotificationType.RateTour)
            {
                type = "Rate tour";
            }else if(_notificationType == NotificationType.JoinedTour)
            {
                type = "Joined tour";
            }else if(_notificationType == NotificationType.TourCanceled)
            {
                type = "Tour canceled";
            }else if(_notificationType == NotificationType.GuideQuit)
            {
                type = "Guide quit";
                return $"{type} :: Guide - {GuideName}";
            }
            return $"{type} :: Tour - \"{_tourName}\"";
        }
    }
}
