using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.View.GuideWindows
{
    public partial class TouristListWindow : Window
    {
        public ObservableCollection<TouristReservationInfo> TouristReservationInfos { get; set; }
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourParticipantRepository _tourParticipantRepository;

        public TouristListWindow()
        {
            InitializeComponent();

            _tourReservationRepository = new TourReservationRepository();
            _tourParticipantRepository = new TourParticipantRepository();

            var tourReservations = _tourReservationRepository.GetNotJoinedReservations(0); //dodaj id ture

            // Populate the ObservableCollection with TouristReservationInfo objects
            TouristReservationInfos = new ObservableCollection<TouristReservationInfo>();
            foreach (var reservation in tourReservations)
            {
                TouristReservationInfos.Add(new TouristReservationInfo
                {
                    TouristName = _tourParticipantRepository.GetAllParticipantNames(reservation.Id),
                    ReservationID = reservation.Id
                });
            }

            // Set the ObservableCollection as the ItemsSource for the DataGrid
            dataGrid.ItemsSource = TouristReservationInfos;
        }

        private void JoinedButton_Click(object sender, RoutedEventArgs e)
        {
            // Perform action using the selected row
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is TouristReservationInfo selectedInfo)
            {
                int reservationId = selectedInfo.ReservationID;
                _tourReservationRepository.JoinTour(reservationId);
            }
            else
            {
                MessageBox.Show("Please select a row first.");
            }
        }
    }

    public class TouristReservationInfo
    {
        public string TouristName { get; set; }
        public int ReservationID { get; set; }
    }
}
