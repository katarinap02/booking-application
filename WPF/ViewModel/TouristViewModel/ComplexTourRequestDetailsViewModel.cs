using BookingApp.Domain.Model.Features;
using BookingApp.WPF.View.TouristWindows;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ComplexTourRequestDetailsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TourRequestViewModel> TourRequests { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        private TourRequestStatus _status;
        public TourRequestStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public DateTime _acceptedDate;
        public DateTime AcceptedDate
        {
            get
            {
                return _acceptedDate;
            }
            set
            {
                if(_acceptedDate != value)
                {
                    _acceptedDate = value;
                    OnPropertyChanged(nameof(AcceptedDate));
                }
            }
        }

        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if(_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if(_city != value)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if(_language != value)
                {
                    _language = value;
                    OnPropertyChanged(nameof(Language));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void ExecuteCloseWindow(object obj)
        {
            Messenger.Default.Send(new CloseWindowMessage());

        }
        public ComplexTourRequestDetailsViewModel ToComplexDetailsViewModel(TourRequestViewModel tourRequestViewModel)
        {
            ComplexTourRequestDetailsViewModel viewModel = new ComplexTourRequestDetailsViewModel();
            viewModel.AcceptedDate = tourRequestViewModel.AcceptedDate;
            viewModel.Status = tourRequestViewModel.Status;
            viewModel.Country = tourRequestViewModel.Country;
            viewModel.City = tourRequestViewModel.City;
            viewModel.Language = tourRequestViewModel.Language;

            var sortedRequests = new ObservableCollection<TourRequestViewModel>(tourRequestViewModel.TourRequestsForComplex.OrderBy(request => request.StartDate));
            viewModel.TourRequests = sortedRequests;
            return viewModel;
        }
        public ComplexTourRequestDetailsViewModel()
        {
            TourRequests = new ObservableCollection<TourRequestViewModel>();
            CloseWindowCommand = new RelayCommand(ExecuteCloseWindow);
        }
    }
}
