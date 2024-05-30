using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TouristViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value != _lastName)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value != _age)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private bool _hasConqueredVoucher;
        public bool HasConqueredVoucher
        {
            get
            {
                return _hasConqueredVoucher;
            }
            set
            {
                if (_hasConqueredVoucher != value)
                {
                    _hasConqueredVoucher = value;
                    OnPropertyChanged(nameof(HasConqueredVoucher));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TouristViewModel() { }
        public TouristViewModel(Tourist tourist)
        {
            UserName = tourist.Username;
            Password = tourist.Password;
            Age = tourist.Age;
            LastName = tourist.LastName;
            Name = tourist.Name;
            Id = tourist.Id;
        }

        public Tourist ToTourist()
        {
            Tourist tourist = new Tourist(Id, Name, LastName, Age, UserName, Password, HasConqueredVoucher);
            return tourist;
        }
    }
}
