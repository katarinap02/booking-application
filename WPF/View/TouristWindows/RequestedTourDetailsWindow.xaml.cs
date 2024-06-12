using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
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

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for RequestedTourDetailsWindow.xaml
    /// </summary>
    public partial class RequestedTourDetailsWindow : Window
    {
        TourRequestViewModel TourRequest { get; set; }
        public RequestedTourDetailsWindow(TourRequestViewModel selectedTourRequest)
        {
            InitializeComponent();
            TourRequest = selectedTourRequest;
            DataContext = TourRequest;

            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
        }
        private void CloseWindow(CloseWindowMessage messsage)
        {
            if (IsActive)
            {
                Close();
            }
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
            CloseWindow(null);
        }
    }
}
