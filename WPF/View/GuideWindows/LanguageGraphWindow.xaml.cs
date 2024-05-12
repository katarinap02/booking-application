using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideWindows
{
    /// <summary>
    /// Interaction logic for LanguageGraphWindow.xaml
    /// </summary>
    public partial class LanguageGraphWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SeriesCollection _tourStatistics;
        public SeriesCollection TourStatistics
        {
            get { return _tourStatistics; }
            set
            {
                _tourStatistics = value;
                OnPropertyChanged(nameof(TourStatistics));
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
        public LanguageGraphWindow(string language, int guide_id)
        {            
            GuideId = guide_id;
            tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            tourRequests = tourRequestService.getRequestsForLanguage(language);            
            MessageBox.Show(tourRequests.Count.ToString());
            
            DataContext = this;
            InitializeComponent();
        }

        public SeriesCollection getGraphYearly() {
            List<int> stats = tourRequestService.GetYearlyStatistic(tourRequests);
            MessageBox.Show(stats[1].ToString());
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
            MessageBox.Show(tourRequests[0].DateRequested.ToString());
            List<int> stats = tourRequestService.GetMonthlyStatistics(tourRequests, year);
            MessageBox.Show(stats[0].ToString()+stats[4].ToString());
            List<string> monthNames = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            // Create a SeriesCollection
            SeriesCollection series = new SeriesCollection();

            // Populate the SeriesCollection with ColumnSeries for each month
            for (int i = 0; i < stats.Count; i++)
            {
                var columnSeries = new ColumnSeries
                {
                    Title = monthNames[i],
                    Values = new ChartValues<int> { stats[i] }
                };

                // Add the ColumnSeries to the SeriesCollection
                series.Add(columnSeries);
            }
            return series;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedYear!=null)
            {
                if (SelectedYear.Contains("All Time"))
                {
                    TourStatistics = getGraphYearly();
                }
                else if (SelectedYear.Contains("2023"))
                {
                    TourStatistics = getMonthlyGraph(2023);
                }
                else if (SelectedYear.Contains("2024"))
                {
                    TourStatistics = getMonthlyGraph(2024);
                }
            }            
        }
    }
}
