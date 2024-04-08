﻿using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.HostPages
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page, IObserver
    {
        public ObservableCollection<AccommodationDTO> Accommodations { get; set; }

        public AccommodationRepository accommodationRepository { get; set; }

        public HostService hostService { get; set; }

        public AccommodationReservationDTO SelectedAccommodation { get; set; }

        public Host host {  get; set; }

        public HostDTO hostDTO { get; set; }

        
        public FirstPage(User user)
        {
            InitializeComponent();
            Accommodations = new ObservableCollection<AccommodationDTO>();
            accommodationRepository = new AccommodationRepository();
            hostService = new HostService();
            DataContext = this;
            host = hostService.GetByUsername(user.Username);
            hostService.BecomeSuperHost(host);
            hostDTO = new HostDTO(host);
            Update();
        }

        private void Displacement_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
        private void Forums_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
        private void RateGuest_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }
        private void Statistic_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in accommodationRepository.GetAll())
            {
                
                Accommodations.Add(new AccommodationDTO(accommodation));

            }
        }
    }
}