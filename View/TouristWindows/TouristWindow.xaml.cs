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
        public static ObservableCollection<Tour> Tours { get; set; }
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

            DurationSearch.Text = EmptyStringToZero(DurationSearch.Text);
            PeopleSearch.Text = EmptyStringToZero(PeopleSearch.Text);

            List<Tour>? foundTours = _repository.FindToursBy(CountrySearch.Text, CitySearch.Text, float.Parse(DurationSearch.Text), LanguageSearch.Text, int.Parse(PeopleSearch.Text));

            if (foundTours != null)
                foreach (Tour t in foundTours)
                    Tours.Add(t);
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
            if (!int.TryParse(((TextBox)sender).Text, out int _))
            {
                MessageBox.Show("Please enter a valid number for search by number of peoples.");
                ((TextBox)sender).Focus();
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
            }
        }
    }
}
