using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestWindows;
using BookingApp.View.HostPages.RatePages;
using BookingApp.View.HostPages;
using BookingApp.View.HostWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.ViewModel.HostGuestViewModel.HostViewModels
{
    public class HostPageViewModel : INotifyPropertyChanged, IObserver
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<AccommodationReservationViewModel> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public AccommodationReservationViewModel SelectedAccommodation { get; set; }

        public Frame HostFrame { get; set; }

        public Menu LeftDock {  get; set; }
        public User User { get; set; }

        public StackPanel RatingPanel {  get; set; }

        public HostPageViewModel(User user, Frame frame, Menu dock, StackPanel panel) {
            Accommodations = new ObservableCollection<AccommodationReservationViewModel>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            HostFrame = frame;
            LeftDock = dock;
            User = user;
            RatingPanel = panel;

            Update();



        }

        public void Update()
        {
          //  Accommodations.Clear();
         //   foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
           // {
           //     Accommodations.Add(new AccommodationReservationViewModel(accommodation));

           // }
            FirstPage firstPage = new FirstPage(User);
            HostFrame.Navigate(firstPage);
        }

       // private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
       // {
       //     RegisterAccommodationWindow registerWindow = new RegisterAccommodationWindow(accommodationRepository, User);

        //    registerWindow.ShowDialog();
       // }

        public void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstPage firstPage = new FirstPage(User);
            HostFrame.Navigate(firstPage);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationPage page = new RegisterAccommodationPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }
        public void GuestRatings_Click(object sender, RoutedEventArgs e)
        {
            RateDisplayPage page = new RateDisplayPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        public void Delay_Click(object sender, RoutedEventArgs e)
        {
            DelayPage page = new DelayPage();
            HostFrame.Navigate(page);
            LeftDock.Visibility = Visibility.Collapsed;
            RatingPanel.Visibility = Visibility.Collapsed;
        }

        

      //  private void Rate_Click(object sender, RoutedEventArgs e)
     //   {
        //    if (SelectedAccommodation != null)
        //    {
         //       RateGuestWindow rateGuestWindow = new RateGuestWindow(SelectedAccommodation);
         //       rateGuestWindow.ShowDialog();
         //   }
         //   else
        //    {
          //      MessageBox.Show("Select guest to rate.");
         //   }

        //    Update();
       // }



        public void More_Click(object sender, RoutedEventArgs e)
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

        public void Rating_Click(object sender, RoutedEventArgs e)
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
