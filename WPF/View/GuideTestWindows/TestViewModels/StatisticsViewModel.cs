using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class StatisticsViewModel: ViewModelBase
    {
        private int GuideId;
        private ViewModelBase _currentChildView;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ICommand ShowRequestStats { get; }
        public ICommand ShowTourStats { get; }
        public StatisticsViewModel(int guide_id) 
        {
            GuideId = guide_id;
            ShowRequestStats = new ViewModelCommand(ExecuteShowRequestStats);
            ShowTourStats = new ViewModelCommand(ExecuteShowTourStats);
            ExecuteShowRequestStats(null);
        }

        public void ExecuteShowRequestStats(object obj) {
            CurrentChildView = new RequestStatsViewModel(GuideId);
        }
        public void ExecuteShowTourStats(object obj) {
            CurrentChildView = new TourStatsViewModel(GuideId);
        }
    }
}
