using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class TourStatsWindowViewModel
    {
        private readonly TourService _tourService;
        public SeriesCollection series { get; set; }

        public TourStatsWindowViewModel(int tourId)
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            UpdateAgeStatistics(tourId);
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
