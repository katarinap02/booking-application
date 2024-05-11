using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        #region Polja

        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;                
            }

            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get
            {
                return _caption;
            }

            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        #endregion


        public ICommand ShowTodaysToursCommand { get; }
        public ICommand MyToursCommand { get; }
        public ICommand UserInfoCommand { get; }
        public ICommand TakePartCommand { get; }
        public ICommand StatsCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand RequestsCommand { get; }
        public ICommand ReportCommand { get; }
        public ICommand AppInfoCommand { get; }

        public MainViewModel()
        {
            
            ShowTodaysToursCommand = new ViewModelCommand(ExecuteShowTodaysToursViewCommand);
            MyToursCommand = new ViewModelCommand(ExecuteShowMyToursViewCommand);
            UserInfoCommand = new ViewModelCommand(ExecuteShowUserInfoCommand);
            TakePartCommand = new ViewModelCommand(ExecuteShowTakePartInViewCommand);
            StatsCommand = new ViewModelCommand(ExecuteShowStatisticsViewCommand);
            LogOutCommand = new ViewModelCommand(ExecuteShowLogoutViewCommand);
            RequestsCommand = new ViewModelCommand(ExecuteShowTourRequestsViewCommand);
            ReportCommand = new ViewModelCommand(ExecuteShowReportCommand);
            AppInfoCommand = new ViewModelCommand(ExecuteShowAppInfoCommand);

            CurrentChildView = new TodaysToursViewModel();
            Caption = "Todays Tours";
            Icon = IconChar.CalendarDay;

        }

        #region ImplementacijaKomandi

        private void ExecuteShowAppInfoCommand(object obj)
        {
            CurrentChildView = new GuideAppInformationViewModel();
            Icon = IconChar.InfoCircle;
            Caption = "Application info";
        }
        private void ExecuteShowReportCommand(object obj)
        {
            CurrentChildView = new GuideReportViewModel();
            Icon = IconChar.FilePdf;
            Caption = "PDF report";
        }
        private void ExecuteShowTourRequestsViewCommand(object obj)
        {
            CurrentChildView = new TourRequestsViewModel();
            Caption = "Tour Requests";
            Icon = IconChar.EnvelopeOpenText;
        }

        private void ExecuteShowTodaysToursViewCommand(object obj)
        {
            CurrentChildView = new TodaysToursViewModel(); 
            Caption = "Todays Tours";
            Icon = IconChar.CalendarDay; 
        }

        private void ExecuteShowMyToursViewCommand(object obj)
        {
            CurrentChildView = new MyToursViewModel(); 
            Caption = "My Tours";
            Icon = IconChar.Route;
        }

        private void ExecuteShowUserInfoCommand(object obj)
        {
            CurrentChildView = new UserInfoViewModel();
            Caption = "User information";
            Icon = IconChar.Route;
        }

        private void ExecuteShowTakePartInViewCommand(object obj)
        {
            CurrentChildView = new TakePartInViewModel();
            Caption = "Take Part in a Tour";
            Icon = IconChar.Signature;
        }

        private void ExecuteShowStatisticsViewCommand(object obj)
        {
            CurrentChildView = new StatisticsViewModel();
            Caption = "Statistics";
            Icon = IconChar.ChartPie;
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
            // Implement logout functionality here
        }

        #endregion
    }

}
