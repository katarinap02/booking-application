using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands
{
    public class DatePickerValuesComparerConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2 || values[0] == null || values[1] == null)
                return false;


            DateTime? date1 = (DateTime?)values[0];
            DateTime? date2 = (DateTime?)values[1];

            if (date1 == null || date2 == null)
                return false;

            return DateTime.Compare(date1.Value, date2.Value) < 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
