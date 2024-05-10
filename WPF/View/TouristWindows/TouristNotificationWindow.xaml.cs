using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.TouristWindows;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristNotificationWindow.xaml
    /// </summary>
    public partial class TouristNotificationWindow : Window
    {
        public TouristNotificationViewModel TouristNotification { get; set; }
        public TouristNotificationWindow(int userId)
        {
            InitializeComponent();
            TouristNotification = new TouristNotificationViewModel();
            DataContext = TouristNotification;

            TouristNotification.UserId = userId;

            TouristNotification.InitializeTouristNotificationWindow();

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            TouristNotification.ExecuteDetailsButton();
        }
    }
}
