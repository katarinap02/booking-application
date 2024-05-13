using BookingApp.WPF.View.GuideWindows;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class RequestsStatsWindowViewModel: INotifyPropertyChanged
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

        public MyICommand location {  get; set; }
        public MyICommand language {  get; set; }

        public RequestsStatsWindowViewModel(int id) {
            GuideId = id;
            location = new MyICommand(Location_Click);
            language = new MyICommand(Language_Click);
        }

        private void Language_Click()
        {
            LanguageGraphWindow languageGraphWindow = new LanguageGraphWindow(LanguageFilter, GuideId);
            languageGraphWindow.Show();
        }

        private void Location_Click()
        {
            LocationGraphWindow locationGraphWindow = new LocationGraphWindow(CityFilter, CountryFilter, GuideId);
            locationGraphWindow.Show();
        }

    }
}
