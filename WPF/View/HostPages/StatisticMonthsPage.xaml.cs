﻿using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
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

namespace BookingApp.WPF.View.HostPages
{
    /// <summary>
    /// Interaction logic for StatisticMonthsPage.xaml
    /// </summary>
    public partial class StatisticMonthsPage : Page
    {
        public StatisticMonthsPage(User user, AccommodationViewModel acc, string year, NavigationService navigationService)
        {
            InitializeComponent();
            DataContext = new StatisticMonthsPageViewModel(user, acc, year, navigationService);
        }
    }
}
