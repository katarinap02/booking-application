using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BookingApp.DTO
{
     public class HostDTO: INotifyPropertyChanged
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

        public HostDTO() { }

        public HostDTO(Host host)
        {
            userName = host.Username;
            password = host.Password;
            isSuperHost = host.IsSuperHost;
        }

        public Host ToHost()
        {
            Host host = new Host(userName, password, IsSuperHost);
            return host;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
