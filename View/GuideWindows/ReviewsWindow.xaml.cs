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
        public GuideRateViewModel selectedRate { get; set; }

        public ReviewsWindow(int tour_id)
        {
            _guideRateService = new GuideRateService(); 
            InitializeComponent();
            DataContext = this;
            getData(tour_id);
        }

        private void getData(int tour_id) {
            guideRateViewModels = new ObservableCollection<GuideRateViewModel>();
            foreach(GuideRateViewModel rate in _guideRateService.getRatesByTour(tour_id))
            {
                guideRateViewModels.Add(rate);
            }
        }

        private void Invalid_Click(object sender, RoutedEventArgs e)
        {
            if(selectedRate == null)
            {
                MessageBox.Show("PLease select review in order to mark it as invalid");
            }
            else
            {
                _guideRateService.markAsInvalid(selectedRate.Id);
                MessageBox.Show("Review marked as invalid", "Reviews Notification");
            }
        }
    }
}
