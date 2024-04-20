using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestWindows;
using BookingApp.View.HostPages;
using BookingApp.View.HostPages.RatePages;
using BookingApp.View.HostWindows;
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
using BookingApp.View.ViewModel;
using BookingApp.View.ViewModel.HostGuestViewModel.HostViewModels;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        
        public HostPageViewModel hostPageViewModel { get; set; }

        public HostWindow(User user)
        {

            InitializeComponent();
            hostPageViewModel = new HostPageViewModel(user, HostFrame, LeftDock, RatingPanel);
            DataContext = hostPageViewModel;

        }


        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.HomeButton_Click(sender, e);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.RegisterButton_Click(sender, e);
        }
        private void GuestRatings_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.GuestRatings_Click(sender, e);
        }

        private void Delay_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.Delay_Click(sender, e); 
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void More_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.More_Click(sender, e);
        }

        private void Rating_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.Rating_Click(sender, e);
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            hostPageViewModel.RateGuest_Click(sender, e);
        }
    }
}
