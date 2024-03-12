using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.DTO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace BookingApp.View
{
    public partial class NewTourWindow
    {
        private readonly TourRepository _tourRepository;
        public TourDTO Tour { get; set; }
        private List<DateTime> selectedDates = new List<DateTime>();

        public NewTourWindow() {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            Tour = new TourDTO();
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            if(Tour.Checkpoints.Count < 2) {
                MessageBox.Show("At least 2 checkpoints are needed in order to make a new tour.");
            }
            else
            {
                int groupId = _tourRepository.NextId();
                foreach (DateTime date in selectedDates)
                {
                    Tour.GroupId = groupId;
                    Tour.Date = date;
                    Tour.Id = _tourRepository.NextPersonalId();
                    _tourRepository.Add(Tour.ToTour());
                }
            
            }
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            //selectedDates.Clear();
            DateTime newDate = dateTimePicker.Value ?? DateTime.MinValue;
            selectedDates.Add(newDate);
            txtDates.ItemsSource = null;
            txtDates.ItemsSource = selectedDates;

        }

        private void AddCheckpoint_Click(object sender, RoutedEventArgs e)
        {
            string checkpoint = txtCheckpointTextBox.Text;
            if (!string.IsNullOrEmpty(checkpoint))
            {
                Tour.Checkpoints.Add(checkpoint);
                txtCheckpointTextBox.Text = ""; // Clear the input textbox after adding the checkpoint
                txtCheckpoints.ItemsSource = null;
                txtCheckpoints.ItemsSource = Tour.Checkpoints;
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

                    // Here you can use the selectedFileName variable to get the URL of the selected image
                    string imageUrl = selectedFileName;
                    // Now you can use imageUrl as needed, such as saving it to a database or displaying it to the user
                    MessageBox.Show(imageUrl);

                    // BITNO dodati parser koji pretvara u relativni URL
                    
                    // Display the image in the Image control
                    //imgPhoto.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
        }





        private void AddPicture_Click(object sender, RoutedEventArgs e)
        {
            string pictureUrl = txtPictureUrlTextBox.Text;
            if (!string.IsNullOrEmpty(pictureUrl))
            {
                Tour.Pictures.Add(pictureUrl);
                txtPictureUrlTextBox.Text = ""; 
                txtPictureUrls.ItemsSource = null;
                txtPictureUrls.ItemsSource = Tour.Pictures;
            }
        }

    }
}
