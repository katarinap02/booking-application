using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourRequestDTOViewModel
    {
        private string _city;
        private string _country;
        private int _id;
        private string _description;
        private TourRequestStatus _status;
        private string _language;
        private List<int> _participantIds;
        private DateTime _startDate;
        private DateTime _endDate;
        private DateTime _acceptedDate;
        private int _guideId;
        private DateTime _dateRequested;

        public int ParticipantNumber
        {
            get => _participantIds.Count;
        }
        public string City
        {
            get { return _city; }
            set { _city = value; NotifyPropertyChanged(nameof(City)); }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; NotifyPropertyChanged(nameof(Country)); }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; NotifyPropertyChanged(nameof(Id)); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; NotifyPropertyChanged(nameof(Description)); }
        }

        public TourRequestStatus Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged(nameof(Status)); }
        }

        public string Language
        {
            get { return _language; }
            set { _language = value; NotifyPropertyChanged(nameof(Language)); }
        }

        public List<int> ParticipantIds
        {
            get { return _participantIds; }
            set { _participantIds = value; NotifyPropertyChanged(nameof(ParticipantIds)); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; NotifyPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; NotifyPropertyChanged(nameof(EndDate)); }
        }

        public DateTime AcceptedDate
        {
            get { return _acceptedDate; }
            set { _acceptedDate = value; NotifyPropertyChanged(nameof(AcceptedDate)); }
        }

        public int GuideId
        {
            get { return _guideId; }
            set { _guideId = value; NotifyPropertyChanged(nameof(GuideId)); }
        }

        public DateTime DateRequested
        {
            get { return _dateRequested; }
            set { _dateRequested = value; NotifyPropertyChanged(nameof(DateRequested)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourRequestDTOViewModel() { }

        public TourRequestDTOViewModel(DateTime endDate) { 
            EndDate = endDate;
        }

        public TourRequestDTOViewModel(TourRequest tourRequest)
        {
            this.Id = tourRequest.Id;
            this.EndDate = tourRequest.EndDate;
            this.AcceptedDate = tourRequest.AcceptedDate;
            this.Language = tourRequest.Language;
            this.Description = tourRequest.Description;
            this.DateRequested = tourRequest.DateRequested;
            this.City = tourRequest.City;
            this.Country = tourRequest.Country;
            this.GuideId = tourRequest.GuideId;
            this.Status = tourRequest.Status;
            this.StartDate = tourRequest.StartDate;
            this.ParticipantIds = tourRequest.ParticipantIds;
        }
        public TourRequest ToTourRequest()
        {
            return new TourRequest
            {
                Id = this.Id,
                EndDate = this.EndDate,
                AcceptedDate = this.AcceptedDate,
                Language = this.Language,
                Description = this.Description,
                DateRequested = this.DateRequested,
                City = this.City,
                Country = this.Country,
                GuideId = this.GuideId,
                Status = this.Status,
                StartDate = this.StartDate,
                ParticipantIds = this.ParticipantIds
            };
        }
    }
}
