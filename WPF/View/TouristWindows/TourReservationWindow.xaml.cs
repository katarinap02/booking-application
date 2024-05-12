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
            TourReservation = new TourReservationViewModel();
            DataContext = TourReservation;
  
            TourReservation.InitializeTourReservationWindow(selectedTour, insertedNumberOfParticipants, userId);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                MessageBox.Show(message.Notification, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }
        private void CloseWindow(CloseWindowMessage message)
        {
            Close();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if(TourReservation.Book())
                Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Default.Unregister(this);
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
    }
}
