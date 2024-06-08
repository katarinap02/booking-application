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
using System.Windows.Controls.Primitives;
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
        public TourRequestWindowViewModel TourRequest {  get; set; }
        public TourRequestWindow(int userId)
        {
            InitializeComponent();
            TourRequest = new TourRequestWindowViewModel();
            DataContext = TourRequest;
            TourRequest.UserId = userId;
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (this.IsActive)
                {
                    InformationMessageBoxWindow mb = new InformationMessageBoxWindow(message.Notification);
                    mb.ShowDialog();
                    //MessageBox.Show(message.Notification, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            TourRequest.InitFrame("Basic");
        }

        private void CloseWindow(CloseWindowMessage message)
        {
            if(this.IsActive == true)
            {
                Close();
            }
        }
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Default.Unregister(this);
        }
        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TourRequest.SaveToCsvCommand.CanExecute(null);
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.SaveToCsvCommand.Execute(null);
        }
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TutorialPopup.IsOpen == false)
            {
                TutorialPopup.IsOpen = true;
                mediaElement.Play();
                return;
            }
            TutorialPopup.IsOpen = false;
            mediaElement.Stop();
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Play();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }
        private void CloseTutorialButton_Click(object sender, RoutedEventArgs e)
        {
            TutorialPopup.IsOpen = false;
            mediaElement.Stop();
        }
        private void MediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            // Media failed to open
            // Show error message in a message box
            MessageBox.Show("Media failed to open: " + e.ErrorException.Message, "Media Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
