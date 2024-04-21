using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public TourViewModel Tour { get; set; }

        public TouristWindow(string username)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;

            Tour.UserName = username;

            MainFrame.Content = new AllToursPage(Tour.getUserId(Tour.UserName));

        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new AllToursPage(Tour.getUserId(Tour.UserName));
            AllToursButton.Background = darkerBlue();
        }

        private void MyToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new MyToursPage(Tour.SelectedTour, Tour.getUserId(Tour.UserName));
            MyToursButton.Background = darkerBlue();
        }

        private void EndedToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new EndedToursPage(Tour.getUserId(Tour.UserName));
            EndedToursButton.Background = darkerBlue();
        }
        private void VouchersButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new VouchersPage(Tour.getUserId(Tour.UserName));
            VouchersButton.Background = darkerBlue();
        }

        private void resetButtonColors()
        {
            AllToursButton.Background = lighterBlue();
            MyToursButton.Background = lighterBlue();
            EndedToursButton.Background = lighterBlue();
            RequestedToursButton.Background = lighterBlue();
            VouchersButton.Background = lighterBlue();
            LogOutButton.Background = lighterBlue();
        }

        private SolidColorBrush darkerBlue()
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#344299"));
        }
        private SolidColorBrush lighterBlue()
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5FD9"));
        }

    }
}
