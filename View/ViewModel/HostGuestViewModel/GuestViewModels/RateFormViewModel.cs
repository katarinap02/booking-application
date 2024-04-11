using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.View.GuestPages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.View.ViewModel.HostGuestViewModel
{
    public class RateFormViewModel : IObserver
    {
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public User User { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public HostService HostService { get; set; }

        public Frame Frame { get; set; }

        public ObservableCollection<string> Images { get; set; }

        public AccommodationRateViewModel AccommodationRate { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }

        public RateAccommodationForm Page { get; set; }

        public RateFormViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation, RateAccommodationForm page)
        {
            Images = new ObservableCollection<string>();    
            AccommodationRateService = new AccommodationRateService();
            AccommodationService = new AccommodationService();
            HostService = new HostService();
            AccommodationRate = new AccommodationRateViewModel();
            User = user;
            Frame = frame;
            Page = page;
            SelectedReservation = selectedReservation;
            SelectedAccommodation = new AccommodationViewModel(AccommodationService.GetById(SelectedReservation.AccommodationId));
            

        }

        public  void Save_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRate.ReservationId = SelectedReservation.Id;
            AccommodationRate.GuestId = User.Id;
            AccommodationRate.HostId = AccommodationService.GetById(SelectedReservation.AccommodationId).HostId;
            AccommodationRateService.Add(AccommodationRate.ToAccommodationRate());
            AccommodationRate rate = AccommodationRate.ToAccommodationRate();
            Host host = HostService.GetById(rate.HostId);
            HostService.BecomeSuperHost(host);
            MessageBox.Show("Rate added");

        }

        public void AddPicture_Click(object sender, RoutedEventArgs e)
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

                    imageUrl = ConvertToRelativePath(imageUrl);
                    AddPicture(imageUrl);


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }

          
        }
        public string ConvertToRelativePath(string input)
        {
            int index = input.IndexOf("Resources");
            if (index != -1)
            {
                return input.Substring(index);
            }
            else
            {
                MessageBox.Show("Please select an image from the resources provided within this app!");
                return input;
            }
        }

        private void AddPicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl))
            {

                AccommodationRate.Images.Add(pictureUrl);

                Update();

            }
        }

        public void Update()
        {
            Images.Clear();
            foreach(string image in AccommodationRate.Images)
            {
                Images.Add(image);
            }
        }
    }
}
