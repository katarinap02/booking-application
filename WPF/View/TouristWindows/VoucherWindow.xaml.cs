using BookingApp.WPF.View.TouristWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
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
using GalaSoft.MvvmLight.Messaging;

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
                CloseWindow(null);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (IsActive)
                {
                    MessageBox.Show(message.Notification, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }
        private void CloseWindow(CloseWindowMessage message)
        {
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
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
