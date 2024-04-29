using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels;
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
    /// Interaction logic for ScheduleReservationWindow.xaml
    /// </summary>
    public partial class ScheduleReservationWindow : Window
    {
        public ScheduleRenovationWindowViewModel ScheduleRenovationWindowViewModel { get; set; }
        public ScheduleReservationWindow(AccommodationViewModel selectedAccommodation, DateTime startDateRange, DateTime endDateRange)
        {
            InitializeComponent();
            ScheduleRenovationWindowViewModel = new ScheduleRenovationWindowViewModel(selectedAccommodation, startDateRange, endDateRange);
            DataContext = ScheduleRenovationWindowViewModel;
        }
    }
}
