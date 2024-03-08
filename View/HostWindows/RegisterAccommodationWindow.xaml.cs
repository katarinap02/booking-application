using BookingApp.DTO;
using BookingApp.Repository;
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

        public AccommodationDTO accommodationDTO { get; set; }
        private AccommodationRepository accommodationRepository;
        public RegisterAccommodationWindow(AccommodationRepository ar)
        {
            InitializeComponent();
            accommodationRepository = ar;
            DataContext = this;
            accommodationDTO = new AccommodationDTO();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MessageBox.Show("Accommodation not added!");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            accommodationRepository.Add(accommodationDTO.ToAccommodation());
            MessageBox.Show("Accommodation added.");
            Close();
        }
    }
}
