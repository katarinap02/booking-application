using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestWindows;
using BookingApp.View.HostPages;
using BookingApp.View.HostPages.RatePages;
using BookingApp.View.HostWindows;
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
using System.Windows.Shapes;
using BookingApp.View.ViewModel;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window, IObserver
    {
        public ObservableCollection<AccommodationReservationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }
        public User User { get; set; }

        public HostWindow(User user)
        {

            InitializeComponent();
            
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            DataContext = this;
            User = user;
            
            Update();

            



        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
            {
                Accommodations.Add(new AccommodationReservationViewModel(accommodation));
                
            }
            FirstPage firstPage = new FirstPage(User);
            HostFrame.Navigate(firstPage);
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationWindow registerWindow = new RegisterAccommodationWindow(accommodationRepository, User);

            registerWindow.ShowDialog();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstPage firstPage = new FirstPage(User);
            HostFrame.Navigate(firstPage);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationPage page = new RegisterAccommodationPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }
        private void GuestRatings_Click(object sender, RoutedEventArgs e)
        {
            RateDisplayPage page = new RateDisplayPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        private void Delay_Click(object sender, RoutedEventArgs e)
        {
            DelayPage page = new DelayPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                RateGuestWindow rateGuestWindow = new RateGuestWindow(SelectedAccommodation);
                rateGuestWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select guest to rate.");
            }

            Update();
        }

        

        private void More_Click(object sender, RoutedEventArgs e)
        {
            if (LeftDock.Visibility == Visibility.Visible)
            {
                LeftDock.Visibility = Visibility.Collapsed;
            }
            else
            {
                LeftDock.Visibility = Visibility.Visible;
            }
        }

        private void Rating_Click(object sender, RoutedEventArgs e)
        {
            if (RatingPanel.Visibility == Visibility.Visible)
            {
                RatingPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                RatingPanel.Visibility = Visibility.Visible;
            }
        }

        
    }
}
