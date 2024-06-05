using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using GalaSoft.MvvmLight.Messaging;
using BookingApp.WPF.View.TouristWindows;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window
    {
        public TourReservationViewModel TourReservation { get; set; }
        public int UserId;


        public TourReservationWindow(TourViewModel selectedTour, int insertedNumberOfParticipants, int userId)
        {
            InitializeComponent();
            TourReservation = new TourReservationViewModel(selectedTour, insertedNumberOfParticipants, userId);
            DataContext = TourReservation;

            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (IsActive)
                {
                    MessageBox.Show(message.Notification, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
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
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(TutorialPopup.IsOpen == false)
            {
                TutorialPopup.IsOpen = true;
                mediaElement.Play();
                return;
            }
            TutorialPopup.IsOpen = false;
            mediaElement.Stop();
        }
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        private void AddParticipant_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourReservation.AddParticipantCommand.Execute(null);
        }

        private void AddParticipant_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
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
    }
}
