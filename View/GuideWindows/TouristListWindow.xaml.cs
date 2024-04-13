using BookingApp.DTO;
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
        private readonly TourParticipantRepository _tourParticipantRepository;
        public List<TourParticipant> tourParticipants { get; set; }
        public ObservableCollection<TourParticipantDTO> tourParticipantDTOs { get; set; }
        public TourParticipantDTO SelectedParticipant { get; set; }
        public int CheckpointNumber { get; set; }

        public TouristListWindow(int tour_id, int current_checkpoint) // BITNO popraviti binding!
        {
            InitializeComponent();
            DataContext = this;
            _tourReservationRepository = new TourReservationRepository();
            _tourParticipantRepository = new TourParticipantRepository();

            prepareData(tour_id);            
            
            CheckpointNumber = current_checkpoint;
        }

        public void prepareData(int tour_id) {
            tourParticipants = _tourReservationRepository.GetNotJoinedReservations(tour_id); 
            tourParticipantDTOs = new ObservableCollection<TourParticipantDTO>();
            foreach( TourParticipant tp in  tourParticipants )
            {
                MessageBox.Show(tp.Name, "Window"); 
                tourParticipantDTOs.Add(new TourParticipantDTO(tp)); 
            }
            dataGrid.ItemsSource = tourParticipantDTOs;
        }

        private void JoinedButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && SelectedParticipant != null)
            {
                _tourParticipantRepository.JoinTour(SelectedParticipant.Id, CheckpointNumber);
                Close();
                MessageBox.Show("Tourist " + SelectedParticipant.Name +" "+ SelectedParticipant.LastName + " joined");
            }
            else
            {
                MessageBox.Show("Please select a row first.");
            }
        }
    }

}
