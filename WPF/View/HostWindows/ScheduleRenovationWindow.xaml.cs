using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
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

namespace BookingApp.WPF.View.HostWindows
{
    /// <summary>
    /// Interaction logic for ScheduleRenovationWindow.xaml
    /// </summary>
    public partial class ScheduleRenovationWindow : Window
    {
        public ScheduleRenovationWindowViewModel ScheduleRenovationWindowViewModel { get; set; }

        public RenovationViewModel RenovationViewModel { get; set; }
        public ScheduleRenovationWindow(AccommodationViewModel selectedAccommodation, User user, RenovationViewModel renovation)
        {
            InitializeComponent();
            RenovationViewModel = renovation;
            ScheduleRenovationWindowViewModel = new ScheduleRenovationWindowViewModel(selectedAccommodation, user, renovation, RenovationCalendar);

            DataContext = ScheduleRenovationWindowViewModel;
        }

        private void ReservationCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar calendar = (Calendar)sender;
            int selectedDatesCount = calendar.SelectedDates.Count;
            if(selectedDatesCount == RenovationViewModel.Duration)
            {
                ScheduleButton.IsEnabled = true;
            }
            else
            {
                ScheduleButton.IsEnabled = false;
            }

            ScheduleRenovationWindowViewModel.WriteFirstAndLast(calendar);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
