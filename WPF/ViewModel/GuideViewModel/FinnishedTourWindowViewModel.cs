using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.GuideWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class FinnishedTourWindowViewModel
    {

        private readonly TourService TourService;
        public User Guide { get; set; }
        public TourViewModel SelectedTour { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TourViewModel> tourViewModels;
        public ObservableCollection<TourViewModel> TourViewModels
        {
            get { return tourViewModels; }
            set
            {
                tourViewModels = value;
                OnPropertyChanged(nameof(TourViewModels));
            }
        }

        public MyICommand Stats { get; set; }
        public MyICommand Review { get; set; }

        public FinnishedTourWindowViewModel(User guide)
        {

            TourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Guide = guide;
            SelectedTour = new TourViewModel();
            TourViewModels = new ObservableCollection<TourViewModel>();
            Stats = new MyICommand(Stats_Click);
            Review = new MyICommand(Review_Click);
            getGridData();
        }

        public void getGridData()
        {
            List<Tour> tours = TourService.findFinnishedToursByGuide(Guide.Id);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            TourViewModels = newViewModels;
        }

        private void Stats_Click()
        {
            if (SelectedTour != null)
            {
                TourStatsWindow tourStatsWindow1 = new TourStatsWindow(SelectedTour.Id);
                tourStatsWindow1.Show();
            }
            else
            {
                MessageBox.Show("No tour is selected!");
            }
        }

        private void Review_Click()
        {
            if (SelectedTour != null)
            {
                ReviewsWindow reviewsWindow = new ReviewsWindow(SelectedTour.Id);
                reviewsWindow.Show();
            }
            else
            {
                MessageBox.Show("No tour is selected!");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
