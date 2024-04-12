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
    /// Interaction logic for DelayPage.xaml
    /// </summary>
    public partial class DelayPage : Page
    {
        public DelayPageViewModel DelayPageViewModel { get; set; }
        public DelayPage(User user)
        {
            InitializeComponent();
            DelayPageViewModel = new DelayPageViewModel(user);
            DataContext = DelayPageViewModel;
            
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            DelayPageViewModel.Approve(sender, e);
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            DelayPageViewModel?.Reject(sender, e);
        }
    }
}
