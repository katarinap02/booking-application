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
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for GuideQuittingWindow.xaml
    /// </summary>
    public partial class GuideQuittingWindow : Window
    {
        public GuideQuitWindowViewModel ViewModel { get; set; }
        public GuideQuittingWindow(int guide_id)
        {
            ViewModel = new GuideQuitWindowViewModel(guide_id);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimise_Buton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Buton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
