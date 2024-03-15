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
        // Informacije o vodicu u konstruktoru
        public User Guide { get; set; }
        // lista dostupnih tura
        public List<TourDTO> TodayDTOs;
        // tour repo -> TourDTO ???
        private readonly TourRepository TourRepository;
        public TourDTO SelectedTour { get; set; }

        public GuideMainWindow(User user)
        {
            TourRepository = new TourRepository();
            Guide = user;
            TodaysTours = TourRepository.findToursNeedingGuide();
            TodayDTOs = new List<TourDTO>();

            foreach(Tour tour in TodaysTours)
            {
                TodayDTOs.Add(new TourDTO(tour));
                MessageBox.Show((tour.Name));
            }
            DataContext = this;
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTourWindow newTourWindow = new NewTourWindow();
            newTourWindow.ShowDialog();
        }
    }
}
