using BookingApp.Model;
using BookingApp.Repository;
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
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourRepository _repository;
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
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());

            UserId = userId;
            MaximumValuePeoples = _repository.FindMaxNumberOfParticipants().ToString();
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
            List<Tour>? foundTours = Search();

            if (foundTours != null)
                foreach (Tour t in foundTours)
                    Tours.Add(t);
        }

        private List<Tour>? Search()
        {
            DurationSearch.Text = EmptyStringToZero(DurationSearch.Text);
            PeopleSearch.Text = EmptyStringToZero(PeopleSearch.Text);

            Tour tour = new Tour("", CitySearch.Text, CountrySearch.Text, "", LanguageSearch.Text, int.Parse(PeopleSearch.Text),
                                new List<string>(), new DateTime(), float.Parse(DurationSearch.Text), new List<string>());
            List<Tour>? foundTours = _repository.SearchTours(tour);
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
                List<Tour>? foundTours = Search();

                if (foundTours != null)
                    foreach (Tour t in foundTours)
                        Tours.Add(t);
            }
            else
            {
                List<Tour> allTours = _repository.GetAll();
                foreach (Tour tour in allTours)
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

                if(Tours.Count != _repository.ToursCount())
                {
                    RefreshDataGrid(true);
                }
                else
                    RefreshDataGrid(false);
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
