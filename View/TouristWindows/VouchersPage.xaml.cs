﻿using BookingApp.Model;
using BookingApp.Repository;
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
        public ObservableCollection<Voucher> Vouchers { get; set; }

        private readonly VoucherRepository _repository;
        public VouchersPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new VoucherRepository();
            Vouchers = new ObservableCollection<Voucher>(_repository.FindVouchersByUser(userId));
        }
    }
}