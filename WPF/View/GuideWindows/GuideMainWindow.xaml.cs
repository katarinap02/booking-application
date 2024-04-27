using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Domain.Model.Features;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;

namespace BookingApp.View.GuideWindows
{
    public partial class GuideMainWindow : Window
    {
        public List<Tour> TodaysTours;
        public User Guide { get; set; }
        public List<TourViewModel> TodayDTOs;
        private readonly TourRepository tourRepository;
        public TourViewModel SelectedTour { get; set; }

        public GuideMainWindow(User user)
        {
            tourRepository = new TourRepository();
            Guide = user;
            
            DataContext = this;
            InitializeComponent();

            GetGridData();

        }

        public void GetGridData() {
            TodaysTours = tourRepository.findToursNeedingGuide();
            TodayDTOs = new List<TourViewModel>();

            foreach (Tour tour in TodaysTours)
            {
                TodayDTOs.Add(new TourViewModel(tour));
            }
            ToursDataGrid.ItemsSource = TodayDTOs;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTourWindow newTourWindow = new NewTourWindow(Guide);
            newTourWindow.ShowDialog();
            GetGridData();
        }

        private void CancelTours_Click(object sender, RoutedEventArgs e)
        {
            CancelTourWindow cancelTourWindow = new CancelTourWindow(Guide);
            cancelTourWindow.ShowDialog();
        }

        private void FinnishedTours_Click(object sender, RoutedEventArgs e)
        {
            FinnishedTour finnishedTourWindow = new FinnishedTour(Guide);
            finnishedTourWindow.ShowDialog();
        }

        private void SelectTourButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null) {
                GuideWithTourWindow guideWithTourWindow = new GuideWithTourWindow(SelectedTour, Guide);
                guideWithTourWindow.Show();
                Close();
            }
            else {
                MessageBox.Show("Please select a tour to cancel!");
            }
        }

        private void YearlyTours_Click(object sender, RoutedEventArgs e)
        {
            MostVisitedTourWindow mostVisitedTourWindow = new MostVisitedTourWindow(Guide);
            mostVisitedTourWindow.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
