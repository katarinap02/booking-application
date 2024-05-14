using BookingApp.WPF.ViewModel;
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
using System.Security.Cryptography;
using BookingApp.Application.Services;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.Guest.GuestPages;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page { 
        
        public User User { get; set; }  
      

        public Frame Frame { get; set; }

        public ProfilePage(User user, Frame frame)
        {
            InitializeComponent();
           
            this.User = user;
            this.Frame = frame;
           
           
            Profile.Content = new ProfileInfo(User, Profile);
           
           

        }

      

        public void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RateAccommodationPage(User, Profile);
        }

        public void RatesByHost_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RatesByHostPage(User, Profile);
        }

        public void Requests_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RequestsPage(User, Profile);
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new ProfileInfo(User, Profile);
        }

        public void Forums_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new ProfileForumsPage(User, Profile);
        }



    }
}
