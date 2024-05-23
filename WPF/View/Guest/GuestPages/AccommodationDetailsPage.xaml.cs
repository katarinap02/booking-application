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
    /// Interaction logic for AccommodationDetailsPage.xaml
    /// </summary>
    public partial class AccommodationDetailsPage : Page
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationDetailsViewModel ViewModel { get; set; }
        public Frame Frame { get; set; }
        public User User { get; set; }
        public AccommodationDetailsPage(Frame frame, User user, AccommodationViewModel selectedAccommodation)
        {
            InitializeComponent();
            Frame = frame;
            SelectedAccommodation = selectedAccommodation;
            User = user;
            ViewModel = new AccommodationDetailsViewModel(Frame, User, SelectedAccommodation);
            DataContext = ViewModel;
        }
    }
}
