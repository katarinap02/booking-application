﻿using BookingApp.DTO;
using BookingApp.Repository;
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

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationPage.xaml
    /// </summary>
    public partial class RegisterAccommodationPage : Page
    {
        public AccommodationDTO accommodationDTO { get; set; }
        private AccommodationRepository accommodationRepository;
        public RegisterAccommodationPage()
        {
            InitializeComponent();
            DataContext = this;
            accommodationDTO = new AccommodationDTO();
            accommodationRepository = new AccommodationRepository();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (accommodationDTO.IsValid)
            {
                accommodationRepository.Add(accommodationDTO.ToAccommodation());
                MessageBox.Show("Accommodation added.");

            }
            else
            {

                MessageBox.Show("Accommodation can not be created. Not all fields are valid.");

           }

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

