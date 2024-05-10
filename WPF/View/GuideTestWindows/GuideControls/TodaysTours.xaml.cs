using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class TodaysTours : UserControl
    {
        public ObservableCollection<CheckpointViewModel> CheckpointsWithColors { get; set; }
        public TodaysTours()
        {
            DataContext = this;
            CheckpointsWithColors = new ObservableCollection<CheckpointViewModel>
            {
                new CheckpointViewModel { Name = "Checkpoint 1", IndicatorColor = Brushes.LightGray },
                new CheckpointViewModel { Name = "Checkpoint 2", IndicatorColor = Brushes.LightGray },
                new CheckpointViewModel { Name = "Checkpoint 3", IndicatorColor = Brushes.LightGray }
            };
            InitializeComponent();
        }
    }
}
