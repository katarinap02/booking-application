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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page { 
        
        public User User { get; set; }  
        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public AccommodationRateRepository AccommodationRateRepository { get; set; }
        public Frame Frame { get; set; }

        public ProfilePage(User user, AccommodationReservationRepository accommodationReservationRepository, AccommodationRepository accommodationRepository, AccommodationRateRepository accommodationRateRepository, Frame frame)
        {
            InitializeComponent();
           
            this.User = user;
            this.Frame = frame;
            this.AccommodationReservationRepository = accommodationReservationRepository;
            this.AccommodationRepository = accommodationRepository;
            this.AccommodationRateRepository = accommodationRateRepository;
           
            Profile.Content = new ProfileInfo(AccommodationReservationRepository, User, Profile);
           
           

        }

      

        public void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RateAccommodationPage(User, AccommodationReservationRepository, AccommodationRepository, AccommodationRateRepository, Profile);
        }

        public void RatesByHost_Click(object sender, RoutedEventArgs e)
        {
           // Frame.Content = new RatesByHostPage(User, AccommodationRateRepository, Frame);
        }

      
       
    }
}
