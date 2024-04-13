using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.GuideWindows
{
    public partial class ReviewsWindow: Window 
    {

        private readonly GuideRateService _guideRateService;

        public ObservableCollection<GuideRateViewModel> guideRateViewModels { get; set; }   

        public ReviewsWindow(int tour_id)
        {
            _guideRateService = new GuideRateService();
            InitializeComponent();
            DataContext = this;
        }

        private void getData(int tour_id) {
            
        }

    }
}
