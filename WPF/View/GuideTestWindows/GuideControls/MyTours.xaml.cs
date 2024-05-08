using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows.GuideControls
{
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : UserControl
    {
        public MyToursViewModel ViewModel { get; set; }
        public MyTours()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
