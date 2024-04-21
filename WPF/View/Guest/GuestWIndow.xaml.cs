using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Application.Services;
using BookingApp.View.GuestPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
      
        public User User { get; set; }
        
        public GuestWindow(User user)
        {
            InitializeComponent();
        
            this.User = user;
            Main.Content = new HomePage(User, Main);
            Main.DataContext = this;
          
            
        }

       

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage(User, Main);
        }

        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage(User, Main);
        }

        private void AccommodationsClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccommodationsPage(User, Main);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            NotificationFrame.Visibility = Visibility.Visible;
            NotificationFrame.Content = new NotificationPopUp(User);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
           NotificationFrame.Visibility = Visibility.Collapsed;
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

        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {

          

        }

        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {
            


        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}