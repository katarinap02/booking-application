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
    public partial class FinnishedTour: Window
    {
        TourRepository TourRepository;
        User Guide {  get; set; }
        TourViewModel SelectedTour { get; set; }
        ObservableCollection<TourViewModel> TourViewModels;

        public FinnishedTour(User guide)
        {
            TourRepository = new TourRepository();
            Guide = guide;
            SelectedTour = new TourViewModel();
            TourViewModels = new ObservableCollection<TourViewModel>();
            getGridData();
        }

        public void getGridData() {
            List<Tour> tours = TourRepository.findToursToCancel(Guide.Id);
            foreach(Tour tour in tours)
            {
                TourViewModels.Add(new TourViewModel(tour));
            }
            ToursDataGrid.ItemsSource = TourViewModels;
        }


    }
}
