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
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace BookingApp.WPF.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for AddTourRequestWindow.xaml
    /// </summary>
    public partial class AddTourRequestWindow : Window
    {
        public TourRequestWindowViewModel TourRequest { get; set; }
        public AddTourRequestWindow(TourRequestWindowViewModel tourRequestWindowViewModel)
        {
            InitializeComponent();
            TourRequest = tourRequestWindowViewModel;
            DataContext = TourRequest;

            TourRequest.InitializeTourRequestWindow();
            Messenger.Default.Register<CloseWindowMessage>(this, CloseWindow);
            Messenger.Default.Register<NotificationMessage>(this, message =>
            {
                if (IsActive) { 
                    MessageBox.Show(message.Notification, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
            foreach (var date in TourRequest.SelectedDates)
            {
                StartDatePicker.BlackoutDates.Add(new CalendarDateRange(date.AddDays(-1))); //ovo treba jer mora biti razlika bar 1 dan
                EndDatePicker.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }

        private void CloseWindow(CloseWindowMessage message)
        {
            if (this.IsActive == true)
            {
                Close();
            }
        }

        private void Integer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }
        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
    }
}
