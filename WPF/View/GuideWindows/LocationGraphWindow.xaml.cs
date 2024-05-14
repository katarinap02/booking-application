using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View;
using BookingApp.WPF.ViewModel.GuideViewModel;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.View.GuideWindows
{
    /// <summary>
    /// Interaction logic for LocationGraphWindow.xaml
    /// </summary>
    public partial class LocationGraphWindow : Window
    {
        public LocationGraphWIndowViewModel viewModel { get; set; }
        public LocationGraphWindow(string city, string country, int guide_id)
        {
            viewModel = new LocationGraphWIndowViewModel(city, country, guide_id);
            DataContext = viewModel;
            InitializeComponent();
        }

        
    }
}
