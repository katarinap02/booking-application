using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.View.TouristWindows;
using BookingApp.WPF.View.TouristWindows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourRequestViewModel : INotifyPropertyChanged
    {
        private readonly TourRequestService _tourRequestService;
        public ObservableCollection<TourRequestViewModel> TourRequests { get; set; }

        public ICommand StatisticsCommand { get; set; }

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

        public void InitializeRequestedToursPage()
        {
            TourRequests.Clear();
            foreach(TourRequest tourRequest in _tourRequestService.GetRequestsByTouristId(UserId))
            {
                TourRequests.Add(new TourRequestViewModel(tourRequest));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ExecuteStatisticsCommand(object obj)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow(UserId);
            tourStatisticsWindow.ShowDialog();
        }
        public TourRequestViewModel()
        {
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            
            StatisticsCommand = new RelayCommand(ExecuteStatisticsCommand);
            TourRequests = new ObservableCollection<TourRequestViewModel>();
        }
        public TourRequestViewModel(TourRequest tourRequest)
        {
            UserId = tourRequest.TouristId;
            Country = tourRequest.Country;
            City = tourRequest.City;
            Language = tourRequest.Language;
            Description = tourRequest.Description;
            Status = tourRequest.Status;
        }
        public void NotificationButton()
        {
            TouristNotificationWindow touristNotificationWindow = new TouristNotificationWindow(UserId);
            touristNotificationWindow.ShowDialog();
        }
    }
}
