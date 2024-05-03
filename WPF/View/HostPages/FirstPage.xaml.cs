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
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {   

        public FirstPageViewModel firstPageViewModel {  get; set; }
        public FirstPage(User user)
        {
            InitializeComponent();
            firstPageViewModel = new FirstPageViewModel(user);
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

        
    }
}
