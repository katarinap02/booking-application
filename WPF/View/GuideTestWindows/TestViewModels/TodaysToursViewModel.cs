using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class TodaysToursViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event EventHandler<TourViewModel> StartTourRequested;
        public ObservableCollection<CheckpointViewModel> CheckpointsWithColors { get; set; }
        public ObservableCollection<string> TourParticipants { get; set; }

        private readonly TourService _tourService;
        private readonly TourReservationService _reservationService;
        public ObservableCollection<TourViewModel> Tours { get; set; }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                UpdateCheckpoints();
                UpdateTourists();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly GuidedTourRepository guidedTourRepository = new GuidedTourRepository();
        private readonly TourRepository tourRepository = new TourRepository();

        private int GuideId;

        public MyICommand ShowInfo {  get; set; }
        public MyICommand Start {  get; set; }

        public TodaysToursViewModel(int guide_id)
        {
            GuideId = guide_id;
            ShowInfo = new MyICommand(ShowingInfo);
            Start = new MyICommand(StartTour);
            _reservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Tours = new ObservableCollection<TourViewModel>();
            TourParticipants = new ObservableCollection<string> { "Select a tour" };
            CheckpointsWithColors = new ObservableCollection<CheckpointViewModel> { new CheckpointViewModel("Select a tour", Brushes.LightGray) };
            GetGridData();
        }

        public void ShowingInfo()
        {
            MessageBox.Show(SelectedTour.Name);
        }
        public void StartTour()
        {
            if(SelectedTour != null)
            {                
                OnStartTourRequested(SelectedTour);
            }
            else
            {
                MessageBox.Show("Please select a tour!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected virtual void OnStartTourRequested(TourViewModel selectedTour)
        {
            guidedTourRepository.Add(GuideId, selectedTour.Id);
            tourRepository.activateTour(selectedTour.Id, GuideId);
            StartTourRequested?.Invoke(this, selectedTour);
        }

        public void GetGridData()
        {
            List<Tour> tours = _tourService.findToursNeedingGuide();
            Tours.Clear();
            foreach (Tour tour in tours)
            {
                Tours.Add(new TourViewModel(tour));
            }
        }

        private void UpdateCheckpoints()
        {
            CheckpointsWithColors.Clear();
            if (SelectedTour != null)
            {
                foreach (string checkpt in SelectedTour.Checkpoints)
                {
                    CheckpointsWithColors.Add(new CheckpointViewModel(checkpt, Brushes.LightGray));
                }
            }
        }

        private void UpdateTourists()
        {
            TourParticipants.Clear();
            if (SelectedTour != null)
            {
                List<TourParticipant> tourParticipants = _reservationService.GetNotJoinedParticipants(SelectedTour.Id);
                foreach (TourParticipant participant in tourParticipants)
                {
                    TourParticipants.Add(participant.Name + " " + participant.LastName);
                }
                if (tourParticipants.Count == 0)
                {
                    TourParticipants.Add("No tourists");
                }
            }
        }

    }
}
