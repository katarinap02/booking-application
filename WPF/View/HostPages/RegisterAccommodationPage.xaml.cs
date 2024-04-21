﻿using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationPage.xaml
    /// </summary>
    public partial class RegisterAccommodationPage : Page
    {
        public AccommodationViewModel accommodationDTO { get; set; }
        private AccommodationRepository accommodationRepository;
        private User user {  get; set; }
        public RegisterAccommodationPage(User us)
        {
            InitializeComponent();
            DataContext = this;
            accommodationDTO = new AccommodationViewModel();
            accommodationRepository = new AccommodationRepository();
            user = us;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
                Accommodation accommodation = accommodationDTO.ToAccommodation();
                accommodation.HostId = user.Id;
                accommodationRepository.Add(accommodation);
                MessageBox.Show("Accommodation added.");

        }

        private void Picture_Click(object sender, RoutedEventArgs e)
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

                accommodationDTO.Picture.Add(pictureUrl);
                txtPictureUrlTextBox.Text = "";
                txtPictureUrls.ItemsSource = null;
                txtPictureUrls.ItemsSource = accommodationDTO.Picture;
            }
        }
    }
}

