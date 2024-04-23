using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationPage.xaml
    /// </summary>
    public partial class RegisterAccommodationPage : Page
    {
        public RegisterAccommodationPageViewModel RegisterAccommodationPageViewModel { get; set; }
        public RegisterAccommodationPage(User us)
        {
            InitializeComponent();
            RegisterAccommodationPageViewModel = new RegisterAccommodationPageViewModel(us);
            DataContext = RegisterAccommodationPageViewModel;
            
            
        }
    }
}


