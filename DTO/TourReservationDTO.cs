using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourReservationDTO : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private List<int> participantIds = new List<int>();
        public List<int> ParticipantIds
        {
            get { return  participantIds; }
            set
            {
                if(participantIds != value)
                {
                    participantIds = value;
                    OnPropertyChanged(nameof(ParticipantIds));
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (tourId != value)
                {
                    tourId = value;
                    OnPropertyChanged(nameof(TourId));
                }
            }
        }

        private int startCheckpoint;

        public int StartCheckpoint
        {
            get { return startCheckpoint; }
            set
            {
                if (startCheckpoint != value)
                {
                    startCheckpoint = value;
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
            id = tourReservation.Id;
            participantIds = tourReservation.ParticipantIds;
            tourId= tourReservation.TourId;
            startCheckpoint = tourReservation.StartCheckpoint;
        }

        public TourReservation ToTourReservation()
        {
            TourReservation tourReservation = new TourReservation(Id, tourId, startCheckpoint);
            tourReservation.Id = id;
            tourReservation.TourId = tourId;
            tourReservation.StartCheckpoint = startCheckpoint;
            tourReservation.ParticipantIds = participantIds;
            return tourReservation;
        }
    }
}
