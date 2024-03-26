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

        public AccommodationRepository AccommodationRepository { get; set; }
        public Frame Frame { get; set; }    
      

        public AccommodationsPage(AccommodationRepository accommodationRepository, User user, Frame frame)
        {
            InitializeComponent();

            Accommodations = new ObservableCollection<AccommodationDTO>();
            this.AccommodationRepository = accommodationRepository;
            accommodationRepository.AccommodationSubject.Subscribe(this);
            this.User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            this.Frame = frame;
           
            Update();
        }
        public void Update()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
            {

                Accommodations.Add(new AccommodationDTO(accommodation));
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
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
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
            Frame.Content = new ReservationInfoPage(AccommodationRepository, SelectedAccommodation, User, Frame);




        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {



        }

    }
}
