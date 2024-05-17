using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.View.GuideTestWindows.GuideControls;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using LiveCharts.Definitions.Series;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class StartedTourViewModel: ViewModelBase
    {
        public TourViewModel Tour { get; set; }
        public ObservableCollection<CheckpointViewModel> CheckpointsWithColors { get; set; }
        public ObservableCollection<TourParticipantViewModel> ExpectedParticipants { get; set; }
        public ObservableCollection<TourParticipantViewModel> JoinedParticipants { get; set; }
        public TourParticipantViewModel SelectedJoinedParticipant { get; set; }
        public TourParticipantViewModel SelectedNotJoinedParticipant { get; set; }

        private readonly TourReservationService reservationService;
        private readonly TourParticipantService tourParticipantService;
        private readonly TourRepository tourRepository;
        private readonly GuidedTourRepository guidedTourRepository;
        public int currentCheckpointIndex;

        public event EventHandler FinnishTourEvent;
        public MyICommand NextCheckpointCommand { get; set; }
        public MyICommand JoinTourCommand { get; set; }
        public MyICommand RemoveFromTourCommand { get; set; }
        public MyICommand FinnishTourCommand { get; set; }

        public StartedTourViewModel(TourViewModel startedTour) {
            NextCheckpointCommand = new MyICommand(NextCheckpoint);
            JoinTourCommand = new MyICommand(JoinTour);
            RemoveFromTourCommand = new MyICommand(RemoveParticipant);
            FinnishTourCommand = new MyICommand(FinnishTour);

            Tour = startedTour;
            CheckpointsWithColors = new ObservableCollection<CheckpointViewModel>();
            currentCheckpointIndex = Tour.CurrentCheckpoint;            

            reservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
            tourRepository = new TourRepository();
            guidedTourRepository = new GuidedTourRepository();

            JoinedParticipants = new ObservableCollection<TourParticipantViewModel>();
            ExpectedParticipants = new ObservableCollection<TourParticipantViewModel>();
            ConvertExpectedParticipants(startedTour.Id);
            ConvertJoinedParticipants(startedTour.Id);
            InitializeCheckpoints();
        }

        public void InitializeCheckpoints()
        {
            var checkpoints = Tour.Checkpoints;
            foreach (var checkpoint in checkpoints)
            {
                CheckpointsWithColors.Add(new CheckpointViewModel { Name = checkpoint, IndicatorColor = Brushes.LightGray });
            }
            UpdateDesign();
        }

        public void JoinTour()
        {
            if (SelectedNotJoinedParticipant != null)
            {
                tourParticipantService.JoinTour(SelectedNotJoinedParticipant.Id, currentCheckpointIndex);                
                JoinedParticipants.Add(SelectedNotJoinedParticipant);
                ExpectedParticipants.Remove(SelectedNotJoinedParticipant);
            }
            else
            {
                MessageBox.Show("Please select the tourist that joined.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void RemoveParticipant()
        {
            if (SelectedJoinedParticipant != null)
            {
                tourParticipantService.RemoveFromTour(SelectedJoinedParticipant.Id);
                ExpectedParticipants.Add(SelectedJoinedParticipant);
                JoinedParticipants.Remove(SelectedJoinedParticipant);

            }
            else
            {
                MessageBox.Show("Please select a tourist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NextCheckpoint()
        {
            currentCheckpointIndex++;
            tourRepository.nextCheckpoint(Tour.Id);

            if (currentCheckpointIndex == Tour.Checkpoints.Count() - 1)
            {
                UpdateDesign();
                MessageBox.Show("You have reached the final checkpoint. The tour has been finnished.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                FinnishTour();
            }
            else if (currentCheckpointIndex < Tour.Checkpoints.Count() - 1)
            {
                UpdateDesign();
            }
        }
        protected virtual void FinnishTour() 
        {
            tourRepository.finnishTour(Tour.Id);
            guidedTourRepository.Remove(Tour.Id);
            FinnishTourEvent?.Invoke(this, EventArgs.Empty);
        }
        

        private void UpdateDesign()
        {            
            for (int i = 0; i < CheckpointsWithColors.Count; i++)
            {
                if (i == currentCheckpointIndex)
                {
                    CheckpointsWithColors[i].IndicatorColor = Brushes.HotPink;
                }
                else
                {
                    CheckpointsWithColors[i].IndicatorColor = Brushes.LightGray;
                }
            }
        }

        public void ConvertExpectedParticipants(int tour_id)
        {
            List<TourParticipant> participants = reservationService.GetNotJoinedParticipants(tour_id);
            foreach (var participant in participants)
            {
                ExpectedParticipants.Add(new TourParticipantViewModel(participant));
            }
        } 
        public void ConvertJoinedParticipants(int tour_id)
        {
            List<TourParticipant> participants = reservationService.GetJoinedParticipantsByTour(tour_id);
            foreach (var participant in participants)
            {
                JoinedParticipants.Add(new TourParticipantViewModel(participant));
            }
        }
    }
}
