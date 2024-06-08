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
using System.Windows.Threading;
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

        public bool IsDemo { get; set; }

        public RegisterAccommodationPageViewModel(User us, AccommodationViewModel acc, NavigationService navigationService, bool demo)
        {
            this.user = us;
            accommodationDTO = new AccommodationViewModel();
            accommodationRepository = new AccommodationRepository();
            SaveCommand = new MyICommand(Save_Click);
            PictureCommand = new MyICommand(Picture_Click);
            Pictures = new ObservableCollection<string>();
            NavigationService = navigationService;
            XCommand = new MyICommand<string>(DeletePicture);
            IsDemo = demo;
            if(IsDemo)
            {
                HandleTextBoxDemo();
            }
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
            RegisterAccommodationPage page = new RegisterAccommodationPage(user, IsDemo, null, NavigationService);
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

        private void HandleTextBoxDemo()
        {
            AddOnTextBox(9.5, "G", 1);
            AddOnTextBox(9.7, "a", 1);
            AddOnTextBox(9.9, "r", 1);
            AddOnTextBox(10.1, "d",1);
            AddOnTextBox(10.3, "e", 1);
            AddOnTextBox(10.5, "n", 1);
            AddOnTextBox(10.7, " ", 1);
            AddOnTextBox(10.9, "H", 1);
            AddOnTextBox(11.1, "o",1);
            AddOnTextBox(11.3, "u",1);
            AddOnTextBox(11.5, "s",1);
            AddOnTextBox(11.7, "e",1);
            AddOnTextBox(12.3, "", 2);
            AddOnTextBox(12.9, "", 3);
            AddOnTextBox(13.5, "", 4);
            AddOnTextBox(14.1, "", 5);
            AddOnTextBox(14.7, "", 6);
            AddOnTextBox(15.3, "", 7);
            AddOnTextBox(21.2, "", 8);
        }

        private void AddOnTextBox(double seconds, string letter, int num)
        {
            if (IsDemo)
            {
                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(seconds)
                };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    if (IsDemo && num == 1)
                    {
                        accommodationDTO.Name = accommodationDTO.Name + letter;
                    }
                    else if(IsDemo && num == 2)
                    {
                        accommodationDTO.CountrySearch = "Serbia";
                        
                    }
                    else if (IsDemo && num == 3)
                    {
                        accommodationDTO.CitySearch = "Novi Sad";
                        accommodationDTO.InitializeAllLocations();
                    }
                    else if (IsDemo && num == 4)
                    {
                        accommodationDTO.IsCheckedHouse = true;
                    }
                    else if (IsDemo && num == 5)
                    {
                        accommodationDTO.MaxGuestNumber = 15;
                    }
                    else if (IsDemo && num == 6)
                    {
                        accommodationDTO.MinReservationDays = 3;
                    }
                    else if (IsDemo && num == 7)
                    {
                        accommodationDTO.ReservationDaysLimit = 5;
                    }
                    else if(IsDemo && num == 8)
                    {
                        AddPicture("../../Resources/Images/house.jpg");
                        Update();
                    }


                };
                timer.Start();
            }
        }
    }
}
