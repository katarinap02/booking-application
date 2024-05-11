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
using BookingApp.WPF.ViewModel.GuideTouristViewModel;

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

            Tour.InitializeNumberOfParticipantsWindow();

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
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tour.ConfirmNumberOfParticipants();
            Close();
        }
    }
}
