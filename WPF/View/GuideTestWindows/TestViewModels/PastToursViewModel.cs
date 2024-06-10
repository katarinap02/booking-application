using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
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

        private static readonly GuideRateService guideRateService = new GuideRateService(Injector.Injector.CreateInstance<IGuideRateRepository>());

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
            if(SelectedTour == null)
            {
                MessageBox.Show("Please select a tour.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TourRequestDTOViewModel tourRequestDTOViewModel = new TourRequestDTOViewModel();
            tourRequestDTOViewModel.StartDate = DateTime.Now;
            tourRequestDTOViewModel.EndDate = DateTime.MaxValue;
            CalendarGuideWindow calendarGuideWindow = new CalendarGuideWindow(GuideId, tourRequestDTOViewModel);
            calendarGuideWindow.Show();
        }

        public void ShowReviews()
        {
            if(SelectedTour==null)
            {
                MessageBox.Show("Please select a tour.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(guideRateService.getRatesByTour(SelectedTour.Id).Count() == 0) {
                MessageBox.Show("Selected tour has no reviews yet!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReviewsWindow reviewsWindow = new ReviewsWindow(GuideId, SelectedTour);
            reviewsWindow.ShowDialog();
        }
    }
}
