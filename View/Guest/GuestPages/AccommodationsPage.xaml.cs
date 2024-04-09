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
using System.Security.Cryptography;
using BookingApp.Services;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for AccommodationsPage.xaml
    /// </summary>
    public partial class AccommodationsPage : Page, IObserver
    {
        public ObservableCollection<AccommodationDTO> Accommodations { get; set; }

        public User User { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public Frame Frame { get; set; }    

        public HostService HostService { get; set; }
      

        public AccommodationsPage(AccommodationService accommodationService, AccommodationReservationService accommodationReservationService, User user, Frame frame)
        {
            InitializeComponent();

            Accommodations = new ObservableCollection<AccommodationDTO>();
            this.AccommodationService = accommodationService;
            AccommodationService.Subscribe(this);
            this.User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            this.Frame = frame;
            this.AccommodationReservationService = accommodationReservationService;
            this.HostService = new HostService();
            
           
            Update();
        }
        public void Update()
        {
            Accommodations.Clear();
            List<AccommodationDTO> superHostAccommodations = new List<AccommodationDTO>();
            List<AccommodationDTO> nonSuperHostAccommodations = new List<AccommodationDTO>();

            SeparateAccommodations(AccommodationService, superHostAccommodations, nonSuperHostAccommodations);
            

            foreach(AccommodationDTO superHostAccommodation in superHostAccommodations)
                Accommodations.Add(superHostAccommodation);
            
            foreach (AccommodationDTO nonSuperHostAccommodation in nonSuperHostAccommodations)
                Accommodations.Add(nonSuperHostAccommodation);
        }

        private void SeparateAccommodations(AccommodationService accommodationService, List<AccommodationDTO> superHostAccommodations, List<AccommodationDTO> nonSuperHostAccommodations)
        {
            foreach (Accommodation accommodation in AccommodationService.GetAll())
            {

                AccommodationDTO accommodationDTO = new AccommodationDTO(accommodation);
                Host host = HostService.GetById(accommodation.HostId);
                HostService.BecomeSuperHost(host);
                accommodationDTO.IsSuperHost = host.IsSuperHost;

                if (accommodationDTO.IsSuperHost)
                    superHostAccommodations.Add(accommodationDTO);
                else
                    nonSuperHostAccommodations.Add(accommodationDTO);

                //MessageBox.Show(Accommodations[0].Type.ToString());
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> queries = new List<string>();
            queries.Add(txtSearchName.Text); //nameQuery
            queries.Add(txtSearchCity.Text); //cityQuery
            queries.Add(txtSearchCountry.Text); //countryQuery
            queries.Add(txtSearchType.Text); //typeQuery
            queries.Add(txtSearchGuestNumber.Text); //guestQuery
            queries.Add(txtSearchReservationDays.Text); //reservationQuery

           AccommodationListBox.ItemsSource = SearchAccommodations(queries);




        }


        private List<AccommodationDTO> SearchAccommodations(List<string> queries)
        {


            ObservableCollection<AccommodationDTO> totalAccommodations = new ObservableCollection<AccommodationDTO>();
            foreach (Accommodation accommodation in AccommodationService.GetAll())
                totalAccommodations.Add(new AccommodationDTO(accommodation));

            List<AccommodationDTO> searchResults = FilterAccommodations(totalAccommodations, queries);


            int totalItems = searchResults.Count;
            List<AccommodationDTO> results = new List<AccommodationDTO>();
            foreach (AccommodationDTO accommodation in searchResults)
                results.Add(accommodation);

            return results;




        }

        private List<AccommodationDTO> FilterAccommodations(ObservableCollection<AccommodationDTO> totalAccommodations, List<string> queries)
        {
            List<AccommodationDTO> filteredAccommodations = totalAccommodations.Where(accommodation => (string.IsNullOrEmpty(queries[0]) || accommodation.Name.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || accommodation.City.ToUpper().Contains(queries[1].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[2]) || accommodation.Country.ToUpper().Contains(queries[2].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[3]) || accommodation.Type.ToString().ToUpper().Contains(queries[3].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[4]) || Convert.ToInt32(queries[4]) <= accommodation.MaxGuestNumber) &&
                                                                           (string.IsNullOrEmpty(queries[5]) || Convert.ToInt32(queries[5]) >= accommodation.MinReservationDays)
                                                                           ).ToList();

            return filteredAccommodations;
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e) {

            
            Button button = sender as Button;
            SelectedAccommodation = button.DataContext as AccommodationDTO;
            Frame.Content = new ReservationInfoPage(AccommodationService,  SelectedAccommodation, AccommodationReservationService, User, Frame);




        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {



        }

    }
}
