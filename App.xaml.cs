using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp
{
    // Version of the App : BookingApp 4.0
    public partial class App : System.Windows.Application
    {
       
        public void ChangeLanguage(string currLang)
        {
            if (currLang.Equals("en-US"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
               
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
               
            }
        }
    }
}
