using BookingApp.Observer;
using BookingApp.Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.HostPages
{
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
