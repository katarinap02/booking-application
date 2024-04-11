using BookingApp.ViewModel;
using BookingApp.Model;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for GuideRateWindow.xaml
    /// </summary>
    public partial class GuideRateWindow : Window
    {
        public TourViewModel SelectedTour { get; set; }
        public GuideRateWindow(TourViewModel selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
        }
    }
}
