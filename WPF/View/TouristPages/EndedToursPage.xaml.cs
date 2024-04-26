using BookingApp.Domain.Model;
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
    /// Interaction logic for EndedToursPage.xaml
    /// </summary>
    public partial class EndedToursPage : Page
    {
        public TourViewModel Tour { get; set; }

        public EndedToursPage(int userId)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;

            Tour.UserId = userId;


            Tour.RefreshEndedTours();
        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Tour.IsRated())
            {
                MessageBox.Show("This tour is already rated");
                return;
            }
            GuideRateWindow guideRateWindow = new GuideRateWindow(Tour.SelectedTour, Tour.UserId);
            guideRateWindow.ShowDialog();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.NotificationButton();
        }

    }
}
