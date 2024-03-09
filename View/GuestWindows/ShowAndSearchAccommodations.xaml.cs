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
namespace BookingApp.View
{

    public partial class ShowAndSearchAccommodations : Window, IObserver
    {
        public ObservableCollection<AccommodationDTO> Accommodations { get; set; }

        public AccommodationDTO SelectedAccommodation { get; set; }

        public AccommodationRepository accommodationRepository { get; set; }    

        public ShowAndSearchAccommodations(AccommodationRepository accommodationRepository)
        {
            

            InitializeComponent();

            Accommodations = new ObservableCollection<AccommodationDTO>();
            this.accommodationRepository = accommodationRepository;
            accommodationRepository.AccommodationSubject.Subscribe(this);
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            Update();
            

        }

        public void Update()
        {
            Accommodations.Clear();
            foreach(Accommodation accommodation in accommodationRepository.GetAll())
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
            queries.Add(comboSearchType.Text);
            queries.Add(txtSearchGuestNumber.Text);
            queries.Add(txtSearchReservationDays.Text);

            AccommodationsDataGrid.ItemsSource = SearchAccommodations(queries);



        }

        private List<Accommodation> SearchAccommodations(List<string> queries)
        {
            string nameQuery = queries[0];
            string Query = queries[0];
            string nameQuery = queries[0];



        }


    }
}