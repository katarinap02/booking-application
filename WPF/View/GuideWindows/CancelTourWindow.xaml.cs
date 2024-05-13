using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System.Collections.Generic;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.WPF.ViewModel.GuideViewModel;

namespace BookingApp.View.GuideWindows
{
    public partial class CancelTourWindow : Window
    {
        

        public CancelTourWindowViewModel CancellationViewModel { get; set; }

        public CancelTourWindow(User guide)
        {
            InitializeComponent();
            CancellationViewModel = new CancelTourWindowViewModel(guide);
            DataContext = CancellationViewModel;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

