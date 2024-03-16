using BookingApp.DTO;
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

namespace BookingApp.View
{

    public partial class ShowAndSearchAccommodations : Window, IObserver
    {
        public ObservableCollection<AccommodationDTO> Accommodations { get; set; }

        public User User { get; set; }
        public AccommodationDTO SelectedAccommodation { get; set; }

        public AccommodationRepository AccommodationRepository { get; set; }    


        public ShowAndSearchAccommodations(AccommodationRepository accommodationRepository, User user)
        {
            

            InitializeComponent();

            Accommodations = new ObservableCollection<AccommodationDTO>();
            this.AccommodationRepository = accommodationRepository;
            accommodationRepository.AccommodationSubject.Subscribe(this);
            this.User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            Update();
            

        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in AccommodationRepository.GetAll())
            {
                
                Accommodations.Add(new AccommodationDTO(accommodation));
                //MessageBox.Show(Accommodations[0].Type.ToString());
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> queries = new List<string>();
            queries.Add(txtSearchName.Text);
            queries.Add(txtSearchCity.Text);
            queries.Add(txtSearchCountry.Text);
            queries.Add(txtSearchType.Text);
            queries.Add(txtSearchGuestNumber.Text);
            queries.Add(txtSearchReservationDays.Text);

            AccommodationsDataGrid.ItemsSource = SearchAccommodations(queries);



        }

        private List<Accommodation> SearchAccommodations(List<string> queries)
        {
            string nameQuery = queries[0];
            string cityQuery = queries[1];
            string countryQuery = queries[2];
            string typeQuery = queries[3];
            string guestQuery = queries[4];
            string reservationQuery = queries[5];

            ObservableCollection<AccommodationDTO> totalAccommodations = new ObservableCollection<AccommodationDTO>();
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
                totalAccommodations.Add(new AccommodationDTO(accommodation));

            var searchResults = totalAccommodations.Where(accommodation => (string.IsNullOrEmpty(nameQuery) || accommodation.Name.ToUpper().Contains(nameQuery.ToUpper())) &&
                                                                           (string.IsNullOrEmpty(cityQuery) || accommodation.City.ToUpper().Contains(cityQuery.ToUpper())) &&
                                                                           (string.IsNullOrEmpty(countryQuery) || accommodation.Country.ToUpper().Contains(countryQuery.ToUpper())) &&
                                                                           (string.IsNullOrEmpty(typeQuery) || accommodation.Type.ToString().ToUpper().Contains(typeQuery.ToUpper())) &&
                                                                           (string.IsNullOrEmpty(guestQuery) || Convert.ToInt32(guestQuery) <= accommodation.MaxGuestNumber)&&
                                                                           (string.IsNullOrEmpty(reservationQuery) || Convert.ToInt32(reservationQuery) >= accommodation.MinReservationDays)
                                                                           ).ToList();


            int totalItems = searchResults.Count;
            List<Accommodation> results = new List<Accommodation>();
            foreach (AccommodationDTO accommodation in searchResults)
                results.Add(accommodation.ToAccommodation());

            return results;




        }

        private void ReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation to reserve");

            }
            else
            {
                DayNumberPopUp dayNumberPopup = new DayNumberPopUp(AccommodationRepository, SelectedAccommodation, User);
                dayNumberPopup.ShowDialog();

            }
            

        }
    }
}