using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel
{
    public class TourReservationDTO : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private List<int> _participantIds = new List<int>();
        public List<int> ParticipantIds
        {
            get { return  _participantIds; }
            set
            {
                if(_participantIds != value)
                {
                    _participantIds = value;
                    OnPropertyChanged(nameof(ParticipantIds));
                }
            }
        }

        private int _tourId;
        public int TourId
        {
            get { return _tourId; }
            set
            {
                if (_tourId != value)
                {
                    _tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private int _touristId;
        public int TouristId
        {
            get { return _touristId; }
            set
            {
                if (_touristId != value)
                {
                    _touristId = value;
                    OnPropertyChanged(nameof(TouristId));
                }
            }
        }

        private int _startCheckpoint;

        public int StartCheckpoint
        {
            get { return _startCheckpoint; }
            set
            {
                if (_startCheckpoint != value)
                {
                    _startCheckpoint = value;
                    OnPropertyChanged(nameof(StartCheckpoint));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourReservationDTO() { }

        public TourReservationDTO(TourReservation tourReservation)
        {
            _id = tourReservation.Id;
            _participantIds = tourReservation.ParticipantIds;
            _tourId= tourReservation.TourId;
            _startCheckpoint = tourReservation.StartCheckpoint;
        }

        public TourReservation ToTourReservation()
        {
            TourReservation tourReservation = new TourReservation(Id, _tourId, _touristId, _startCheckpoint);
            tourReservation.Id = _id;
            tourReservation.TourId = _tourId;
            tourReservation.TouristId = _touristId;
            tourReservation.StartCheckpoint = _startCheckpoint;
            tourReservation.ParticipantIds = _participantIds;
            return tourReservation;
        }
    }
}
