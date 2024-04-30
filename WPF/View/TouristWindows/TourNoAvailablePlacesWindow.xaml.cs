using BookingApp.WPF.ViewModel.GuideTouristViewModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourNoAvailablePlacesWindow.xaml
    /// </summary>
    public partial class TourNoAvailablePlacesWindow : Window
    {
        public TourNoAvailablePlacesViewModel Tour {  get; set; }
        public TourNoAvailablePlacesWindow(TourViewModel selectedTour, int userId)
        {
            InitializeComponent();
            Tour = new TourNoAvailablePlacesViewModel();
            DataContext = Tour;
            Tour.SelectedTour = selectedTour;
            Tour.UserId = userId;

            Tour.RefreshToursByCity();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            Tour.BookNumberOfParticipants();
            Close();
        }
    }
}
