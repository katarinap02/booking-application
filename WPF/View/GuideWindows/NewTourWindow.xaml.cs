using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using BookingApp.Domain.Model.Features;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.View
{
    public partial class NewTourWindow
    {
        private readonly TourRepository _tourRepository;
        public TourViewModel Tour { get; set; }
        private List<DateTime> selectedDates = new List<DateTime>();
        private readonly TourRequestService tourRequestService;
        private User Guide;
        private bool IsByRequest;

        public NewTourWindow(User guide) { // string koji se parsira po , ako je location itd
            InitializeComponent();
            DataContext = this;
            tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            _tourRepository = new TourRepository();
            Tour = new TourViewModel();
            Guide = guide;
            
        }

        public void stringChecker(string sstring) {
            if (!string.IsNullOrEmpty(sstring))
            {
                IsByRequest = true;
                string[] parts = sstring.Split(',');
                if(parts.Length==2)
                {
                    UpdateLocation(parts);
                }
                else
                {
                    UpdateLanguage(parts[0]);
                }
            }
            else
            {
                IsByRequest = false;
            }
        }

        public void UpdateLanguage(string language)
        {
            Tour.Language = language;
            txtLanguage.Text = language;
            txtLanguage.IsReadOnly = true;
        }

        public void UpdateLocation(string[] parts)
        {
            Tour.City = parts[0];
            Tour.Country = parts[1];
            txtCity.IsReadOnly = true;
            txtCity.Text = parts[0];
            txtCountry.IsReadOnly = true;
            txtCountry.Text = parts[1];
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
                    Tour.GuideId = Guide.Id;
                    Tour.GroupId = groupId;
                    Tour.Date = date;
                    Tour.Id = _tourRepository.NextPersonalId();
                    Tour.AvailablePlaces = Tour.MaxTourists;
                    if (IsByRequest)
                    {
                        tourRequestService.CreateTourByStatistics(Tour.ToTour(), "");
                    }
                    else
                    {
                        _tourRepository.Add(Tour.ToTour());
                    }
                    
                }
            
            }
            MessageBox.Show("Tour added");
            Close();
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
                txtCheckpointTextBox.Text = ""; 
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

                    string imageUrl = selectedFileName;

                    imageUrl = convertToRelativePath(imageUrl);
                    AddPicture(imageUrl);

                    //imgPhoto.Source = bitmapImage; //kasnije verzije gde se prikazuje
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
                txtPictureUrlTextBox.Text = ""; 
                txtPictureUrls.ItemsSource = null;
                txtPictureUrls.ItemsSource = Tour.Pictures;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
