using BookingApp.Domain.Model.Features;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class RegisterAccommodationPageViewModel :  INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public AccommodationViewModel accommodationDTO { get; set; }
        private AccommodationRepository accommodationRepository;

        public ObservableCollection<string> Pictures { get; set; }
        private User user { get; set; }

        public MyICommand SaveCommand { get; set; }

        public MyICommand PictureCommand { get; set; }

        public MyICommand<string> XCommand { get; set; }

        public RegisterAccommodationPageViewModel(User us)
        {
            this.user = us;
            accommodationDTO = new AccommodationViewModel();
            accommodationRepository = new AccommodationRepository();
            SaveCommand = new MyICommand(Save_Click);
            PictureCommand = new MyICommand(Picture_Click);
            Pictures = new ObservableCollection<string>();
            XCommand = new MyICommand<string>(DeletePicture);
            Update();
        }

        private void Save_Click()
        {
            Accommodation accommodation = accommodationDTO.ToAccommodation();
            accommodation.HostId = user.Id;
            accommodationRepository.Add(accommodation);
            MessageBox.Show("Accommodation added.");

        }

        private void Picture_Click()
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
                Update();
              //  txtPictureUrlTextBox.Text = "";
              //  txtPictureUrls.ItemsSource = null;
              //  txtPictureUrls.ItemsSource = accommodationDTO.Picture;
            }
        }

        public void Update()
        {
            Pictures.Clear();
            foreach(string picture in accommodationDTO.Picture)
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

        public string ConvertToAbsolutePath(string relativePath)
        {
            string pattern = "/";


            string replacedPath = Regex.Replace(relativePath, pattern, "\\");


            if (replacedPath.StartsWith("..\\..\\Resources/Images/"))
            {
                replacedPath = replacedPath.Replace("..\\..\\", "");
            }

            return replacedPath;
        }


        public void DeletePicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl))
            {   string newUrl = ConvertToAbsolutePath(pictureUrl);
                foreach(string url in accommodationDTO.Picture)
                {
                    if (newUrl.Contains(url))
                    {
                        accommodationDTO.Picture.Remove(url);
                        break;
                    }
                        
                }
            }
                    
            Update();
        }
    }
}
