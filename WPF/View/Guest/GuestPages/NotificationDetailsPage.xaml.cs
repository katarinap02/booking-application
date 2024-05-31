using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
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
using BookingApp.WPF.ViewModel.Commands;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for NotificationDetailsPage.xaml
    /// </summary>
    public partial class NotificationDetailsPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public NotificationDetailsViewModel ViewModel { get; set; }

        public DelayRequestViewModel SelectedRequest { get; set; }
        
        public NavigationService NavigationService { get; set; }
        public NotificationDetailsPage(DelayRequestViewModel selectedRequest, User user, Frame frame)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            SelectedRequest = selectedRequest;
            ViewModel = new NotificationDetailsViewModel(SelectedRequest, this);

            DataContext = ViewModel;
            ViewModel.BackCommand = new GuestICommand(OnBack);
            NavigationService = Frame.NavigationService;
            
        }

        private void OnBack()
        {
            NavigationService.GoBack();
        }

        private void ContentChanged(object sender, EventArgs e)
        {
            
        }

    }
}
