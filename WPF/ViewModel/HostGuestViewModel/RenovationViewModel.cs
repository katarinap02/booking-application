using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class RenovationViewModel : INotifyPropertyChanged
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

        private int hostId;
        public int HostId
        {
            get { return hostId; }
            set
            {
                if (hostId != value)
                {
                    hostId = value;
                    OnPropertyChanged("HostId");
                }
            }
        }


        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }
            set
            {
                if (accommodationId != value)
                {

                    accommodationId = value;
                    OnPropertyChanged("AccommodationId");
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {

                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {

                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        private int duration;
        public int Duration
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

        private AccommodationService accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());

        public string DateString => StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");
        public string AccommodationName => accommodationService.GetById(AccommodationId).Name;

        public AccommodationType Type => accommodationService.GetById(AccommodationId).Type;

        public string Location => accommodationService.GetById(AccommodationId).City + " " + accommodationService.GetById(AccommodationId).Country;


        public RenovationViewModel() { }

        public RenovationViewModel(Renovation ra)
        {
            id = ra.Id;
            hostId = ra.HostId;
            accommodationId = ra.AccommodationId;
            startDate = ra.StartDate;
            endDate = ra.EndDate;
            duration = ra.Duration;
            description = ra.Description;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Renovation ToReservation()
        {
            Renovation ra = new Renovation(accommodationId, hostId, startDate, endDate, duration, description);
            return ra;
        }
    }


}
