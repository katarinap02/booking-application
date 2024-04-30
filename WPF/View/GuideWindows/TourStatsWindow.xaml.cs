using BookingApp.Application.Services.FeatureServices;
using BookingApp.Repository;
using BookingApp.Application.Services;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideViewModel;

namespace BookingApp.View.GuideWindows
{
    public partial class TourStatsWindow : Window
    {
       
        public TourStatsWindowViewModel TourStatsWindowViewModel { get; set; }

        public TourStatsWindow(int tourId)
        {
            InitializeComponent();
            TourStatsWindowViewModel = new TourStatsWindowViewModel(tourId);           
            DataContext = TourStatsWindowViewModel;
        }

        

        
    }
}
