using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        public TourDTO TourDTO { get; set; }
        public TourDTO SelectedTour {  get; set; }
        private readonly TourRepository _repository;
        public TouristWindow()
        {
            InitializeComponent();
            DataContext = this;
            _repository = new TourRepository();
            TourDTO = new TourDTO();
            Tours = new ObservableCollection<Tour>(_repository.GetAll());

            DurationSearch.TextChanged += TextBox_TextChanged;
            PeopleSearch.TextChanged += TextBox_TextChanged;

            ValidateTextBoxes();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tours.Clear();
            DurationSearch.Text = DurationSearch.Text == "" ? "0" : DurationSearch.Text;
            PeopleSearch.Text = PeopleSearch.Text == "" ? "0" : PeopleSearch.Text;
            List<Tour>? foundTours = _repository.FindToursBy(CountrySearch.Text, CitySearch.Text, float.Parse(DurationSearch.Text), LanguageSearch.Text, int.Parse(PeopleSearch.Text));
            if (foundTours != null)
                foreach (Tour t in foundTours)
                    Tours.Add(t);
        }

        private void ValidateTextBoxes()
        {
            searchButton.IsEnabled = TourDTO.isValid;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxes();
        }
    }
}
