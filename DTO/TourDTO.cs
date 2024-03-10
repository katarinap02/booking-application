using System;
using System.Collections.Generic;
using System.ComponentModel;
using BookingApp.Model;

namespace BookingApp.DTO
{
    public class TourDTO : INotifyPropertyChanged
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
                    OnPropertyChanged("Id");
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
                    OnPropertyChanged("Name");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (City != value)
                {
                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                if(Country != value)
                {
                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private int maxTourists;
        public int MaxTourists
        {
            get { return maxTourists; }
            set
            {
                if (maxTourists != value)
                {
                    maxTourists = value;
                    OnPropertyChanged("MaxTourists");
                }
            }
        }

        private List<string> checkpoints = new List<string>();
        public List<string> Checkpoints
        {
            get { return checkpoints; }
            set
            {
                if (checkpoints != value)
                {
                    checkpoints = value;
                    OnPropertyChanged("Checkpoints");
                }
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private float duration;
        public float Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private List<string> pictures = new List<string>();
        public List<string> Pictures
        {
            get { return pictures; }
            set
            {
                if (pictures != value)
                {
                    pictures = value;
                    OnPropertyChanged("Pictures");
                }
            }
        }

        private TourStatus status;
        public TourStatus Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int groupId;
        public int GroupId
        {
            get { return groupId; }
            set
            {
                if (groupId != value)
                {
                    groupId = value;
                    OnPropertyChanged("GroupId");
                }
            }
        }

        private int currentCheckpoint;
        public int CurrentCheckpoint
        {
            get { return currentCheckpoint; }
            set
            {
                if (currentCheckpoint != value)
                {
                    currentCheckpoint = value;
                    OnPropertyChanged("CurrentCheckpoint");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourDTO() { }

        public TourDTO (Tour tour)
        {
            name = tour.Name;
            id = tour.Id;
            city = tour.City;
            duration = tour.Duration;
            language = tour.Language;
            description  = tour.Description;
            status = tour.Status;
            maxTourists = tour.MaxTourists;
            date = tour.Date;
            checkpoints = tour.Checkpoints;
            pictures = tour.Pictures;
            country = tour.Country;

        }

        public Tour ToTour()
        {
            Tour tour = new Tour(name, city, country, description, language, maxTourists, checkpoints, date, duration, pictures);
            tour.Id = id;
            tour.Status = status;
            tour.GroupId = groupId;
            return tour;
        }

    }


}
