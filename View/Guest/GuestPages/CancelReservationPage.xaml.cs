using BookingApp.Model;
using BookingApp.Services;
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
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for CancelReservationPage.xaml
    /// </summary>
    /// 



    public partial class CancelReservationPage : Page
    {
       

        public AccommodationReservationViewModel SelectedReservation { get; set; }

        public CancellationPageViewModel ViewModel { get; set; }
        public User User { get; set; }  
        public Frame Frame { get; set; }
        public CancelReservationPage(AccommodationReservationViewModel selectedReservation, User user, Frame frame)
        {
            InitializeComponent();
            
            this.SelectedReservation = selectedReservation;
            this.User = user;
            this.Frame = frame;
            ViewModel = new CancellationPageViewModel(SelectedReservation);
            DataContext = SelectedReservation;

        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CancelReservation_Click(sender, e);

        }
    }
}
