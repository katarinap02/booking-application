using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Application.Services;
using BookingApp.WPF.ViewModel;
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
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for GuestRatePage.xaml
    /// </summary>
    public partial class GuestRatePage : Page
    {
        public GuestRatePageViewModel GuestRatePageViewModel { get; set; }
        public GuestRatePage(User user, NavigationService NavigationService)
        {
            InitializeComponent();
            GuestRatePageViewModel = new GuestRatePageViewModel(user, NavigationService);
            DataContext = GuestRatePageViewModel;
            
        }
    }
}
