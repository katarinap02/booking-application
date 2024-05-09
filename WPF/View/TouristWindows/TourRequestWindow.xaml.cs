using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
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

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourRequestWindow.xaml
    /// </summary>
    public partial class TourRequestWindow : Window
    {
        public TourRequestViewModel TourRequest {  get; set; }
        public TourRequestWindow(int userId)
        {
            InitializeComponent();
            TourRequest = new TourRequestViewModel();
            DataContext = TourRequest;
            TourRequest.UserId = userId;
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                MessageBox.Show(message.Notification, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            });

            TourRequest.InitializeTourRequestWindow();
        }

        private void CloseWindow(CloseWindowMessage message)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Default.Unregister(this);
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister<NotificationMessage>(this);
        }
    }
}
