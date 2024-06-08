using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using BookingApp.WPF.View.TouristWindows;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        public TourViewModel Tour { get; set; }

        public AllToursPage(int userId)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            DataContext = Tour;
            Tour.UserId = userId;
            Tour.initializeAllTours();

            Tour.RefreshAllToursDataGrid(false);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (Focus())
                {
                    InformationMessageBoxWindow mb = new InformationMessageBoxWindow(message.Notification);
                    mb.ShowDialog();
                    //MessageBox.Show(message.Notification, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            });
        }
        private void CanExecute(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.ResetButton();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.SearchButton();
        }
        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.BookButton(Tour.SelectedTour);
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.DetailsButton(Tour.SelectedTour, false);
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.NotificationButton();
        }

        private void CountrySearch_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void CountrySearch_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CountrySearch.Focus();
        }

        private void Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tour.SearchButton();
        }

        private void Reset_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tour.ResetButton();
        }

        private void Notification_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            CanExecute(e);
        }

        private void Notification_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tour.NotificationButton();
        }
    }
}
