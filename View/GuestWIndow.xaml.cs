using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.View.GuestPages;
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
            Main.Content = new HomePage(AccommodationRepository, User);
        }

       

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage(AccommodationRepository, User);
        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {

        }

        private void AccommodationsClick(object sender, RoutedEventArgs e)
        {

        }

        private void ForumsClick(object sender, RoutedEventArgs e)
        {

        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {

        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {

        }
    }
}