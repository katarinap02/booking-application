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
using BookingApp.View.ViewModel;

namespace BookingApp.View
{

    public partial class ShowAndSearchAccommodations : Window, IObserver
    {
        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }

        public User User { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationRepository AccommodationRepository { get; set; }    


        public ShowAndSearchAccommodations(AccommodationRepository accommodationRepository, User user)
        {
            

            InitializeComponent();

            Accommodations = new ObservableCollection<AccommodationViewModel>();
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
                
                Accommodations.Add(new AccommodationViewModel(accommodation));
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

            AccommodationsDataGrid.ItemsSource = SearchAccommodations(queries);
           



        }


        private List<AccommodationViewModel> SearchAccommodations(List<string> queries)
        {
           

            ObservableCollection<AccommodationViewModel> totalAccommodations = new ObservableCollection<AccommodationViewModel>();
            foreach (Accommodation accommodation in AccommodationRepository.GetAll())
                totalAccommodations.Add(new AccommodationViewModel(accommodation));

            List<AccommodationViewModel> searchResults = FilterAccommodations(totalAccommodations, queries);  


            int totalItems = searchResults.Count;
            List<AccommodationViewModel> results = new List<AccommodationViewModel>();
            foreach (AccommodationViewModel accommodation in searchResults)
                results.Add(accommodation);

            return results;




        }

        private List<AccommodationViewModel> FilterAccommodations(ObservableCollection<AccommodationViewModel> totalAccommodations, List<string> queries)
        {
            List<AccommodationViewModel> filteredAccommodations = totalAccommodations.Where(accommodation => (string.IsNullOrEmpty(queries[0]) || accommodation.Name.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || accommodation.City.ToUpper().Contains(queries[1].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[2]) || accommodation.Country.ToUpper().Contains(queries[2].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[3]) || accommodation.Type.ToString().ToUpper().Contains(queries[3].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[4]) || Convert.ToInt32(queries[4]) <= accommodation.MaxGuestNumber) &&
                                                                           (string.IsNullOrEmpty(queries[5]) || Convert.ToInt32(queries[5]) >= accommodation.MinReservationDays)
                                                                           ).ToList();

            return filteredAccommodations;
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