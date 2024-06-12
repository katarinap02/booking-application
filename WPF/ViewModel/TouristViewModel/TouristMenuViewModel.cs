﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.TouristWindows;
using BookingApp.WPF.View.TouristPages;
using BookingApp.WPF.View.TouristWindows;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TouristMenuViewModel : INotifyPropertyChanged
    {
        public UserService _userService { get; set; }
        public VoucherService _voucherService { get; set; }
        public TourService _tourService { get; set; }

        public ICommand LogoutCommand { get; set; }

        private ICommand _allToursCommand;
        public ICommand AllToursCommand
        {
            get
            {
                if (_allToursCommand == null)
                {
                    _allToursCommand = new RelayCommand(param => AllToursInit());
                }
                return _allToursCommand;
            }
        }
        private void AllToursInit()
        {
            MainFrameContent = new AllToursPage(getUserId(UserName));
        }

        private ICommand _shortcutsCommand;
        public ICommand ShortcutsCommand
        {
            get
            {
                if (_shortcutsCommand == null)
                {
                    _shortcutsCommand = new RelayCommand(param => Shortcuts());
                }
                return _shortcutsCommand;
            }
        }

        private void Shortcuts()
        {
            if (IsShortcutsOpen)
            {
                IsShortcutsOpen = false;
                return;
            }
            IsShortcutsOpen = true;
        }

        private ICommand _myToursCommand;
        public ICommand MyToursCommand
        {
            get
            {
                if (_myToursCommand == null)
                {
                    _myToursCommand = new RelayCommand(param => MyToursInit());
                }
                return _myToursCommand;
            }
        }
        private void MyToursInit()
        {
            MainFrameContent = new MyToursPage(SelectedTour, getUserId(UserName));
        }

        private ICommand _requestedToursCommand;
        public ICommand RequestedToursCommand
        {
            get
            {
                if (_requestedToursCommand == null)
                {
                    _requestedToursCommand = new RelayCommand(param => RequestedToursInit());
                }
                return _requestedToursCommand;
            }
        }
        private void RequestedToursInit()
        {
            MainFrameContent = new RequestedToursPage(getUserId(UserName));
        }

        private ICommand _endedToursCommand;
        public ICommand EndedToursCommand
        {
            get
            {
                if (_endedToursCommand == null)
                {
                    _endedToursCommand = new RelayCommand(param => EndedToursInit());
                }
                return _endedToursCommand;
            }
        }
        private void EndedToursInit()
        {
            MainFrameContent = new EndedToursPage(getUserId(UserName));
        }

        private ICommand _vouchersCommand;
        public ICommand VouchersCommand
        {
            get
            {
                if (_vouchersCommand == null)
                {
                    _vouchersCommand = new RelayCommand(param => VouchersInit());
                }
                return _vouchersCommand;
            }
        }
        private void VouchersInit()
        {
            MainFrameContent = new VouchersPage(getUserId(UserName));
        }

        private bool _isShortcutsOpen;
        public bool IsShortcutsOpen
        {
            get
            {
                return _isShortcutsOpen;
            }
            set
            {
                if(_isShortcutsOpen != value)
                {
                    _isShortcutsOpen = value;
                    OnPropertyChanged(nameof(IsShortcutsOpen));
                }
            }
        }

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
                if (value != _mainFrameContent)
                {
                    _mainFrameContent = value;
                    OnPropertyChanged(nameof(MainFrameContent));
                }
            }
        }

        private void ExecuteLogoutCommand(object obj)
        {
            Messenger.Default.Send(new LogoutMessage());
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

        public void Initialize()
        {
            // initializing tour requests
            _userService.UpdateTourRequests();

            _voucherService.RefreshVouchers();

            // add vouchers to user
            if(_tourService.GetTourCountForLastYear(UserId) >= 5 && !_voucherService.isTouristConqueredVoucher(UserId))
            {
                _voucherService.AwardVoucher(UserId);
                InformationMessageBoxWindow mb = new InformationMessageBoxWindow("Congratulations on earning a voucher for attending 5 tours in the past year, valid for any tour over the next 6 months as a thank you for your loyalty!");
                mb.ShowDialog();
                //MessageBox.Show("Congratulations on earning a voucher for attending 5 tours in the past year, \nvalid for any tour over the next 6 months as a thank you for your loyalty!", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                //Messenger.Default.Send(new NotificationMessage("Congratulations on earning a voucher for attending 5 tours in the past year, valid for any tour over the next 6 months as a thank you for your loyalty!"));
            }
        }

        public TouristMenuViewModel(string username, int userId)
        {
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            _voucherService = new VoucherService(Injector.Injector.CreateInstance<IVoucherRepository>());
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());

            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);

            IsShortcutsOpen = false;
            UserName = username;
            UserId = userId;
            Initialize();
        }
    }
}
