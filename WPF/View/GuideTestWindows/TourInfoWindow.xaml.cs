using BookingApp.Domain.Model.Features;
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

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for TourInfoWindow.xaml
    /// </summary>
    public partial class TourInfoWindow : Window
    {
        public TourViewModel tourViewModel { get; set; }
        public TourInfoWindow(Tour tour)
        {
            tourViewModel = new TourViewModel(tour);
            DataContext = this;
            InitializeComponent();
        }
    }
}
