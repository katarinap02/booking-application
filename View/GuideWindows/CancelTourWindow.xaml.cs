using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.GuideWindows
{
    public partial class CancelTourWindow: Window
    {
        private readonly TourRepository TourRepository;
        public User Guide { get; set; }
        public TourViewModel SelectedTour { get; set; }
        public ObservableCollection<TourViewModel> TourViewModels;

        public CancelTourWindow(User guide)
        {
            InitializeComponent();
            TourRepository = new TourRepository();
            Guide = guide;
            TourViewModels = new ObservableCollection<TourViewModel>();
            
            getGridData();
        }

        public void getGridData()
        {
            List<Tour> tours = TourRepository.findToursToCancel(Guide.Id);
            foreach (Tour tour in tours)
            {
                TourViewModels.Add(new TourViewModel(tour));
            }
            ToursDataGrid.ItemsSource = TourViewModels;
        }

        private void CancelTour_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour  != null)
            {
                MessageBox.Show(SelectedTour.Id.ToString());
                TourRepository.cancelTour(SelectedTour.Id, Guide.Id);
                //getGridData();
            }
            else
            {
                MessageBox.Show("Please select a tour to cancel!");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
