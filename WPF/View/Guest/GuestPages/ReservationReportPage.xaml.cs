using BookingApp.Domain.Model.Features;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for ReservationReportPage.xaml
    /// </summary>
    public partial class ReservationReportPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        

        public ReservationReportViewModel ViewModel { get; set; }
        public ReservationReportPage(User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            ViewModel = new ReservationReportViewModel(User, Frame, this);
            DataContext = ViewModel;
        }
    }
}
