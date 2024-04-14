using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BookingApp.Observer;
using BookingApp.Model;
using System.Security.Cryptography;
using BookingApp.Services;
using BookingApp.View.ViewModel;
using BookingApp.ViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for AccommodationsPage.xaml
    /// </summary>
    public partial class AccommodationsPage : Page
    {
        

        public User User { get; set; }
    
        public Frame Frame { get; set; }    

        public ShowAccommodationsViewModel ViewModel { get; set; }
      

        public AccommodationsPage(User user, Frame frame)
        {
            InitializeComponent();

           
            this.User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            this.Frame = frame;
            ViewModel = new ShowAccommodationsViewModel(User, Frame, this);
            DataContext = ViewModel;
            ViewModel.Update();
           
          
        }
       

       

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
           
           ViewModel.SearchButton_Click(sender, e); 
            


        }


       

        private void ReserveButton_Click(object sender, RoutedEventArgs e) {

           
            
           ViewModel.ReserveButton_Click(sender, e);
            


        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {



        }

    }
}
