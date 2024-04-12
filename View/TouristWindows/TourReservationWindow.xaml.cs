using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourReservationWindow.xaml
    /// </summary>
    public partial class TourReservationWindow : Window
    {
        public TourReservationViewModel TourReservation { get; set; }
        public int UserId;


        public TourReservationWindow(TourViewModel selectedTour, int insertedNumberOfParticipants, int userId)
        {
            InitializeComponent();
            TourReservation = new TourReservationViewModel();
            DataContext = TourReservation;
            TourReservation.SelectedTour = selectedTour;
            TourReservation.ParticipantCount = insertedNumberOfParticipants.ToString();
            TourReservation.UserId = userId;

        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if(TourReservation.Book())
                Close();
        }

        private void AddParticipantButton_Click(object sender, RoutedEventArgs e)
        {
            TourReservation.AddParticipant();
        }



        private void removeParticipant_Click(object sender, RoutedEventArgs e)
        {
            TourReservation.RemoveParticipant();
        }

    }
}
