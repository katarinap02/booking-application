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
using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.TouristViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for CreatedTourDetailsWindow.xaml
    /// </summary>
    public partial class CreatedTourDetailsWindow : Window
    {
        public CreatedTourDetailsWindowViewModel CreatedTourDetailsWindowViewModel { get; set; }
        public CreatedTourDetailsWindow(TouristNotificationViewModel selectedNotification)
        {
            InitializeComponent();
            CreatedTourDetailsWindowViewModel = new CreatedTourDetailsWindowViewModel(selectedNotification);
            DataContext = CreatedTourDetailsWindowViewModel;

            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
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
    }
}
