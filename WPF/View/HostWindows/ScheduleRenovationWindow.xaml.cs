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
        public ScheduleRenovationWindow(AccommodationViewModel selectedAccommodation, User user, RenovationViewModel renovation)
        {
            InitializeComponent();
            ScheduleRenovationWindowViewModel = new ScheduleRenovationWindowViewModel(selectedAccommodation, user, renovation);
            DataContext = ScheduleRenovationWindowViewModel;
        }
    }
}
