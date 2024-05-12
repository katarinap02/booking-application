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
    /// Interaction logic for RequestStatsWindow.xaml
    /// </summary>
    public partial class RequestStatsWindow : Window, INotifyPropertyChanged
    {
        private string _cityFilter;
        public string CityFilter
        {
            get { return _cityFilter; }
            set
            {
                if (_cityFilter != value)
                {
                    _cityFilter = value;
                    OnPropertyChanged(nameof(CityFilter));
                }
            }
        }

        private string _countryFilter;
        public string CountryFilter
        {
            get { return _countryFilter; }
            set
            {
                if (_countryFilter != value)
                {
                    _countryFilter = value;
                    OnPropertyChanged(nameof(CountryFilter));
                }
            }
        }

        private string _languageFilter;
        public string LanguageFilter
        {
            get { return _languageFilter; }
            set
            {
                if (_languageFilter != value)
                {
                    _languageFilter = value;
                    OnPropertyChanged(nameof(LanguageFilter));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int GuideId;
        public RequestStatsWindow(int guide_id)
        {
            GuideId = guide_id;
            DataContext = this;
            InitializeComponent();
        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            LanguageGraphWindow languageGraphWindow = new LanguageGraphWindow(LanguageFilter, GuideId);
            languageGraphWindow.Show();
        }

        private void Location_Click(object sender, RoutedEventArgs e)
        {
            LocationGraphWindow locationGraphWindow = new LocationGraphWindow(CityFilter, CountryFilter, GuideId);
            locationGraphWindow.Show();
        }
    }
}
