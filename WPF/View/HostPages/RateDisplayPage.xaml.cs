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
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
using BookingApp.Domain.Model.Features;

namespace BookingApp.WPF.View.HostPages.RatePages
{
    /// <summary>
    /// Interaction logic for RateDisplayPage.xaml
    /// </summary>
    public partial class RateDisplayPage : Page, IObserver
    {
        
        public RateDisplayPage(User user)
        {
            InitializeComponent();
            DataContext = new RateDisplayPageViewModel(user);
            
        }

        public void Update()
        {
           
        }

        private void RateGuest_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
