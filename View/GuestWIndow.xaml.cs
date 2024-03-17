using BookingApp.Model;
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

namespace BookingApp.View
{
   
    public partial class GuestWindow : Window
    {
        public AccommodationRepository AccommodationRepository;

        public User User {  get; set; }
        public GuestWindow(User user)
        {
            InitializeComponent();
            AccommodationRepository = new AccommodationRepository();
            this.User = user;
        }

        public void ShowAccommodations_Click(object sender, RoutedEventArgs e)
        {
            ShowAndSearchAccommodations showAndSearchAccommodations = new ShowAndSearchAccommodations(AccommodationRepository, User);
            showAndSearchAccommodations.ShowDialog();

        }
    }
}