using BookingApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Repository.ReservationRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.RepositoryInterfaces.Reservations;

namespace BookingApp.View.GuideWindows
{
    public partial class TouristListWindow : Window
    {
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourParticipantService _tourParticipantService;
        public List<TourParticipant> tourParticipants { get; set; }
        public ObservableCollection<TourParticipantViewModel> tourParticipantDTOs { get; set; }
        public TourParticipantViewModel SelectedParticipant { get; set; }
        public int CheckpointNumber { get; set; }

        public TouristListWindow(int tour_id, int current_checkpoint) // BITNO popraviti binding!
        {
            InitializeComponent();
            DataContext = this;
            _tourReservationRepository = new TourReservationRepository();
            _tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());

            prepareData(tour_id);            
            
            CheckpointNumber = current_checkpoint;
        }

        public void prepareData(int tour_id) {
            tourParticipants = _tourReservationRepository.GetNotJoinedReservations(tour_id); 
            tourParticipantDTOs = new ObservableCollection<TourParticipantViewModel>();
            foreach( TourParticipant tp in  tourParticipants )
            { 
                tourParticipantDTOs.Add(new TourParticipantViewModel(tp)); 
            }
            dataGrid.ItemsSource = tourParticipantDTOs;
        }

        private void JoinedButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && SelectedParticipant != null)
            {
                _tourParticipantService.JoinTour(SelectedParticipant.Id, CheckpointNumber);
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
