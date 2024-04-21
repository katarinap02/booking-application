﻿using BookingApp.WPF.ViewModel;
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
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for VoucherWindow.xaml
    /// </summary>
    public partial class VoucherWindow : Window
    {
        public VoucherViewModel Voucher { get; set; }
        public VoucherWindow(int userId)
        {
            InitializeComponent();
            Voucher = new VoucherViewModel();
            DataContext = Voucher;

            Voucher.UserId = userId;
            if (!Voucher.RefreshVoucherDataGrid())
                Close();
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            Voucher.Use();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}