using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for MyToursPage.xaml
    /// </summary>
    public partial class MyToursPage : Page
    {
        public ObservableCollection<TourViewModel> Tours { get; set; }

        public TourViewModel SelectedTour { get; set; }
        private readonly TouristService _touristService;
        public MyToursPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _touristService = new TouristService();
            Tours = new ObservableCollection<TourViewModel>(_touristService.FindMyTours(userId));
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            TourDetailsWindow tourDetailsWindow = new TourDetailsWindow(SelectedTour, true);
            tourDetailsWindow.ShowDialog();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
