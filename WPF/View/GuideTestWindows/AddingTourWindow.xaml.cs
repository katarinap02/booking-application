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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.WPF.View.GuideTestWindows
{
    public partial class AddingTourWindow: Window
    {
        private readonly TourRepository _tourRepository;
        public TourViewModel Tour { get; set; }
        private List<DateTime> selectedDates = new List<DateTime>();
        private int GuideId;
        public StringToNumberViewModel StringDuration { get; set; }
        public StringToNumberViewModel StringTouristsNumber { get; set; }
        public StringViewModel typedCheckpoint {  get; set; }
        public ObservableCollection<string> Pictures {  get; set; }
        public AddingTourWindow(int guide_id)
        {
            Pictures = new ObservableCollection<string>();
            typedCheckpoint = new StringViewModel();
            StringDuration = new StringToNumberViewModel();
            StringTouristsNumber = new StringToNumberViewModel();
            GuideId = guide_id;
            DataContext = this;
            _tourRepository = new TourRepository();
            Tour = new TourViewModel();
            InitializeComponent();
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            if (Tour.Checkpoints.Count < 2)
            {
                MessageBox.Show("At least 2 checkpoints are needed in order to make a new tour.");
            }
            else
            {
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

            }
            MessageBox.Show("Tour added");            
            Close();
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
        
    }
}
