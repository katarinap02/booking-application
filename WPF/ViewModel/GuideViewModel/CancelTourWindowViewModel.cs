using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
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
    public class CancelTourWindowViewModel
    {
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

        public TourViewModel SelectedTour { get; set; }

        private readonly TourService tourService;
        public User Guide { get; set; }
        public MyICommand Exit {  get; set; }
        public MyICommand Cancel { get; set; }

        public CancelTourWindowViewModel(User guide)
        {
            tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Guide = guide;
            TourViewModels = new ObservableCollection<TourViewModel>();
            Exit = new MyICommand(Exit_Click);
            Cancel = new MyICommand(CancelTour_Click);
            getGridData();
        }

        public void getGridData()
        {
            TourViewModels.Clear();
            List<Tour> tours = tourService.findToursToCancel(Guide.Id);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            TourViewModels = newViewModels;
        }

        private void CancelTour_Click()
        {
            if (SelectedTour != null)
            {                
                tourService.cancelTour(SelectedTour.Id, Guide.Id);
                getGridData();
            }
            else
            {
                MessageBox.Show("Please select a tour to cancel!");
            }
        }

        private void Exit_Click()
        {
            // izameniti  BITNO
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
