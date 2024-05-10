using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class CheckpointViewModel : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        private Brush indicatorColor;
        public Brush IndicatorColor
        {
            get { return indicatorColor; }
            set { indicatorColor = value; NotifyPropertyChanged(nameof(IndicatorColor)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
