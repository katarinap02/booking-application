using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour {  get; set; }
        private readonly TourRepository _repository;
        public TouristWindow()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            List<Tour> foundTours = Search();

            if (foundTours != null)
                foreach (Tour t in foundTours)
                    Tours.Add(t);
        }

        private List<Tour> Search()
        {
            DurationSearch.Text = EmptyStringToZero(DurationSearch.Text);
            PeopleSearch.Text = EmptyStringToZero(PeopleSearch.Text);

            List<Tour>? foundTours = _repository.FindToursBy(CountrySearch.Text, CitySearch.Text, float.Parse(DurationSearch.Text), LanguageSearch.Text, int.Parse(PeopleSearch.Text));
            return foundTours;
        }

        private string EmptyStringToZero(string text)
        {
            if(text == string.Empty)
            {
                return "0";
            }

            return text;
        }

        private void DurationSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            e.Handled = IsFloat(textBox, e);
        }

        private bool IsFloat(TextBox textBox, TextCompositionEventArgs e)
        {
            if((!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                || (e.Text == "." && textBox.Text.Contains("."))
                || (e.Text == "." && textBox.Text.Length == 0))
            {
                return true;
            }
            return false;
        }

        private void DurationSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
            }
        }

        private void PeopleSearch_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsDigit(e);
        }

        private bool IsDigit(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                return true;

            return false;
        }

        private void PeopleSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "0";
                return;
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
                TourNumberOfParticipantsWindow tourNumberOfParticipantsWindow = new TourNumberOfParticipantsWindow(SelectedTour);
                tourNumberOfParticipantsWindow.ShowDialog();

                if(Tours.Count != _repository.ToursCount())
                {
                    RefreshDataGrid(true);
                }
                else
                    RefreshDataGrid(false);
            }
        }

        private void RefreshDataGrid(bool withSearch)
        {
            Tours.Clear();
            if (withSearch)
            {
                List<Tour> foundTours = Search();

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

        private void DurationPlus_Click(object sender, RoutedEventArgs e)
        {
            if(DurationSearch.Text == "")
                DurationSearch.Text = "0";

            DurationSearch.Text = (Convert.ToDouble(DurationSearch.Text) + 0.5).ToString();
        }

        private void DurationMinus_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToDouble(DurationSearch.Text) > 0)
            {
                DurationSearch.Text = (Convert.ToDouble(DurationSearch.Text)  - 0.5).ToString();
            }
        }

        private void ParticipantsPlus_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleSearch.Text == "")
                PeopleSearch.Text = "0";

            if (Convert.ToInt32(PeopleSearch.Text) < _repository.FindMaxNumberOfParticipants())
                PeopleSearch.Text = (Convert.ToInt32(PeopleSearch.Text) + 1).ToString();
        }

        private void ParticipantsMinus_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(PeopleSearch.Text) > 0)
            {
                PeopleSearch.Text = (Convert.ToInt32(PeopleSearch.Text) - 1).ToString();
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            CountrySearch.Text = "";
            CitySearch.Text = "";
            DurationSearch.Text = "0";
            LanguageSearch.Text = "";
            PeopleSearch.Text = "0";
        }
    }
}
