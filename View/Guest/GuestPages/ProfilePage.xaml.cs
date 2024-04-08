using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using BookingApp.Observer;
using BookingApp.Model;
using System.Security.Cryptography;
using BookingApp.Services;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page { 
        
        public User User { get; set; }  
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }
        public Frame Frame { get; set; }

        public ProfilePage(User user, AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, AccommodationRateService accommodationRateService, Frame frame)
        {
            InitializeComponent();
           
            this.User = user;
            this.Frame = frame;
            this.AccommodationReservationService = accommodationReservationService;
            this.AccommodationService = accommodationService;
            this.AccommodationRateService = accommodationRateService;
           
            Profile.Content = new ProfileInfo(AccommodationReservationService, AccommodationService, User, Profile);
           
           

        }

      

        public void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RateAccommodationPage(User, AccommodationReservationService, AccommodationService, AccommodationRateService, Profile);
        }

        public void RatesByHost_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RatesByHostPage(User, AccommodationRateService, Profile);
        }

      
       
    }
}
