using BookingApp.Repository;
using BookingApp.View.HostWindows;
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

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        public AccommodationRepository accommodationRepository { get; set; }
        public HostWindow()
        {
            InitializeComponent();
            accommodationRepository = new AccommodationRepository();
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationWindow registerWindow = new RegisterAccommodationWindow(accommodationRepository);
            registerWindow.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
