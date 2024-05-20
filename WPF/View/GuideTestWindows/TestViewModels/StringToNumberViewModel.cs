using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class StringToNumberViewModel: ViewModelBase
    {
        private string _number;
        public string Number
        {
            get => _number;
            set 
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }

        }
        public StringToNumberViewModel() { }
        public int convertToInt() {
            if (Number == null || string.IsNullOrEmpty(Number)) { return 0; }
            return Convert.ToInt32(Number);
        }
    }
}
