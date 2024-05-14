using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class StringViewModel: ViewModelBase
    {
        private string _string;
        public string SString
        {
            get => _string;
            set
            {
                _string = value;
                OnPropertyChanged(nameof(SString));
            }

        }
        public StringViewModel() { }
    }
}
