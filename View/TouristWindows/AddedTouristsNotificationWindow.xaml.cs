using BookingApp.DTO;
using BookingApp.Services;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for AddedTouristsNotificationWindow.xaml
    /// </summary>
    public partial class AddedTouristsNotificationWindow : Window
    {
        public TouristNotificationViewModel SelectedNotification { get; set; }
        public ObservableCollection<string> Tourists { get; set; }
        private readonly TouristService touristService;
        public AddedTouristsNotificationWindow(TouristNotificationViewModel selectedNotification)
        {
            InitializeComponent();
            DataContext = this;
            SelectedNotification = selectedNotification;
            touristService = new TouristService();
            Tourists = new ObservableCollection<string>(touristService.GetParticipantsThatJoined(selectedNotification.ToTouristNotification()));

            tourNameLabel.Content = selectedNotification.TourName;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
