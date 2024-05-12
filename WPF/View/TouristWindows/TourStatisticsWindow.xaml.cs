using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.GuideViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for TourStatisticsWindow.xaml
    /// </summary>
    public partial class TourStatisticsWindow : Window
    {
        public TourStatisticsViewModel TourStatisticsViewModel { get; set; }
        public TourStatisticsWindow(int userId)
        {
            InitializeComponent();
            TourStatisticsViewModel = new TourStatisticsViewModel(userId);
            DataContext = TourStatisticsViewModel;
            TourStatisticsViewModel.InitializeTourStatistics();

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

    public class YearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int year = (int)value;
            return year == 0 ? "All years" : year.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string yearString = (string)value;
            return yearString == "All years" ? 0 : int.Parse(yearString);
        }
    }
}
