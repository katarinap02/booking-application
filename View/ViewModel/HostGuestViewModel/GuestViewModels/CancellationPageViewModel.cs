﻿using BookingApp.Model;
using BookingApp.Services;
using BookingApp.View.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View
{
    public class CancellationPageViewModel
    {
        public string AccommodationDetails { get; set; }
       
      
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public ReservationCancellationService ReservationCancellationService { get; set; }
        public CancellationPageViewModel(AccommodationReservationViewModel selectedReservation)
        {
            SelectedReservation = selectedReservation;
            AccommodationReservationService = new AccommodationReservationService();
            AccommodationService = new AccommodationService();
            ReservationCancellationService = new ReservationCancellationService();


        }

        public void CancelReservation_Click(object sender, RoutedEventArgs e)
        {


            AccommodationReservationService.CancelReservation(AccommodationService, ReservationCancellationService, SelectedReservation);


        }

    }
}