using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.GuideWindows
{
    public partial class GuideMainWindow : Window
    {
        public List<Tour> TodaysTours;
        public User Guide { get; set; }
        public List<TourDTO> TodayDTOs;
        private readonly TourRepository tourRepository;
        public TourDTO SelectedTour { get; set; }

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
            TodayDTOs = new List<TourDTO>();

            foreach (Tour tour in TodaysTours)
            {
                TodayDTOs.Add(new TourDTO(tour));
            }
            ToursDataGrid.ItemsSource = TodayDTOs;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTourWindow newTourWindow = new NewTourWindow(Guide);
            newTourWindow.ShowDialog();
            GetGridData();
        }

        private void SelectTourButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null) {
                GuideWithTourWindow guideWithTourWindow = new GuideWithTourWindow(SelectedTour, Guide);
                guideWithTourWindow.Show();
                Close();
            }
            else {
                Tour t = tourRepository.GetTourById(10);
                GuideWithTourWindow guideWithTourWindow = new GuideWithTourWindow(new TourDTO(t), Guide);
                guideWithTourWindow.Show();
                Close();
            }
        }
    }
}
