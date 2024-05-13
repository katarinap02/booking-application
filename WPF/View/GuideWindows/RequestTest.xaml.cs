using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace BookingApp.WPF.View.GuideWindows
{
    public partial class RequestTest: Window
    {
        public RequestMainViewModel MainViewModel { get; set; }
        public RequestTest(int ID)
        {
            MainViewModel = new RequestMainViewModel(ID);
            DataContext = MainViewModel;
            InitializeComponent();
        }

    }
}
