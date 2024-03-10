﻿using BookingApp.DTO;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Observer;
using BookingApp.Model;


namespace BookingApp.View
{
    public partial class DayNumberPopUp : Window
    {
        public AccommodationDTO SelectedAccommodation { get; set; }

        public int DayNumber {  get; set; }
        public DayNumberPopUp(AccommodationDTO SelectedAccommodation)
        {
            InitializeComponent();
            this.SelectedAccommodation = SelectedAccommodation;

        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            DayNumber = Convert.ToInt32(txtDayNumber.Text);
            if(DayNumber < SelectedAccommodation.MinReservationDays)
                MessageBox.Show("Minimal number of reservation days is: " + SelectedAccommodation.MinReservationDays.ToString());
            else
            {
                ReservationCalendarWindow calendarWindow = new ReservationCalendarWindow(SelectedAccommodation, DayNumber);
                calendarWindow.ShowDialog();

            }
            
            
        }
    }
}
