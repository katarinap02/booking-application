using BookingApp.DTO;
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
    /// Interaction logic for EndedToursPage.xaml
    /// </summary>
    public partial class EndedToursPage : Page
    {
        public ObservableCollection<TourViewModel> Tours { get; set; }
        public TourViewModel SelectedTour {  get; set; }
        //private readonly TourReservationRepository _repository;
        private readonly TouristService _touristService;
        public EndedToursPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            //_repository = new TourReservationRepository();
            //Tours = new ObservableCollection<Tour>(_repository.FindMyEndedTours(userId));
            _touristService = new TouristService();
            Tours = new ObservableCollection<TourViewModel>(_touristService.FindMyEndedTours(userId));

        }

        private void RateButton_Click(object sender, RoutedEventArgs e)
        {
            GuideRateWindow guideRateWindow = new GuideRateWindow(SelectedTour);
            guideRateWindow.ShowDialog();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
