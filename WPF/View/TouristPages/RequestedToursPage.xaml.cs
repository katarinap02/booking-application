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
            TourRequest = new TourRequestViewModel(userId);
            DataContext = TourRequest;

            TourRequest.InitializeRequestedToursPage();
        }
        private void CanExecute(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            TourRequest.NotificationButton();
        }

        private void RequestTour_OnClick(object sender, RoutedEventArgs e)
        {
            TourRequest.RequestTourClick();
        }
        private void Notification_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Notification_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.NotificationButton();
        }

        private void Request_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Request_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.RequestTourClick();
        }

        private void BasicTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void BasicTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RequestTabControl.SelectedIndex = 0;
        }

        private void ComplexTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void ComplexTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RequestTabControl.SelectedIndex = 1;
        }

        private void Statistics_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Statistics_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.StatisticsClick();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            RequestedTourDetailsWindow requestedTourDetailsWindow = new RequestedTourDetailsWindow(TourRequest.SelectedTourRequest);
            requestedTourDetailsWindow.ShowDialog();
        }

        private void ComplexDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            ComplexTourRequestDetailsWindow window = new ComplexTourRequestDetailsWindow(TourRequest.SelectedTourRequest);
            window.ShowDialog();
        }
    }
}
