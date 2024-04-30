using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.GuideViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.View.GuideWindows
{
    public partial class ReviewsWindow: Window 
    {

        
        public ReviewsWindowViewModel reviewsWindowViewModel { get; set; }

        public ReviewsWindow(int tour_id)
        {
            InitializeComponent();
            reviewsWindowViewModel = new ReviewsWindowViewModel(tour_id);
            DataContext = reviewsWindowViewModel;
        }

        
    }
}
