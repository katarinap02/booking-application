using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.TouristWindows;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization.Charting.Compatible;
using System.Windows.Input;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        public TourRequestService TourRequestService { get; set; }

        public SeriesCollection SeriesCollectionLanguage { get; set; }
        public SeriesCollection SeriesCollectionCity { get; set; }

        public string[] Languages { get; set; }
        public string[] Cities { get; set; }

        private ICommand _close;
        public ICommand Close
        {
            get
            {
                if(_close == null)
                {
                    _close = new RelayCommand(param => CloseWindow());
                }
                return _close;
            }
        }

        private void CloseWindow()
        {
            Messenger.Default.Send(new CloseWindowMessage());
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

        private List<int> _years;
        public List<int> Years
        {
            get
            {
                return _years;
            }
            set
            {
                if(_years != value)
                {
                    _years = value;
                    OnPropertyChanged(nameof(Years));
                }
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if(_selectedYear != value)
                {
                    _selectedYear = value;
                    InitializePercentage();
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }

        private int _acceptedRequestPercentage;
        public int AcceptedRequestPercentage
        {
            get
            {
                return _acceptedRequestPercentage;
            }
            set
            {
                if (_acceptedRequestPercentage != value)
                {
                    _acceptedRequestPercentage = value;
                    OnPropertyChanged(nameof(AcceptedRequestPercentage));
                }
            }
        }
        private int _rejectedRequestPercentage;
        public int RejectedRequestPercentage
        {
            get
            {
                return _rejectedRequestPercentage;
            }
            set
            {
                if (_rejectedRequestPercentage != value)
                {
                    _rejectedRequestPercentage = value;
                    OnPropertyChanged(nameof(RejectedRequestPercentage));
                }
            }
        }

        private double _acceptedRequestAvgNumberParticipants;
        public double AcceptedRequestAvgNumberParticipants
        {
            get
            {
                return _acceptedRequestAvgNumberParticipants;
            }
            set
            {
                if(_acceptedRequestAvgNumberParticipants != value)
                {
                    _acceptedRequestAvgNumberParticipants = value;
                    OnPropertyChanged(nameof(AcceptedRequestAvgNumberParticipants));
                }
            }
        }

        private Dictionary<string, int> _requestCountByLanguage;
        public Dictionary<string, int> RequestCountByLanguage
        {
            get
            {
                return _requestCountByLanguage;
            }
            set
            {
                if (_requestCountByLanguage != value)
                {
                    _requestCountByLanguage = value;
                    OnPropertyChanged(nameof(RequestCountByLanguage));
                }
            }
        }

        private Dictionary<string, int> _requestCountByCity;
        public Dictionary<string, int> RequestCountByCity
        {
            get
            {
                return _requestCountByCity;
            }
            set
            {
                if (_requestCountByCity != value)
                {
                    _requestCountByCity = value;
                    OnPropertyChanged(nameof(RequestCountByCity));
                }
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InitializeTourStatistics()
        {
            RequestCountByLanguage = TourRequestService.GetRequestCountByLanguage(UserId);
            RequestCountByCity = TourRequestService.GetRequestCountByCity(UserId);

            UpdateChart();
            Years = new List<int> { 0 };
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year > currentYear - 10; year--)
            {
                Years.Add(year);
            }
            InitializePercentage();
       }

        private void InitializePercentage()
        {
            AcceptedRequestPercentage = (int)Math.Round(TourRequestService.GetAcceptedRequestPercentage(UserId, SelectedYear));
            RejectedRequestPercentage = (int)Math.Round(TourRequestService.GetRejectedRequestPercentage(UserId, SelectedYear));
            AcceptedRequestAvgNumberParticipants = TourRequestService.GetAverageNumberOfPeopleInAcceptedRequests(UserId, SelectedYear);

        }

        public void UpdateChart()
        {
            SeriesCollectionLanguage.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Requests",
                Values = new ChartValues<int>(RequestCountByLanguage.Values),
                Fill = new SolidColorBrush(Color.FromRgb(219, 55, 90)),
                ColumnPadding = 1
            });
            SeriesCollectionCity.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Requests",
                Values = new ChartValues<int>(RequestCountByCity.Values),
                Fill = new SolidColorBrush(Color.FromRgb(219, 55, 90)),
                ColumnPadding = 1
            });

            Languages = RequestCountByLanguage.Keys.ToArray();
            Cities = RequestCountByCity.Keys.ToArray();
        }

        public TourStatisticsViewModel(int userId)
        {
            TourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            UserId = userId;
            RequestCountByLanguage = new Dictionary<string, int>();
            RequestCountByCity = new Dictionary<string, int>();
            SeriesCollectionLanguage = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "Requests",
                    Values = new ChartValues<int>(RequestCountByLanguage.Values)
                }
            };
            SeriesCollectionCity = new SeriesCollection
            {
                new LiveCharts.Wpf.ColumnSeries
                {
                    Title = "Requests",
                    Values = new ChartValues<int>(RequestCountByCity.Values)
                }
            };

            Languages = RequestCountByLanguage.Keys.ToArray();
            Cities = RequestCountByCity.Keys.ToArray();
        }
    }
}
