using BookingApp.Domain.Model.Features;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.HostPages;
using BookingApp.WPF.View.HostPages;
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
using System.Windows.Navigation;
using Wpf.Ui.Controls;

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

        public NavigationService NavigationService { get; set; }

        public RegisterAccommodationPageViewModel(User us, AccommodationViewModel acc, NavigationService navigationService)
        {
            this.user = us;
            accommodationDTO = new AccommodationViewModel();
            accommodationRepository = new AccommodationRepository();
            SaveCommand = new MyICommand(Save_Click);
            PictureCommand = new MyICommand(Picture_Click);
            Pictures = new ObservableCollection<string>();
            NavigationService = navigationService;
            XCommand = new MyICommand<string>(DeletePicture);
            if(acc != null)
            {
                accommodationDTO.CountrySearch = acc.Country;
                accommodationDTO.CitySearch = acc.City;
            }
            Update();
        }

        private void Save_Click()
        {
            Accommodation accommodation = accommodationDTO.ToAccommodation();
            accommodation.HostId = user.Id;
            accommodationRepository.Add(accommodation);
            System.Windows.MessageBox.Show("Accommodation added.");
            if(NavigationService != null) { 
            RegisterAccommodationPage page = new RegisterAccommodationPage(user, null, NavigationService);
            this.NavigationService.Navigate(page);
            }



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
                    System.Windows.MessageBox.Show($"Error loading image: {ex.Message}");
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
                System.Windows.MessageBox.Show("Please select an image from the resources privided within this app!");
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

        public void LoadLocation(AccommodationViewModel acc)
        {
            if(acc != null) { 
            accommodationDTO.InitializeAllLocations(acc.Country);
            }
            else accommodationDTO.InitializeAllLocations("");


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
