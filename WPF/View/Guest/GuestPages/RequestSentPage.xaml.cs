using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.View.GuestPages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for RequestSentPage.xaml
    /// </summary>
    public partial class RequestSentPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public RequestSentViewModel ViewModel { get; set; }
        public RequestSentPage(User user, Frame frame, DelayRequestViewModel requestViewModel)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            ViewModel = new RequestSentViewModel(requestViewModel);
            DataContext = ViewModel;
            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProfileInfo(User, Frame);

        }

        private void AllRequests_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new RequestsPage(User, Frame);
        }
    }
}
