using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.View.GuestWindows;
using BookingApp.View.HostWindows;
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


namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window, IObserver
    {
        public ObservableCollection<AccommodationReservationDTO> Accommodations { get; set; }
        public AccommodationRepository accommodationRepository { get; set; }
        public AccommodationReservationRepository accommodationReservationRepository { get; set; }

        public AccommodationReservationDTO SelectedAccommodation { get; set; }


        public HostWindow()
        {

            InitializeComponent();
            
            Accommodations = new ObservableCollection<AccommodationReservationDTO>();
            accommodationRepository = new AccommodationRepository();
            accommodationReservationRepository = new AccommodationReservationRepository();
            DataContext = this;
            Update();
            


        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetGuestForRate())
            {
                Accommodations.Add(new AccommodationReservationDTO(accommodation));
                
            }
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
          //  NavigateToPage("RegisterAccommodationPage");
            RegisterAccommodationWindow registerWindow = new RegisterAccommodationWindow(accommodationRepository);
            registerWindow.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                RateGuestWindow rateGuestWindow = new RateGuestWindow(SelectedAccommodation);
                rateGuestWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select guest to rate.");
            }

            Update();
        }

        private void NavigateToPage(string pageName)
        {
            
          //  String pageUri = "View/HostPages/" + pageName + ".xaml"; // ovo je nacin sa putanjama, a moze da se instancira i nova stranica prilikom navigacije, pa da ne moraju da se koriste putanje, ali ima neke razlike u ponasanju stranica prilikom navigacije (procitati na linku)
            //HostFrame.Navigate(new Uri(pageUri, UriKind.RelativeOrAbsolute)); // ovo je skraceni zapis za MainFrame.NavigationService.Navigate(...);
        }




    }
}
