using BookingApp.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
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

namespace BookingApp.WPF.View.GuideWindows
{
    /// <summary>
    /// Interaction logic for RequestStatsWindow.xaml
    /// </summary>
    public partial class RequestStatsWindow : Window
    {
        public RequestsStatsWindowViewModel ViewModel { get; set; }
        public RequestStatsWindow(int guide_id)
        {
            ViewModel = new RequestsStatsWindowViewModel(guide_id);
            DataContext = ViewModel;
            InitializeComponent();
        }

        
    }
}
