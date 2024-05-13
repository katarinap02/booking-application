using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class LocationGraphWIndowViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SeriesCollection _tourStatistics;
        public SeriesCollection LocationStatistics
        {
            get { return _tourStatistics; }
            set
            {
                _tourStatistics = value;
                OnPropertyChanged(nameof(LocationStatistics));
            }
        }


        private string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private readonly TourRequestService tourRequestService;
        private List<TourRequest> tourRequests;
        private int GuideId;

        public MyICommand Updating { get; set; }
        public MyICommand Adding { get; set; }

        public LocationGraphWIndowViewModel(string city, string country, int guide_id) {
            Updating = new MyICommand(Update_Click);
            Adding = new MyICommand(AddTourByStats_Click);
            GuideId = guide_id;
            tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            tourRequests = tourRequestService.getRequestsForLocation(city, country);
        }

        public SeriesCollection getGraphYearly()
        {
            List<int> stats = tourRequestService.GetYearlyStatistic(tourRequests);
            SeriesCollection series = new SeriesCollection
                                        {
                                            new ColumnSeries
                                            {
                                                Title = "2023",
                                                Values = new ChartValues<int> { stats[0] }
                                            },
                                            new ColumnSeries
                                            {
                                                Title = "2024",
                                                Values = new ChartValues<int> { stats[1] }
                                            }
                                        };
            return series;
        }

        public SeriesCollection getMonthlyGraph(int year)
        {
            List<int> stats = tourRequestService.GetMonthlyStatistics(tourRequests, year);
            List<string> monthNames = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            SeriesCollection series = new SeriesCollection();

            for (int i = 0; i < stats.Count; i++)
            {
                var columnSeries = new ColumnSeries
                {
                    Title = monthNames[i],
                    Values = new ChartValues<int> { stats[i] }
                };

                series.Add(columnSeries);
            }
            return series;
        }

        private void Update_Click()
        {
            if (SelectedYear != null)
            {
                if (SelectedYear.Contains("All Time"))
                {
                    LocationStatistics = getGraphYearly();
                }
                else if (SelectedYear.Contains("2023"))
                {
                    LocationStatistics = getMonthlyGraph(2023);
                }
                else if (SelectedYear.Contains("2024"))
                {
                    LocationStatistics = getMonthlyGraph(2024);
                }
            }
        }

        private void AddTourByStats_Click()
        {
            string location = tourRequestService.GetLocationSuggestion();
            NewTourWindow newTourWindow = new NewTourWindow(GuideId, location);
            newTourWindow.Show();
        }
    }
}
