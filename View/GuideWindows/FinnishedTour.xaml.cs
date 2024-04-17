﻿using BookingApp.ViewModel;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Services;

namespace BookingApp.View.GuideWindows
{
    public partial class FinnishedTour : Window
    {
        private readonly TourService TourService;
        public User Guide { get; set; }
        public TourViewModel SelectedTour { get; set; }
        public ObservableCollection<TourViewModel> TourViewModels;

        public FinnishedTour(User guide)
        {
            InitializeComponent();
            DataContext = this;
            TourService = new TourService();
            Guide = guide;
            SelectedTour = new TourViewModel();
            TourViewModels = new ObservableCollection<TourViewModel>();
            getGridData();
        }

        public void getGridData()
        {
            List<Tour> tours = TourService.findFinnishedToursByGuide(Guide.Id);
            foreach (Tour tour in tours)
            {
                TourViewModels.Add(new TourViewModel(tour));
            }
            ToursDataGrid.ItemsSource = TourViewModels;
        }

        private void Stats_Click(object sender, RoutedEventArgs e)
        {
            if(ToursDataGrid.SelectedItem != null)
            {
                SelectedTour = (TourViewModel)ToursDataGrid.SelectedItem;
                TourStatsWindow tourStatsWindow1 = new TourStatsWindow(SelectedTour.Id);
                tourStatsWindow1.Show(); //puca ovde
            }
            else
            {
                MessageBox.Show("No tour is selected!");
            }
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            if (ToursDataGrid.SelectedItem != null)
            {
                SelectedTour = (TourViewModel)ToursDataGrid.SelectedItem;
                ReviewsWindow reviewsWindow = new ReviewsWindow(SelectedTour.Id);
                reviewsWindow.Show();
            }
            else
            {
                MessageBox.Show("No tour is selected!");
            }
        }
    }
}