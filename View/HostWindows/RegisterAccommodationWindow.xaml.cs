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

namespace BookingApp.View.HostWindows
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationWindow.xaml
    /// </summary>
    public partial class RegisterAccommodationWindow : Window
    {
        public RegisterAccommodationWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Accommodation not added!");
        }
    }
}
