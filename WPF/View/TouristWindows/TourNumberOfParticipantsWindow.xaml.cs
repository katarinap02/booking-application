using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourNumberOfParticipantsWindow.xaml
    /// </summary>
    public partial class TourNumberOfParticipantsWindow : Window
    {
        public TourNumberOfParticipantsViewModel Tour {  get; set; }

        public TourNumberOfParticipantsWindow(TourViewModel selectedTour, int userId)
        {
            InitializeComponent();
            Tour = new TourNumberOfParticipantsViewModel();
            DataContext = Tour;
            Tour.SelectedTour = selectedTour;
            Tour.AvailablePlaces = selectedTour.AvailablePlaces;
            Tour.UserId = userId;

            Tour.RefreshToursByCity();


            InitializeWindow();

        }

        private void InitializeWindow()
        {
             (this.Width, this.Height) = Tour.InitializeNumberOfParticipantsWindow();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.ConfirmNumberOfParticipants();
            Close();
        }


        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.BookNumberOfParticipants();
            Close();
        }
    }
}
