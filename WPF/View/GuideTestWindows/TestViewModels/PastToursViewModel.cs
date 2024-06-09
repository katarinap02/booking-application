using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class PastToursViewModel: ViewModelBase
    {
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

        private int GuideId;

        public MyICommand AddTour { get; set; }
        public MyICommand AddTourDate { get; set; }
        public MyICommand Reviews { get; set; }

        public PastToursViewModel(int id) {
            GuideId = id;
            tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            TourViewModels = new ObservableCollection<TourViewModel>();
            getToursByGuide(id);
            AddTour = new MyICommand(AddNewTour);
            AddTourDate = new MyICommand(AddNewTourDate);
            Reviews = new MyICommand(ShowReviews);
        }

        private void getToursByGuide(int guideId)
        {
            TourViewModels.Clear();
            List<Tour> tours = tourService.getPastToursByGuide(guideId);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            TourViewModels = newViewModels;
        }

        public void AddNewTour()
        {
            AddingTourWindow addingTourWindow = new AddingTourWindow(GuideId);
            addingTourWindow.ShowDialog();
        }

        public void AddNewTourDate()
        {
            MessageBox.Show("Not implemented yet");
        }

        public void ShowReviews()
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow();
            reviewsWindow.ShowDialog();
        }
    }
}
