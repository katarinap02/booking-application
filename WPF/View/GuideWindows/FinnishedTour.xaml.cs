using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using BookingApp.Application.Services;
using BookingApp.Domain.Model.Features;
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System.ComponentModel;
using BookingApp.WPF.ViewModel.GuideViewModel;

namespace BookingApp.View.GuideWindows
{
    public partial class FinnishedTour : Window
    {
        FinnishedTourWindowViewModel viewModel { get; set; }

        public FinnishedTour(User guide)
        {
            InitializeComponent();
            viewModel = new FinnishedTourWindowViewModel(guide);
            DataContext = viewModel;
        }
        
    }
}