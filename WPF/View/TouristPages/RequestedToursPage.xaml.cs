using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
using BookingApp.WPF.View.TouristWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;

namespace BookingApp.WPF.View.TouristPages
{
    /// <summary>
    /// Interaction logic for RequestedToursPage.xaml
    /// </summary>
    public partial class RequestedToursPage : Page
    {
        public TourRequestViewModel TourRequest { get; set; }
        public RequestedToursPage(int userId)
        {
            InitializeComponent();
            TourRequest = new TourRequestViewModel();
            DataContext = TourRequest;
            TourRequest.UserId = userId;
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            TourRequest.NotificationButton();
        }

        private void RequestTour_OnClick(object sender, RoutedEventArgs e)
        {
            TourRequestWindow tourRequestWindow = new TourRequestWindow(TourRequest.UserId);
            tourRequestWindow.ShowDialog();
        }
    }
}
