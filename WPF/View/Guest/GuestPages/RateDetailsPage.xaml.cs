using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
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

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for RateDetailsPage.xaml
    /// </summary>
    public partial class RateDetailsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public GuestRateViewModel SelectedRate { get; set; }

        public RateDetailsViewModel ViewModel { get; set; }
        public RateDetailsPage(GuestRateViewModel selectedRate, User user, Frame frame)
        {
            InitializeComponent();
            SelectedRate = selectedRate;
            ViewModel = new RateDetailsViewModel(SelectedRate);
            DataContext = ViewModel;
        }
    }
}
