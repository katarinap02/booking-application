using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;

namespace BookingApp.WPF.View.TouristWindows
{
    public class ValidationErrorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int errorCount = (int)value;

            if (errorCount > 0)
            {
               return Validation.GetErrors((DependencyObject)parameter)[0].ErrorContent;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
