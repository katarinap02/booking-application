using BookingApp.Model;
using BookingApp.Repository;
using System.Collections.Generic;
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
        public int CheckpointNumber { get; set; }

        public TouristListWindow(int tour_id, int current_checkpoint)
        {
            InitializeComponent();

            _tourReservationRepository = new TourReservationRepository();
            _tourParticipantRepository = new TourParticipantRepository();

            var tourReservations = _tourReservationRepository.GetNotJoinedReservations(tour_id);
            CheckpointNumber = current_checkpoint;

            TouristReservationInfos = new ObservableCollection<TouristReservationInfo>();
            if(tourReservations != null)
            {
                prepareData(tourReservations);
            }

            dataGrid.ItemsSource = TouristReservationInfos;
        }

        private void prepareData(List<TourReservation> tourReservations) {
            foreach (var reservation in tourReservations)
            {
                TouristReservationInfos.Add(new TouristReservationInfo
                {
                    TouristName = _tourParticipantRepository.GetAllParticipantNames(reservation.Id),
                    ReservationID = reservation.Id
                });
            }
        }

        private void JoinedButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is TouristReservationInfo selectedInfo)
            {
                int reservationId = selectedInfo.ReservationID;
                MessageBox.Show(reservationId.ToString());
                _tourReservationRepository.JoinTour(reservationId, CheckpointNumber);
                Close();
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
