using BookingApp.Model;
using BookingApp.ViewModel;
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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for NotificationPopUp.xaml
    /// </summary>
    public partial class NotificationPopUp : Page
    {
        public User User { get; set; }
        public NotificationViewModel ViewModel { get; set; }
        public NotificationPopUp(User user)
        {
            InitializeComponent();
            User = user;
            ViewModel = new NotificationViewModel(User);
            DataContext = ViewModel;
            ViewModel.Update();
           // MessageBox.Show(ViewModel.Notifications[0]);
        }


    }
}
