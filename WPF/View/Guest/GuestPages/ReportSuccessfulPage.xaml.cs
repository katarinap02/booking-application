using BookingApp.Domain.Model.Features;
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
    /// Interaction logic for ReportSuccessfulPage.xaml
    /// </summary>
    public partial class ReportSuccessfulPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public string Path { get; set; }
        public ReportSuccessfulViewModel ViewModel { get; set; }
        public ReportSuccessfulPage(User user, Frame frame, string path)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            Path = path;
            ViewModel = new ReportSuccessfulViewModel(User, Frame, Path);
            DataContext = ViewModel;
            Loaded += Page_Loaded;


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }
    }
}
