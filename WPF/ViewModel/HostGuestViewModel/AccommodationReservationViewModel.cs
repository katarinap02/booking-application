using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using BookingApp.Repository;

using BookingApp.WPF.View.Guest.GuestTools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class AccommodationReservationViewModel : INotifyPropertyChanged
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

        private int guestId;
        public int GuestId
        {
            get { return guestId; }
            set
            {
                if (guestId != value)
                {
                    guestId = value;
                    OnPropertyChanged("GuestId");
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

        private int numberOfPeople;

        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            set
            {
                if (numberOfPeople != value)
                {

                    numberOfPeople = value;
                    OnPropertyChanged("NumberOfPeople");
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
                if (city != value)
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
                if (country != value)
                {

                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }


        private string onePicture;
        public string OnePicture
        {
            get { return onePicture; }
            set
            {
                if (onePicture != value)
                {

                    onePicture = value;
                    OnPropertyChanged("OnePicture");
                }
            }
        }



        private HostService hostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
        private AccommodationService accommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
        private UserService userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());

        public string AccommodationDetails => Name + ", " + City + ", " + Country;
        public string HostUsername => GetHostUsername(hostService, accommodationService.GetById(AccommodationId).HostId);
        public string DateRangeString => StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");

        //public string DateString => StartDate.ToString("MM/dd/yyyy") + " - " + EndDate.ToString("MM/dd/yyyy");

        public string GuestUsername => userService.GetById(GuestId).Username;

        public string AccommodationName => accommodationService.GetById(AccommodationId).Name;

        public Accommodation acc => accommodationService.GetById(AccommodationId);
        public PathConverter PathConverter { get; set; }

        private string GetHostUsername(HostService hostService, int hostId)
        {
            return hostService.GetById(hostId).Username;
        }

        public int NumberOfDays => (EndDate - StartDate).Days + 1;

        public AccommodationReservationViewModel() { }


        public AccommodationReservationViewModel(AccommodationReservation ac)
        {
            id = ac.Id;
            guestId = ac.GuestId;
            accommodationId = ac.AccommodationId;
            startDate = ac.StartDate;
            endDate = ac.EndDate;
            numberOfPeople = ac.NumberOfPeople;
            name = ac.Name;
            city = ac.City;
            country = ac.Country;
            PathConverter = new PathConverter();
            if (acc.Pictures.Count != 0)
                OnePicture = PathConverter.ConvertToRelativePathSecond(acc.Pictures[0]);

            else
                OnePicture = "../../Resources/Images/no_image.jpg";


        }

        public AccommodationReservation ToAccommodationReservation()
        {

            AccommodationReservation a = new AccommodationReservation(guestId, accommodationId, startDate, endDate, numberOfPeople, name, city, country);
            a.Id = id;

            return a;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
