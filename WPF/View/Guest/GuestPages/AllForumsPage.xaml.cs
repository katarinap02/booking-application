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
    /// Interaction logic for AllForumsPage.xaml
    /// </summary>
    public partial class AllForumsPage : Page
    {

        public User User { get; set; }

        public Frame Frame { get; set; }

        public AllForumsViewModel ViewModel { get; set; }

        public AllForumsPage(User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            ViewModel = new AllForumsViewModel(User, Frame, this);
            DataContext = ViewModel;

            foreach (ComboBoxItem item in forumTypeBox.Items)
            {
                if (item.Content.ToString() == "All forums" || item.Content.ToString() == "Svi forumi") 
                {
                    forumTypeBox.SelectedItem = item;
                    break;
                }
            }
        }

        public void ForumBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           ViewModel.ForumBox_SelectionChanged(sender, e);
        }
    }
}
