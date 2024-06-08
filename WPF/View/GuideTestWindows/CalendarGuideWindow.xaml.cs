using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for CalendarGuideWindow.xaml
    /// </summary>
    public partial class CalendarGuideWindow : Window
    {
        public CalendarViewModel ViewModel { get; set; }
        public CalendarGuideWindow()
        {
            InitializeComponent();
        }

        public CalendarGuideWindow(int guide_id, TourRequestDTOViewModel tourRequest)
        {
            ViewModel = new CalendarViewModel(guide_id, tourRequest);
            DataContext = ViewModel;
            InitializeComponent();
            if(ViewModel.blackoutDates != null)
            {
                AddBlackOutDates();
            }
        }

        public void AddBlackOutDates()
        {
            foreach (var date in ViewModel.blackoutDates)
            {
                MyCalendar.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }

        private void TimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            // Allow only numeric input and colon
            Regex regex = new Regex("[0-9:]"); // This regex matches only digits and colon
            return regex.IsMatch(text);
        }

        private void TimeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Validate input against 12-hour time format (hh:mm)
            Regex regex = new Regex(@"^(0?[1-9]|1[0-2]):[0-5][0-9]$");
            if (textBox.Text.Length == 5 && !regex.IsMatch(textBox.Text))
            {
                // If input is not in the correct format, clear the textbox
                textBox.Text = "";
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
