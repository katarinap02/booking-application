using BookingApp.Application.Services;
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
using System.Windows.Shapes;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.View.TouristWindows;
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for AddedTouristsNotificationWindow.xaml
    /// </summary>
    public partial class AddedTouristsNotificationWindow : Window
    {
        public TouristNotificationViewModel TouristNotification { get; set; }
        public AddedTouristsNotificationWindow(TouristNotificationViewModel selectedNotification)
        {
            InitializeComponent();
            TouristNotification = new TouristNotificationViewModel();
            DataContext = TouristNotification;
            TouristNotification.SelectedNotification = selectedNotification;
            TouristNotification.CurrentCheckpoint = selectedNotification.CurrentCheckpoint;
            TouristNotification.InitializeAddedTouristsWindow();
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }
        private void CloseWindow(CloseWindowMessage messsage)
        {
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
    }
}
