using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class GuestViewModel : INotifyPropertyChanged
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

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {

                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {

                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        private bool isSuperGuest;
        public bool IsSuperGuest
        {
            get { return isSuperGuest; }
            set
            {
                if (isSuperGuest != value)
                {

                    isSuperGuest = value;
                    OnPropertyChanged("IsSuperHost");
                }
            }
        }


        private int yearlyReservations;
        public int YearlyReservations
        {
            get { return yearlyReservations; }
            set
            {
                if (yearlyReservations != value)
                {

                    yearlyReservations = value;
                    OnPropertyChanged("YearlyReservations");
                }
            }
        }

        private int bonusPoints;
        public int BonusPoints
        {
            get { return bonusPoints; }
            set
            {
                if (bonusPoints != value)
                {

                    yearlyReservations = value;
                    OnPropertyChanged("BonusPoints");
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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestViewModel(Guest g)
        {
            Username = g.Username;
            Password = g.Password;
            YearlyReservations = g.YearlyReservations;
            IsSuperGuest = g.IsSuperGuest;
            BonusPoints = g.BonusPoints;
            StartDate = g.StartDate;
            EndDate = g.EndDate;

        }

        public Guest ToGuest()
        {
            Guest guest = new Guest(username, password, yearlyReservations, startDate, endDate, bonusPoints, isSuperGuest);
            guest.Id = Id;
            return guest;
        }
    }
}
