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
        private readonly TourReservationRepository _tourReservationRepository;
        public List<TourParticipant> tourParticipants { get; set; }
        public int CheckpointNumber { get; set; }

        public TouristListWindow(int tour_id, int current_checkpoint) // BITNO poopraviti binding!
        {
            InitializeComponent();

            _tourReservationRepository = new TourReservationRepository();
            _tourParticipantRepository = new TourParticipantRepository();


            tourParticipants = _tourReservationRepository.GetNotJoinedReservations(tour_id);
            CheckpointNumber = current_checkpoint;

            dataGrid.ItemsSource = tourParticipants;
        }


        private void JoinedButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                MessageBox.Show(tourParticipants[0].LastName);
            }
            else
            {
                MessageBox.Show("Please select a row first.");
            }
        }
    }

}
