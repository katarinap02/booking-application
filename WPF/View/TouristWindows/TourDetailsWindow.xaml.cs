using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using BookingApp.WPF.ViewModel;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourDetailsWindow.xaml
    /// </summary>
    public partial class TourDetailsWindow : Window
    {
        public TourViewModel Tour { get; set; }
        public TourDetailsWindow(TourViewModel selectedTour, bool isMyTour)
        {
            InitializeComponent();
            Tour = new TourViewModel();
            Tour.SelectedTour = selectedTour;
            DataContext = Tour;
            Tour.PdfPanel = Visibility.Collapsed;
            Tour.TourDetailsWindowInitialization(isMyTour);
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
