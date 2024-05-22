using BookingApp.Domain.Model.Features;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for CloseThreadSuccessfulPage.xaml
    /// </summary>
    public partial class CloseThreadSuccessfulPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public ForumViewModel ClosedForum { get; set; }
        public CreateForumSuccessfulViewModel ViewModel { get; set; }
        public CloseThreadSuccessfulPage(User user, Frame frame, ForumViewModel closedForum)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            ClosedForum = closedForum;
            ViewModel = new CreateForumSuccessfulViewModel(User, Frame, ClosedForum);
            DataContext = ViewModel;
        }
    }
}
