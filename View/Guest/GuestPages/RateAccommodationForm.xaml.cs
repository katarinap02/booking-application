using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RateAccommodationForm.xaml
    /// </summary>
    public partial class RateAccommodationForm : Page
    {
        public AccommodationReservationDTO SelectedReservation { get; set; }
        public User User { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public HostService HostService { get; set; }

        public Frame Frame { get; set; }

        public AccommodationRateDTO AccommodationRate { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }
        public RateAccommodationForm(User user, AccommodationReservationDTO selectedReservation, AccommodationService accommodationService, AccommodationRateService accommodationRateService, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.SelectedReservation = selectedReservation;
            this.AccommodationService = accommodationService;
            this.HostService = new HostService();
            this.Frame = frame;
            this.AccommodationRateService = accommodationRateService;
            this.AccommodationRate = new AccommodationRateDTO();
            DataContext = this;

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AccommodationRate.ReservationId = SelectedReservation.Id;
            AccommodationRate.GuestId = User.Id;
            AccommodationRate.HostId = AccommodationService.GetById(SelectedReservation.AccommodationId).HostId;
            AccommodationRateService.Add(AccommodationRate.ToAccommodationRate());
            AccommodationRate rate = AccommodationRate.ToAccommodationRate();
            Host host = HostService.GetById(rate.HostId);
            HostService.BecomeSuperHost(host);
            MessageBox.Show("jej");

        }

        private void AddPicture_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Please select an image from the resources privided within this app!");
                return input;
            }
        }

        private void AddPicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl))
            {

                AccommodationRate.Images.Add(pictureUrl);
                
              //  pictureListBox.ItemsSource = AccommodationRate.Images;
                
            }
        }
    }
}
