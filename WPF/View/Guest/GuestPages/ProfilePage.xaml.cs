﻿using BookingApp.WPF.ViewModel;
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
using System.Windows.Media.Animation;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page { 
        
        public User User { get; set; }  
      

        public Frame Frame { get; set; }
       
        public GuestWindow GuestWindow { get; set; }
        public ProfilePage(User user, Frame frame)
        {
            InitializeComponent();
           
            this.User = user;
            this.Frame = frame;
           
           
            Profile.Content = new ProfileInfo(User, Profile);
            Loaded += ProfilePage_Loaded;
           

        }
        private async void ProfilePage_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            menuBorder.BeginAnimation(Border.OpacityProperty, fadeInAnimation);
            Profile.BeginAnimation(Frame.OpacityProperty, fadeInAnimation); 

            await Task.Delay(500);
        }


        public void RateAccommodation_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RateAccommodationPage(User, Profile);
           // GuestWindow.profileMenu.Focus();
        }

        public void RatesByHost_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RatesByHostPage(User, Profile);
            //GuestWindow.profileMenu.Focus();
        }

        public void Requests_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new RequestsPage(User, Profile);
           // GuestWindow.profileMenu.Focus();
        }

        public void Profile_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new ProfileInfo(User, Profile);
          //  GuestWindow.profileMenu.Focus();
        }

        public void Forums_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new ProfileForumsPage(User, Profile);
        //    GuestWindow.profileMenu.Focus();
        }

        public void Settings_Click(object sender, RoutedEventArgs e)
        {
            Profile.Content = new SettingsPage(User, Profile);
           // GuestWindow.profileMenu.Focus();
        }



    }
}
