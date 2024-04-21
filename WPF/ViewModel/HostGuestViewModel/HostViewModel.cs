using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class HostViewModel : INotifyPropertyChanged
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

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {

                    userName = value;
                    OnPropertyChanged("UserName");
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

        private bool isSuperHost;
        public bool IsSuperHost
        {
            get { return isSuperHost; }
            set
            {
                if (isSuperHost != value)
                {

                    isSuperHost = value;
                    OnPropertyChanged("IsSuperHost");
                }
            }
        }

        private double rateAverage;
        public double RateAverage
        {
            get { return rateAverage; }
            set
            {
                if (rateAverage != value)
                {

                    rateAverage = value;
                    OnPropertyChanged("RateAverage");
                }
            }
        }

        private int rateCount;
        public int RateCount
        {
            get { return rateCount; }
            set
            {
                if (rateCount != value)
                {

                    rateCount = value;
                    OnPropertyChanged("RateCount");
                }
            }
        }

        public HostViewModel() { }

        public HostViewModel(Host host)
        {
            userName = host.Username;
            password = host.Password;
            isSuperHost = host.IsSuperHost;
            rateAverage = host.RateAverage;
            rateCount = host.RateCount;
        }

        public Host ToHost()
        {
            Host host = new Host(userName, password, IsSuperHost, rateAverage, rateCount);
            return host;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
