﻿using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for MyToursPage.xaml
    /// </summary>
    public partial class MyToursPage : Page
    {
        public TourViewModel Tour { get; set; }
        public MyToursPage(TourViewModel selectedTour, int userId)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;

            Tour.UserId = userId;

            Tour.SelectedTour = selectedTour;

            Tour.RefreshMyTours();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.DetailsButton(Tour.SelectedTour, true);
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.NotificationButton();
        }
    }
}
