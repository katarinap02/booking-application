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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for AnywhereAnytimeCalendarPage.xaml
    /// </summary>
    public partial class AnywhereAnytimeCalendarPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public int DayNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AnywhereAnytimeCalendarViewModel ViewModel { get; set; }
        public AnywhereAnytimeCalendarPage(User user, Frame frame, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate, AccommodationViewModel selectedAccommodation)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            DayNumber = dayNumber;
            GuestNumber = guestNumber;
            StartDate = startDate;
            EndDate = endDate;
            SelectedAccommodation = selectedAccommodation;
            ViewModel = new AnywhereAnytimeCalendarViewModel(User, Frame, DayNumber, GuestNumber, StartDate, EndDate, this, SelectedAccommodation);
            DataContext = ViewModel;
            Loaded += Page_Loaded;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

            ViewModel.Calendar_SelectedDatesChanged(sender, e);
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
