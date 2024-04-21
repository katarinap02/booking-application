using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        public TourViewModel Tour { get; set; }

        public AllToursPage(int userId)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;
            Tour.UserId = userId;
            Tour.initializeAllTours();

            Tour.RefreshAllToursDataGrid(false);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.ResetButton();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.SearchButton();
        }


        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.BookButton(Tour.SelectedTour);
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.DetailsButton(Tour.SelectedTour, false);
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.NotificationButton();
        }
    }
}
