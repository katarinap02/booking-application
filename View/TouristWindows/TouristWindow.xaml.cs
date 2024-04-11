using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public TourViewModel Tour { get; set; }

        public string Username;
        public TouristWindow(string username)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;

            Username = username;

            MainFrame.Content = new AllToursPage();

        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AllToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new AllToursPage();
            AllToursButton.Background = darkerBlue();
        }

        private void MyToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new MyToursPage(Tour.SelectedTour, Tour.getUserId(Username));
            MyToursButton.Background = darkerBlue();
        }

        private void EndedToursButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new EndedToursPage(Tour.getUserId(Username));
            EndedToursButton.Background = darkerBlue();
        }
        private void VouchersButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonColors();
            MainFrame.Content = new VouchersPage(Tour.getUserId(Username));
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
