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
    /// Interaction logic for RequestedTourDetailsWindow.xaml
    /// </summary>
    public partial class RequestedTourDetailsWindow : Window
    {
        TourRequestViewModel TourRequest { get; set; }
        public RequestedTourDetailsWindow(TourRequestViewModel selectedTourRequest)
        {
            InitializeComponent();
            TourRequest = selectedTourRequest;
            DataContext = TourRequest;
        }
    }
}
