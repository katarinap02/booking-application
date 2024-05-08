using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections;
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
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for ReservationInfoPage.xaml
    /// </summary>
    public partial class ReservationInfoPage : Page
    {
        public AccommodationViewModel SelectedAccommodation { get; set; }
       

        public ReservationFinishViewModel ViewModel { get; set; }
        public User User { get; set; }
      
        public Frame Frame { get; set; }

       
        public ReservationInfoPage(AccommodationViewModel selectedAccommodation, User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            SelectedAccommodation = selectedAccommodation;
            ViewModel = new ReservationFinishViewModel(SelectedAccommodation, User, Frame, this);
            
            txtStartDate.DisplayDateStart = DateTime.Now;
            txtEndDate.DisplayDateStart = DateTime.Now;
            Hint.Visibility = Visibility.Hidden;

            DataContext = this;
          
           

           
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            

           
           ViewModel.Continue_Click(sender, e);


        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Hint.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Hint.Visibility = Visibility.Hidden;

        }
    }
}
