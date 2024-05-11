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
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
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
    }
}
