using BookingApp.Model;
using BookingApp.View.ViewModel;
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
    /// Interaction logic for RequestDetailsPage.xaml
    /// </summary>
    public partial class RequestDetailsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public DelayRequestViewModel ViewModel { get; set; }

        public DelayRequestViewModel SelectedRequest { get; set; }
        public RequestDetailsPage(DelayRequestViewModel selectedRequest, User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            SelectedRequest = selectedRequest;
            ViewModel = new DelayRequestViewModel(User, Frame, SelectedRequest);
            
            DataContext = ViewModel;
        }

        
    }
}
