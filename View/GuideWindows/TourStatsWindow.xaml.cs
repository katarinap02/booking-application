using LiveCharts;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.View.GuideWindows
{
    public partial class TourStatsWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ChartValues<double> _ageStatistics;
        public ChartValues<double> AgeStatistics
        {
            get { return _ageStatistics; }
            set
            {
                _ageStatistics = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AgeStatistics)));
            }
        }

        public TourStatsWindow(int tourId)
        {
            InitializeComponent();
            UpdateAgeStatistics(tourId);
            DataContext = this;
        }

        private void UpdateAgeStatistics(int tourId)
        {
            List<int> ageCounts = GetAgeStatistic(tourId);

            AgeStatistics = new ChartValues<double>
            {
                ageCounts[0],
                ageCounts[1],
                ageCounts[2]
            };
        }

        private List<int> GetAgeStatistic(int tourId)
        {
            return new List<int> { 10, 20, 5 }; // Placeholder data, replace with actual statistics
        }
    }
}
