using BookingApp.DTO;
using BookingApp.Services;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristNotificationWindow.xaml
    /// </summary>
    public partial class TouristNotificationWindow : Window
    {
        public ObservableCollection<TouristNotificationViewModel> touristNotificationViewModels { get; set; }
        public TouristNotificationViewModel SelectedNotification {  get; set; }
        private readonly TouristService _touristService;
        public TouristNotificationWindow()
        {
            InitializeComponent();
            DataContext = this;
            _touristService = new TouristService();
            touristNotificationViewModels = new ObservableCollection<TouristNotificationViewModel>(_touristService.GetAllNotifications());

            ButtonInitialization();
        }

        private void ButtonInitialization()
        {
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            AddedTouristsNotificationWindow addedTouristsNotificationWindow = new AddedTouristsNotificationWindow(SelectedNotification);
            addedTouristsNotificationWindow.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
