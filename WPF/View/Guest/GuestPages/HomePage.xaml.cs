using BookingApp.Repository;
using BookingApp.Application.Services;
using BookingApp.View.GuideWindows;
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
using BookingApp.Domain.Model.Features;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
   
        public User User { get; set; }

        public Frame Frame { get; set; }    
        
        public HomePage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            DataContext = this;
            this.Frame = frame;
            
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new AccommodationsPage(User, Frame);
            
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
