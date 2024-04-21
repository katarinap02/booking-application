using BookingApp.Application.Services.FeatureServices;
using BookingApp.Repository;
using BookingApp.Application.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.View.GuideWindows
{
    public partial class TourStatsWindow : Window
    {
        private readonly TourRepository _tourRepository;
        private readonly TourService _tourService;
        public SeriesCollection series { get; set; }

        public TourStatsWindow(int tourId)
        {
            InitializeComponent();
            _tourRepository = new TourRepository();
            _tourService = new TourService();
            UpdateAgeStatistics(tourId);            
            DataContext = this;
        }

        private void UpdateAgeStatistics(int tourId)
        {
            List<int> ages = _tourService.GetAgeStatistic(tourId);
            series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Under 18",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[0]) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Between 18 and 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[1]) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Above 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[2]) },
                    DataLabels = true
                }
            };
        }

        
    }
}
