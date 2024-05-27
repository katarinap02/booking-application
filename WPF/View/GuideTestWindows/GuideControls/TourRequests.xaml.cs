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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.GuideTestWindows.GuideControls
{
    /// <summary>
    /// Interaction logic for TourRequests.xaml
    /// </summary>
    public partial class TourRequests : UserControl
    {
        public TourRequests()
        {
            InitializeComponent();
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            country_tb.Text = string.Empty;
            city_tb.Text = string.Empty;
            language_tb.Text = string.Empty;
            number_tb.Text= string.Empty;
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }
    }
}
