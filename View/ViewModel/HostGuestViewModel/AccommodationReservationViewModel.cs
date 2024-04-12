using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.View.GuestPages;
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

namespace BookingApp.View.ViewModel
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

        public string Location => City + ", " + Country;
        public string DateRangeString => StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");

        private AccommodationService accommodationService = new AccommodationService();
        private HostService hostService = new HostService();
        public string AccommodationDetails => Name + ", " + Location;


        public string HostUsername => GetHostUsername(hostService, accommodationService.GetById(AccommodationId).HostId);

        private string GetHostUsername(HostService hostService, int hostId)
        {
            return hostService.GetById(hostId).Username;
        }

        public int NumberOfDays => (EndDate - StartDate).Days + 1;

        public AccommodationViewModel SelectedAccommodation { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }
        public User User { get; set; }
        public int DayNumber { get; set; }
        public Frame Frame { get; set; }
        public ReservationInfoPage ReservationInfoPage { get; set; }
       
       

        public AccommodationReservationViewModel() { }

        public AccommodationReservationViewModel(AccommodationViewModel selectedAccommodation, User user, Frame frame, ReservationInfoPage reservationInfoPage) {

            SelectedAccommodation = selectedAccommodation;
            AccommodationService = new AccommodationService();
            ReservationInfoPage = reservationInfoPage;
            User = user;
            Frame = frame;
            AccommodationReservationService = new AccommodationReservationService();
            
        }

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

        public void Continue_Click(object sender, RoutedEventArgs e)
        {




            DayNumber = Convert.ToInt32(ReservationInfoPage.txtDayNumber.Text);
            DateTime start = Convert.ToDateTime(ReservationInfoPage.txtStartDate.Text);
            DateTime end = Convert.ToDateTime(ReservationInfoPage.txtEndDate.Text);


            Frame.Content = new CalendarPage(AccommodationService, AccommodationReservationService, SelectedAccommodation, DayNumber, User, start, end, Frame);


        }

        private bool ValidateDayNumber(int dayNumber)
        {
            if (DayNumber < SelectedAccommodation.MinReservationDays)
            {

                return false;
            }
            else
            {

                return true;
            }
        }

        private bool ValidateDateInputs(DateTime start, DateTime end)
        {
            if (start >= end)
            {

                return false;
            }
            else
            {

                return true;
            }


        }

        public void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            
         
            AccommodationService = new AccommodationService();
            AccommodationReservationService = new AccommodationReservationService();
            AccommodationReservationService.CancelReservation(AccommodationService, this);
          

        }

       
    }
}
