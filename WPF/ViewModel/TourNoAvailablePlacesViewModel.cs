using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel
{
    public class TourNoAvailablePlacesViewModel : INotifyPropertyChanged
    {
        public TourService _tourService { get; set; }

        public ObservableCollection<TourViewModel> Tours { get; set; }

        private TourViewModel _selectedTour;


        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshToursByCity()
        {
            Tours.Clear();
            List<TourViewModel> tours = ToTourViewModel(_tourService.GetTourByCityWithAvailablePlaces(SelectedTour.City));
            foreach (var tour in tours)
            {
                Tours.Add(tour);
            }
        }

        public void BookNumberOfParticipants()
        {
            TourReservationWindow tourReservationWindow = new TourReservationWindow(SelectedTour, 1, UserId);
            tourReservationWindow.ShowDialog();
        }

        public List<TourViewModel> ToTourViewModel(List<Tour> Tours)
        {
            // creating list from Tour to TourViewModel
            List<TourViewModel> ToursViewModel = new List<TourViewModel>();
            foreach (Tour tour in Tours)
            {
                ToursViewModel.Add(new TourViewModel(tour));
            }
            return ToursViewModel;
        }

        public TourNoAvailablePlacesViewModel()
        {
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            Tours = new ObservableCollection<TourViewModel>();
        }
    }
}
