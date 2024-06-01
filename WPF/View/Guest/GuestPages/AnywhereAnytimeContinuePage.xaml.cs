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
    /// Interaction logic for AnywhereAnytimeContinuePage.xaml
    /// </summary>
    public partial class AnywhereAnytimeContinuePage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public int DayNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }
        public AnywhereAnytimeContinueViewModel ViewModel { get; set; }
        public AnywhereAnytimeContinuePage(User user, Frame frame, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            DayNumber = dayNumber;
            StartDate = startDate;
            EndDate = endDate;
            GuestNumber = guestNumber;
            ViewModel = new AnywhereAnytimeContinueViewModel(User, Frame, DayNumber, GuestNumber, StartDate, EndDate);
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
