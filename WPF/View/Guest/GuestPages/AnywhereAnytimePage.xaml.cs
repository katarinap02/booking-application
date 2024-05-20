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
    /// Interaction logic for AnywhereAnytimePage.xaml
    /// </summary>
    public partial class AnywhereAnytimePage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public AnywhereAnytimeViewModel ViewModel { get; set; }
        public AnywhereAnytimePage(User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            ViewModel = new AnywhereAnytimeViewModel(User, Frame, this);
            DataContext = ViewModel;
        }
    }
}
