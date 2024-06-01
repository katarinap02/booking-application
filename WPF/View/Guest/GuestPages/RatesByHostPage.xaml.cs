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
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.View.Guest.GuestPages;
using System.Windows.Media.Animation;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RatesByHostPage.xaml
    /// </summary>
    /// 


    public partial class RatesByHostPage : Page
    {
        public RatesByHostViewModel ViewModel { get; set; }
        public User User { get; set; }
       
      
        public Frame Frame { get; set; }

        public GuestRateViewModel SelectedRate { get; set; }

        public RatesByHostPage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;
            ViewModel = new RatesByHostViewModel(User, Frame);
            DataContext = ViewModel;
            ViewModel.Update();
            Loaded += Page_Loaded;

        }
        
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            RateDetailsViewModel tmpSelectedRate = button.DataContext as RateDetailsViewModel;
            SelectedRate = tmpSelectedRate.SelectedRate;
            Frame.Content = new RateDetailsPage(SelectedRate, User, Frame);

           


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }

    }
}
