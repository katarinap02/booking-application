using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.Guest
{
    public class AppTheme
    {
        public static void ChangeTheme(Uri themeuri)
        {
            ResourceDictionary theme = new ResourceDictionary() { Source = themeuri };
            System.Windows.Application.Current.Resources.Clear();
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}
