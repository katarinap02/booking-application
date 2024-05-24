using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.TouristViewModel
{
    public class ComplexTourRequestWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly ComplexTourRequestService _complexTourRequestService;

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        public ObservableCollection<TourRequestWindowViewModel> TourRequests {  get; set; }

        private ICommand _removeTourCommand;
        public ICommand RemoveTourCommand
        {
            get
            {
                if (_removeTourCommand == null)
                {
                    _removeTourCommand = new RelayCommand(
                        param => RemoveTour(param),
                        param => CanRemoveParticipant());
                }
                return _removeTourCommand;
            }
        }
        private void RemoveTour(object tourRequest)
        {
            TourRequests.Remove(tourRequest as TourRequestWindowViewModel);
        }
        private bool CanRemoveParticipant()
        {
            return TourRequests.Count > 0;
        }
        private int _numberOfTours;
        public int NumberOfTours
        {
            get
            {
                return _numberOfTours;
            }
            set
            {
                if (_numberOfTours != value)
                {
                    _numberOfTours = value;
                    OnPropertyChanged(nameof(NumberOfTours));
                }
            }
        }

        private string _name;
        [Required(ErrorMessage = "Name is Required")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if( _name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public bool HasErrors => Errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }

            return Enumerable.Empty<string>();
        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            // ovde treba raise can execute change

        }

        public void InitializeWindow()
        {

        }

        public ComplexTourRequestWindowViewModel()
        {
            _complexTourRequestService = new ComplexTourRequestService(Injector.Injector.CreateInstance<IComplexTourRequestRepository>());
            TourRequests = new ObservableCollection<TourRequestWindowViewModel>();
        }

    }
}
