using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.HostPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.HostPages;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class HostWindowViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Frame HostFrame { get; set; }

        public Menu LeftDock { get; set; }
        public User User { get; set; }

        public StackPanel RatingPanel { get; set; }

        public StackPanel RenovationPanel { get; set; } 
        
        public HostWindowViewModel(User user, Frame frame, Menu dock, StackPanel panel, StackPanel panelR)
        {

            HostFrame = frame;
            LeftDock = dock;
            User = user;
            RatingPanel = panel;
            RenovationPanel = panelR;
            Update();

        }

        public void Update()
        {

            FirstPage firstPage = new FirstPage(User, HostFrame, LeftDock, RatingPanel);

            HostFrame.Navigate(firstPage);
        }

        public void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstPage firstPage = new FirstPage(User, HostFrame, LeftDock, RatingPanel);
            HostFrame.Navigate(firstPage);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
            RenovationPanel.Visibility = Visibility.Collapsed;
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationPage page = new RegisterAccommodationPage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }
        public void GuestRatings_Click(object sender, RoutedEventArgs e)
        {
            RateDisplayPage page = new RateDisplayPage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
            RenovationPanel.Visibility = Visibility.Collapsed;
        }

        public void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestRatePage page = new GuestRatePage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
            RenovationPanel.Visibility = Visibility.Collapsed;
        }

        public void Delay_Click(object sender, RoutedEventArgs e)
        {
            DelayPage page = new DelayPage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
            RenovationPanel.Visibility = Visibility.Collapsed;
        }

        public void ScheduleRenovation_Click(object sender, RoutedEventArgs e)
        {
            ScheduleRenovationPage page = new ScheduleRenovationPage(User);
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
            RenovationPanel.Visibility = Visibility.Collapsed;
        }

        public void More_Click(object sender, RoutedEventArgs e)
        {
            if (LeftDock.Visibility == Visibility.Visible)
            {
                LeftDock.Visibility = Visibility.Collapsed;
            }
            else
            {
                LeftDock.Visibility = Visibility.Visible;
            }
        }

        public void Rating_Click(object sender, RoutedEventArgs e)
        {
            if (RatingPanel.Visibility == Visibility.Visible)
            {
                RatingPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                RatingPanel.Visibility = Visibility.Visible;
            }
        }

        public void Renovation_Click(object sender, RoutedEventArgs e)
        {
            if (RenovationPanel.Visibility == Visibility.Visible)
            {
                RenovationPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                RenovationPanel.Visibility = Visibility.Visible;
            }
        }

    }
}
