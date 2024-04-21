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
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}
