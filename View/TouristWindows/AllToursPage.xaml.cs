using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page, INotifyPropertyChanged
    {
        //public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<TourViewModel> Tours { get; set; }
        public TourViewModel SelectedTour { get; set; }

        //private readonly TourRepository _repository;
        private readonly TouristService _touristService;
        public int UserId;

        #region Property
        private int _maximumValuePeoples;

        public string MaximumValuePeoples
        {
            get
            {
                return _maximumValuePeoples.ToString();
            }
            set
            {
                if (value != _maximumValuePeoples.ToString())
                {
                    _maximumValuePeoples = Convert.ToInt32(value);
                    OnPropertyChanged(nameof(_maximumValuePeoples));
                }
            }
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        public AllToursPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _touristService = new TouristService();
            Tours = new ObservableCollection<TourViewModel>();

            UserId = userId;
            MaximumValuePeoples = _touristService.FindMaxNumberOfParticipants().ToString();

            RefreshDataGrid(false);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            CountrySearch.Text = "";
            CitySearch.Text = "";
            DurationSearch.Text = "0";
            LanguageSearch.Text = "";
            PeopleSearch.Text = "0";
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            List<TourViewModel>? foundTours = Search();

            if (foundTours != null)
                foreach (TourViewModel t in foundTours)
                    Tours.Add(t);
        }

        private List<TourViewModel>? Search()
        {
            DurationSearch.Text = EmptyStringToZero(DurationSearch.Text);
            PeopleSearch.Text = EmptyStringToZero(PeopleSearch.Text);

            Tour tour = new Tour("", CitySearch.Text, CountrySearch.Text, "", LanguageSearch.Text, int.Parse(PeopleSearch.Text),
                                new List<string>(), new DateTime(), float.Parse(DurationSearch.Text), new List<string>());
            List<TourViewModel>? foundTours = _touristService.SearchTours(tour);
            return foundTours;
        }

        private string EmptyStringToZero(string text)
        {
            if (text == string.Empty)
            {
                return "0";
            }

            return text;
        }
        
        private void RefreshDataGrid(bool withSearch)
        {
            Tours.Clear();
            if (withSearch)
            {
                List<TourViewModel>? foundTours = Search();

                if (foundTours != null)
                    foreach (TourViewModel t in foundTours)
                        Tours.Add(t);
            }
            else
            {
                List<TourViewModel> allTours = _touristService.GetAll();
                foreach (TourViewModel tour in allTours)
                    Tours.Add(tour);
            }

        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour == null)
            {
                MessageBox.Show("Something wrong happened");
            }
            else
            {
                TourNumberOfParticipantsWindow tourNumberOfParticipantsWindow = new TourNumberOfParticipantsWindow(SelectedTour, UserId);
                tourNumberOfParticipantsWindow.ShowDialog();

                if(Tours.Count != _touristService.ToursCount())
                {
                    RefreshDataGrid(true);
                }
                else
                    RefreshDataGrid(false);
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            TourDetailsWindow tourDetailsWindow = new TourDetailsWindow(SelectedTour, false);
            tourDetailsWindow.ShowDialog();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            TouristNotificationWindow touristNotificationWindow = new TouristNotificationWindow();
            touristNotificationWindow.ShowDialog();
        }
    }
}
