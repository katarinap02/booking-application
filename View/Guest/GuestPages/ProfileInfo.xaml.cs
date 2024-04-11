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
using BookingApp.View.ViewModel.HostGuestViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ProfileInfo.xaml
    /// </summary>
    public partial class ProfileInfo : Page
    {
      
        public User User { get; set; }
       
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public Frame Frame {  get; set; }   

        public ProfileInfoViewModel ViewModel { get; set; }
        public ProfileInfo(AccommodationReservationService accommodationReservationService, AccommodationService accommodationService, User user, Frame frame)
        {
            InitializeComponent();
          
            this.User = user;
            this.Frame = frame;
            ViewModel = new ProfileInfoViewModel(User, Frame);
            DataContext = ViewModel;
            
            ViewModel.Update();
        }

       

        public void Cancel_Click(object sender, RoutedEventArgs e) { 

            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new CancelReservationPage(SelectedReservation, User, Frame);

        
        
        
        }

        public void Delay_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SelectedReservation = button.DataContext as AccommodationReservationViewModel;
            Frame.Content = new DelayRequestPage(SelectedReservation, User, Frame);

        }
    }
}
