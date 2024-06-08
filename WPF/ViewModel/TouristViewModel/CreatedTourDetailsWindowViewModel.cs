using BookingApp.Application.Services.FeatureServices;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.View.TouristWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class CreatedTourDetailsWindowViewModel : INotifyPropertyChanged
    {
        private readonly TouristNotificationService notificationService;

        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if(_closeCommand == null)
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

        private string _tourName;
        public string TourName
        {
            get
            {
                return _tourName;
            }
            set
            {
                if(_tourName != value)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(TourName));
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
                if(_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
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
                    OnPropertyChanged(nameof(Country));
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
        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if(_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeCreatedTourDetailsWindow()
        {
            Tour tour = notificationService.GetTourById(SelectedNotification.TourId);
            if(tour == null)
            {
                return;
            }
            TourName = SelectedNotification.TourName;
            City = tour.City;
            Country = tour.Country;
            Language = tour.Language;
            Date = tour.Date;
        }

        public CreatedTourDetailsWindowViewModel()
        {
            notificationService = new TouristNotificationService(Injector.Injector.CreateInstance<ITouristNotificationRepository>());
        }

    }
}
