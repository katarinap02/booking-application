using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.Commands;
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

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public GuestICommand SaveCommand { get; set; }

        public SettingsViewModel ViewModel { get; set; }
        public SettingsPage(User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            DataContext = new SettingsViewModel(user);
            Loaded += Page_Loaded;
            
           // SaveCommand = new GuestICommand(OnSave);



        }

       

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }
    }
}
