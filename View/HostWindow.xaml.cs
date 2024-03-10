using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
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
            foreach (AccommodationReservation accommodation in accommodationReservationRepository.GetAll())
            {

                Accommodations.Add(new AccommodationReservationDTO(accommodation));
                
            }
        }

        private void RegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
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
            //    CommentForm viewCommentForm = new CommentForm(SelectedAccommodation);
             //   viewCommentForm.Show();
            }
            else
            {
                MessageBox.Show("Select guest to rate.");
            }
        }
    }
}
