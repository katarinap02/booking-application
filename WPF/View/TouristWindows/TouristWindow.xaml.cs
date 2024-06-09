using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using BookingApp.WPF.View.TouristPages;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using BookingApp.WPF.View.TouristWindows;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TouristWindow.xaml
    /// </summary>
    public partial class TouristWindow : Window
    {
        public TouristMenuViewModel Tour { get; set; }

        public TouristWindow(string username, int userId)
        {
            InitializeComponent();
            Tour = new TouristMenuViewModel(username, userId);
            DataContext = Tour;

            Messenger.Default.Register<LogoutMessage>(this, LogoutWindow);
            MainFrame.Content = new AllToursPage(Tour.getUserId(Tour.UserName));
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (IsActive)
                {
                    InformationMessageBoxWindow mb = new InformationMessageBoxWindow(message.Notification);
                    mb.ShowDialog();
                    //MessageBox.Show(message.Notification, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });

        }
        private void LogoutWindow(LogoutMessage message)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Default.Unregister(this);
        }
        private void AllTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Content = new AllToursPage(Tour.getUserId(Tour.UserName));
            AllToursButton.IsChecked = true;
        }

        private void MyTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MyTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Content = new MyToursPage(Tour.SelectedTour, Tour.getUserId(Tour.UserName));
            MyToursButton.IsChecked = true;
        }

        private void EndedTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EndedTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Content = new EndedToursPage(Tour.getUserId(Tour.UserName));
            EndedToursButton.IsChecked = true;
        }

        private void RequestedTours_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RequestedTours_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Content = new RequestedToursPage(Tour.getUserId(Tour.UserName));
            RequestedToursButton.IsChecked = true;
        }

        private void Vouchers_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Vouchers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainFrame.Content = new VouchersPage(Tour.getUserId(Tour.UserName));
            VouchersButton.IsChecked = true;
        }

        private void Logout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Notifications_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Notifications_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TouristNotificationWindow touristNotificationWindow = new TouristNotificationWindow(Tour.UserId);
            touristNotificationWindow.ShowDialog();
        }
        private void CloseShortcutsButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.IsShortcutsOpen = false;
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Tour.IsShortcutsOpen)
            {
                e.Handled = true;
            }
        }
    }
}
