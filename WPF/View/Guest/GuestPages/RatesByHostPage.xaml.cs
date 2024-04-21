using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RatesByHostPage.xaml
    /// </summary>
    /// 


    public partial class RatesByHostPage : Page
    {
        public RatesByHostViewModel ViewModel { get; set; }
        public User User { get; set; }
       
      
        public Frame Frame { get; set; }

        

        public RatesByHostPage(User user, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.Frame = frame;
            ViewModel = new RatesByHostViewModel(User, Frame);
            DataContext = ViewModel;
            ViewModel.Update();

        }
        
        
    }
}
