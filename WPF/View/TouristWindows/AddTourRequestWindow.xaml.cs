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
                    MessageBoxWindow mb = new MessageBoxWindow(message.Notification);
                    mb.ShowDialog();
                    //MessageBox.Show(message.Notification, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
        private void AddParticipant_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TourRequest.AddParticipantCommand.CanExecute(null);
        }

        private void AddParticipant_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.AddParticipantCommand.Execute(null);
        }

        private void ParticipantName_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ParticipantName_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NameTextBox.Focus();
        }

        private void CountryFocus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CountryFocus_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CountryComboBox.Focus();
        }
        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        private void Confirm_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TourRequest.AddToToursCommand.CanExecute(null);
        }

        private void Confirm_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TourRequest.AddToToursCommand.Execute(null);
        }
    }
}
