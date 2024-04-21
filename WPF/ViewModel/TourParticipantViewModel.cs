using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel
{
    public class TourParticipantViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private int _reservationId;
        public int ReservationId
        {
            get { return _reservationId; }
            set
            {
                if (_reservationId != value)
                {
                    _reservationId = value;
                    OnPropertyChanged(nameof(ReservationId));
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private int _years;
        public int Years
        {
            get { return _years; }
            set
            {
                if (_years != value)
                {
                    _years = value;
                    OnPropertyChanged(nameof(Years));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourParticipantViewModel() { }
        public TourParticipantViewModel(TourParticipant tourParticipant)
        {
            _name = tourParticipant.Name;
            _id = tourParticipant.Id;
            _lastName = tourParticipant.LastName;
            _years = tourParticipant.Years;
        }

        public TourParticipant ToTourParticipant()
        {
            TourParticipant tourParticipant = new TourParticipant(_id, _reservationId, _name, _lastName, _years);
            tourParticipant.Id = _id;
            tourParticipant.ReservationId = _reservationId;
            tourParticipant.Name = _name;
            tourParticipant.LastName = _lastName;
            tourParticipant.Years = _years;
            return tourParticipant;
        }
        public override string ToString()
        {
            return $"Name:        {_name}\nLastName:  {_lastName}\nYears:          {_years}\n";
        }
    }
}
