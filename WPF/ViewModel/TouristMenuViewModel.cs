using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.TouristWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel
{
    public class TouristMenuViewModel : INotifyPropertyChanged
    {
        public UserService _userService { get; set; }
        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get
            {
                return _selectedTour;
            }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        private Page _mainFrameContent;
        public Page MainFrameContent
        {
            get
            {
                return _mainFrameContent;
            }
            set
            {
                if(value != _mainFrameContent)
                {
                    _mainFrameContent = value;
                    OnPropertyChanged(nameof(MainFrameContent));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int getUserId(string Username)
        {

            UserId = _userService.GetByUsername(Username).Id;
            return UserId;
        }

        public TouristMenuViewModel()
        {
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());

        }
    }
}
