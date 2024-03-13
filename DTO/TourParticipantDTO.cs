using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class TourParticipantDTO : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private int reservationId;
        public int ReservationId
        {
            get { return reservationId;}
            set
            {
                if (reservationId != value)
                {
                    reservationId = value;
                    OnPropertyChanged(nameof(ReservationId));
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if(lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private int years;
        public int Years
        {
            get { return years;}
            set
            {
                if(years != value)
                {
                    years = value;
                    OnPropertyChanged(nameof(Years));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourParticipantDTO() { }
        public TourParticipantDTO(TourParticipant tourParticipant)
        {
            name = tourParticipant.Name;
            id = tourParticipant.Id;
            lastName = tourParticipant.LastName;
            years = tourParticipant.Years;
        }

        public TourParticipant ToTourParticipant()
        {
            TourParticipant tourParticipant = new TourParticipant(id, reservationId, name, lastName, years);
            tourParticipant.Id = id;
            tourParticipant.ReservationId = reservationId;
            tourParticipant.Name = name;
            tourParticipant.LastName = lastName;
            tourParticipant.Years = years;
            return tourParticipant;
        }
        public override string ToString()
        {
            return $"Name:        {name}\nLastName:  {lastName}\nYears:          {years}\n";
        }
    }
}
