using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class TourStatsViewModel: ViewModelBase
    {
        public SeriesCollection series { get; set; }
        public ObservableCollection<TourViewModel> Tours { get; set; } = new ObservableCollection<TourViewModel>();

        private string _caption;
        public string Caption
        {
            get { return _caption; }
            set { _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        private string _combo;
        public string Combo
        {
            get { return _combo; }
            set { _combo = value;
                OnPropertyChanged(nameof(Combo));
                ChangeMostVisited();
            }
        }

        private string _tourName;
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        private string _participantNumber;
        public string ParticipantNumber
        {
            get { return _participantNumber; }
            set
            {
                _participantNumber = value;
                OnPropertyChanged(nameof(ParticipantNumber));
            }
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                UpdateAgeStatistics(_selectedTour.Id);
            }
        }
        private readonly TourService tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
        private readonly TourReservationService reservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>()); 
        private int GuideId;
        public TourStatsViewModel(int guide_id)
        { 
            GuideId = guide_id;
            series = new SeriesCollection();
            Combo = "System.Windows.Controls.ComboBoxItem: All time";
            getGridData();            
        }

        public void getGridData()
        {
            List<Tour> tours = tourService.findFinnishedToursByGuide(GuideId);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            Tours = newViewModels;
            if(tours.Count > 0)
            {
                UpdateAgeStatistics(tours[0].Id);
            }            
        }

        private void UpdateAgeStatistics(int tourId)
        {
            Caption = tourService.GetTourById(tourId).Name;
            List<int> ages = tourService.GetAgeStatistic(tourId);
            series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Under 18",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[0]) },
                    Fill = Brushes.DodgerBlue,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Between 18 and 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[1]) },
                    Fill = Brushes.Coral,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Above 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[2]) },
                    Fill = Brushes.DeepPink,
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(series));
        }


        private void ChangeMostVisited()
        {
            if(Combo == "System.Windows.Controls.ComboBoxItem: All time")
            {
                ShowMostVisitedTourAllTime();
            }
            else
            {
                int selectedYear = 0;
                if (Combo.Contains("2023")) { selectedYear = 2023; }
                else if (Combo.Contains("2024")) { selectedYear = 2024; }  
                ShowMostVisitedTourPreviousYears(selectedYear);
            }
        }

        private void ShowMostVisitedTourAllTime()
        {
            Tour mostVisitedTourAllTime = tourService.GetMostPopularTourForGuide(GuideId);
            
            if (mostVisitedTourAllTime != null)
            {
                TourName = mostVisitedTourAllTime.Name;
                int participantCount = reservationService.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
                ParticipantNumber= $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantNumber = "";
            }


        }

        private void ShowMostVisitedTourPreviousYears(int year)
        {
            Tour mostVisitedTourPreviousYears = tourService.GetMostPopularTourForGuideInYear(GuideId, year);

            if (mostVisitedTourPreviousYears != null)
            {
                TourName = mostVisitedTourPreviousYears.Name;
                int participantCount = reservationService.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
                ParticipantNumber = $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantNumber = "";
            }

        }
    }
}
