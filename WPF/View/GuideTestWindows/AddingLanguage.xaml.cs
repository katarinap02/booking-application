using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for AddingLanguage.xaml
    /// </summary>
    public partial class AddingLanguage : Window
    {
        private readonly TourRepository _tourRepository;
        public TourViewModel Tour { get; set; }
        private List<DateTime> selectedDates = new List<DateTime>();
        private int GuideId;
        public StringToNumberViewModel StringDuration { get; set; }
        public StringToNumberViewModel StringTouristsNumber { get; set; }
        public StringViewModel typedCheckpoint { get; set; }
        public ObservableCollection<string> Pictures { get; set; }
        public AddingLanguage(int guide_id, string language)
        {
            Tour = new TourViewModel();
            Tour.Language = language;
            Pictures = new ObservableCollection<string>();
            typedCheckpoint = new StringViewModel();
            StringDuration = new StringToNumberViewModel();
            StringTouristsNumber = new StringToNumberViewModel();
            GuideId = guide_id;
            DataContext = this;
            _tourRepository = new TourRepository();
            InitializeComponent();
            HideAllLabels();
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            if (!Check())
            {
                return;
            }
            if (Tour.Checkpoints.Count < 2)
            {
                lblCheckpointsError.Visibility = Visibility.Visible;
                return;
            }
            else
            {

                if (selectedDates.Count == 0)
                {
                    lblDatesError.Visibility = Visibility.Visible;
                    return;
                }
                int groupId = _tourRepository.NextId();
                foreach (DateTime date in selectedDates)
                {
                    Tour.GuideId = GuideId;
                    Tour.GroupId = groupId;
                    Tour.Date = date;
                    Tour.Duration = StringDuration.convertToInt();
                    Tour.MaxTourists = StringTouristsNumber.convertToInt();
                    Tour.Id = _tourRepository.NextPersonalId();
                    Tour.AvailablePlaces = Tour.MaxTourists;
                    _tourRepository.Add(Tour.ToTour());
                }
                MessageBox.Show("Tour added sucessfully. It is now visible in my tours segment. Shortcut for accessing that is Ctrl+M", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (selectedDates.Count == 0)
            {
                lblDatesError.Visibility = Visibility.Visible;
                return;
            }
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            DateTime newDate = dateTimePicker.Value ?? DateTime.MinValue;
            selectedDates.Add(newDate);
            dates.ItemsSource = null;
            dates.ItemsSource = selectedDates;

        }

        private void AddCheckpoint_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrEmpty(typedCheckpoint.SString))
            {
                Tour.Checkpoints.Add(typedCheckpoint.SString);
                typedCheckpoint.SString = "";
                CheckpointsList.ItemsSource = null; // lista 
                CheckpointsList.ItemsSource = Tour.Checkpoints;
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                try
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(selectedFileName));

                    string imageUrl = selectedFileName;

                    imageUrl = convertToRelativePath(imageUrl);
                    AddPicture(imageUrl);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
        }


        public string convertToRelativePath(string input)
        {
            int index = input.IndexOf("Resources");
            if (index != -1)
            {
                return input.Substring(index);
            }
            else
            {
                MessageBox.Show("Please select an image from the resources privided within this app!");
                return input;
            }
        }


        private void AddPicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl))
            {
                Tour.Pictures.Add(pictureUrl);
                Update();
            }
        }

        public void Update()
        {
            Pictures.Clear();
            foreach (string picture in Tour.Pictures)
            {
                string pictureNew = ConvertToRelativePath(picture);
                Pictures.Add(pictureNew);
            }
        }
        public string ConvertToRelativePath(string inputPath)
        {

            string pattern = @"\\";


            string replacedPath = Regex.Replace(inputPath, pattern, "/");


            if (replacedPath.StartsWith("Resources/Images/"))
            {
                replacedPath = "../../" + replacedPath;
            }

            return replacedPath;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // Get the item associated with the button
                var item = button.CommandParameter;

                // Remove the item from the ListBox's ItemsSource
                if (pictures.ItemsSource is ObservableCollection<string> picturesList)
                {
                    picturesList.Remove(item as string);
                    string path = convertToRelativePath(item as string);
                    Tour.Pictures.Remove(path);

                }
            }
        }

        public void HideAllLabels()
        {
            lblTourNameError.Visibility = Visibility.Collapsed;
            lblCityError.Visibility = Visibility.Collapsed;
            lblCountryError.Visibility = Visibility.Collapsed;
            lblLanguageError.Visibility = Visibility.Collapsed;
            lblTouristsNumberError.Visibility = Visibility.Collapsed;
            lblDurationError.Visibility = Visibility.Collapsed;
            lblDescriptionError.Visibility = Visibility.Collapsed;
            lblCheckpointsError.Visibility = Visibility.Collapsed;
            lblDatesError.Visibility = Visibility.Collapsed;
        }


        private bool Check()
        {
            bool isValid = true;

            // Check if tour name is provided
            if (string.IsNullOrWhiteSpace(txtTourName.Text))
            {
                lblTourNameError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblTourNameError.Visibility = Visibility.Collapsed;
            }

            // Check if city is provided
            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                lblCityError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblCityError.Visibility = Visibility.Collapsed;
            }

            // Check if country is provided
            if (string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                lblCountryError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblCountryError.Visibility = Visibility.Collapsed;
            }

            // Check if language is provided
            if (string.IsNullOrWhiteSpace(txtLanguage.Text))
            {
                lblLanguageError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblLanguageError.Visibility = Visibility.Collapsed;
            }

            // Check if number of tourists is provided
            if (string.IsNullOrWhiteSpace(txtTouristsNumber.Text))
            {
                lblTouristsNumberError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblTouristsNumberError.Visibility = Visibility.Collapsed;
            }

            // Check if duration is provided
            if (string.IsNullOrWhiteSpace(txtDuration.Text))
            {
                lblDurationError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblDurationError.Visibility = Visibility.Collapsed;
            }

            // Check if description is provided
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblDescriptionError.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                lblDescriptionError.Visibility = Visibility.Collapsed;
            }

            // If all validations pass, proceed with adding the tour
            return isValid;
        }

    }
}
