﻿using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for VouchersPage.xaml
    /// </summary>
    public partial class VouchersPage : Page
    {
        public VoucherViewModel Voucher { get; set; }

        public VouchersPage(int userId)
        {
            InitializeComponent();
            Voucher = new VoucherViewModel();
            DataContext = Voucher;

            Voucher.UserId = userId;

            Voucher.RefreshVouchers();
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            Voucher.NotificationButton();
        }
    }
}