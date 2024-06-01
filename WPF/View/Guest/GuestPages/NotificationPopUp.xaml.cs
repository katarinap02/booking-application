using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
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
using System.Windows.Media.Animation;
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
        
       
        public Frame Frame { get; set; }
        public NotificationViewModel ViewModel { get; set; }
        public RequestDetailsViewModel Request { get; set; }
        public GuestWindow GuestWindow { get; set; }
        public NotificationPopUp(User user, Frame frame, GuestWindow guestWindow)
        {
            InitializeComponent();
            User = user;
            GuestWindow = guestWindow;
            ViewModel = new NotificationViewModel(User,  GuestWindow);
            DataContext = ViewModel;
            ViewModel.Update();
            Frame = frame;
            // MessageBox.Show(ViewModel.Notifications[0]);
           

        }

      
        private void Details_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string notification = button.DataContext as string;
            DelayRequestViewModel request = ViewModel.GetRequest(notification);
            
            Frame.Content = new NotificationDetailsPage(request, User, Frame);

            
        }


    }
}
