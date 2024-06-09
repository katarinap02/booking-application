using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.View.TouristWindows;
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for TourDetailsWindow.xaml
    /// </summary>
    public partial class TourDetailsWindow : Window
    {
        public TourDetailsViewModel Tour { get; set; }
        public TourDetailsWindow(TourViewModel selectedTour, bool isMyTour, string username)
        {
            InitializeComponent();
            Tour = new TourDetailsViewModel(selectedTour, isMyTour, username);
            DataContext = Tour;

            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (IsActive)
                {
                    InformationMessageBoxWindow mb = new InformationMessageBoxWindow(message.Notification);
                    mb.ShowDialog();
                }
            });
        }
        private void CloseWindow(CloseWindowMessage messsage)
        {
            Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Messenger.Default.Unregister(this);
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ExportPdf_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExportPdf_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Tour.ExportToPDFCommand.Execute(null);
        }
    }
}
