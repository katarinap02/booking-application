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
using BookingApp.WPF.ViewModel.Commands;
using System.Windows.Media.Animation;

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




            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ContinueCommand.RaiseCanExecuteChanged();
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
