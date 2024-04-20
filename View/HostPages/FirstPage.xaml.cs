using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.View.ViewModel;
using BookingApp.View.ViewModel.HostGuestViewModel.HostViewModels;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {   

        public FirstPageViewModel firstPageViewModel {  get; set; }
        public FirstPage(User user, Frame frame, Menu dock, StackPanel panel)
        {
            InitializeComponent();
            firstPageViewModel = new FirstPageViewModel(user, frame, dock, panel);
            DataContext = firstPageViewModel;
            
        }

        private void Displacement_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            
        }
        private void Forums_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
        private void RateGuest_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            
        }
        private void Statistic_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }

        private void Displacement_Click(object sender, RoutedEventArgs e)
        {
            firstPageViewModel.Delay_Navigate(sender, e);
        }

        private void RateGuest_Click(object sender, RoutedEventArgs e)
        {
            firstPageViewModel.RateGuest_Navigate(sender, e);
        }
    }
}
