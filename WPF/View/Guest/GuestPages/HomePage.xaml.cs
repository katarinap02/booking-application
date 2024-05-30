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
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.View.Guest.GuestPages;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
   
        
        public User User { get; set; }

        public Frame Frame { get; set; }    
        
        public HomePageViewModel ViewModel { get; set; }

        //KOMANDE
       
        public HomePage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;
            ViewModel = new HomePageViewModel(User, Frame);
           
            DataContext = ViewModel;
            
        }

       

      
    }
}
