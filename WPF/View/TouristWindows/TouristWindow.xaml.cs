using BookingApp.Domain.Model;
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
        public TouristMenuViewModel Tour { get; set; }

        public TouristWindow(string username)
        {
            InitializeComponent();
            Tour = new TouristMenuViewModel();
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
            MainFrame.Content = new AllToursPage(Tour.getUserId(Tour.UserName));
        }

        private void MyToursButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MyToursPage(Tour.SelectedTour, Tour.getUserId(Tour.UserName));
        }

        private void EndedToursButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EndedToursPage(Tour.getUserId(Tour.UserName));
        }
        private void VouchersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new VouchersPage(Tour.getUserId(Tour.UserName));
        }
    }
}
