using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository;
using BookingApp.Repository.FeatureRepository;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using FontAwesome.Sharp;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
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

        private int GuideId;

        private readonly GuidedTourRepository guidedTourRepository = new GuidedTourRepository();
        private readonly TourService tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());

        public ICommand ShowTodaysToursCommand { get; }
        public ICommand MyToursCommand { get; }
        public ICommand UserInfoCommand { get; }
        public ICommand TakePartCommand { get; }
        public ICommand StatsCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand RequestsCommand { get; }
        public ICommand ReportCommand { get; }
        public ICommand AppInfoCommand { get; }

        public ICommand CtrlUCommand { get; }
        public ICommand CtrlTCommand { get; }
        public ICommand CtrlMCommand { get; }
        public ICommand CtrlSCommand { get; }
        public ICommand CtrlPCommand { get; }
        public ICommand CtrlRCommand { get; }
        public ICommand CtrlICommand { get; }
        public ICommand CtrlNCommand { get; }

        public ICommand Shortcuts { get; }

        #region Klikovi
        private bool _isUserInfoClicked;
        private bool _isShowTodaysToursClicked;
        private bool _isMyToursClicked;
        private bool _isRequestsClicked;
        private bool _isTakePartClicked;
        private bool _isStatsClicked;
        private bool _isAppInfoClicked;

        public bool IsUserInfoClicked
        {
            get { return _isUserInfoClicked; }
            set
            {
                if (_isUserInfoClicked != value)
                {
                    _isUserInfoClicked = value;
                    OnPropertyChanged(nameof(IsUserInfoClicked));
                    ExecuteShowUserInfoCommand(null);
                }
            }
        }

        public bool IsShowTodaysToursClicked
        {
            get { return _isShowTodaysToursClicked; }
            set
            {
                if (_isShowTodaysToursClicked != value)
                {
                    _isShowTodaysToursClicked = value;
                    OnPropertyChanged(nameof(IsShowTodaysToursClicked));
                    ExecuteShowTodaysToursViewCommand(null);
                }
            }
        }

        public bool IsMyToursClicked
        {
            get { return _isMyToursClicked; }
            set
            {
                if (_isMyToursClicked != value)
                {
                    _isMyToursClicked = value;
                    OnPropertyChanged(nameof(IsMyToursClicked));
                    ExecuteShowMyToursViewCommand(null);
                }
            }
        }

        public bool IsRequestsClicked
        {
            get { return _isRequestsClicked; }
            set
            {
                if (_isRequestsClicked != value)
                {
                    _isRequestsClicked = value;
                    OnPropertyChanged(nameof(IsRequestsClicked));
                    ExecuteShowTourRequestsViewCommand(null);
                }
            }
        }

        public bool IsTakePartClicked
        {
            get { return _isTakePartClicked; }
            set
            {
                if (_isTakePartClicked != value)
                {
                    _isTakePartClicked = value;
                    OnPropertyChanged(nameof(IsTakePartClicked));
                    ExecuteShowTakePartInViewCommand(null);
                }
            }
        }

        public bool IsStatsClicked
        {
            get { return _isStatsClicked; }
            set
            {
                if (_isStatsClicked != value)
                {
                    _isStatsClicked = value;
                    OnPropertyChanged(nameof(IsStatsClicked));
                    ExecuteShowStatisticsViewCommand(null);
                }
            }
        }

        public bool IsAppInfoClicked
        {
            get { return _isAppInfoClicked; }
            set
            {
                if (_isAppInfoClicked != value)
                {
                    _isAppInfoClicked = value;
                    OnPropertyChanged(nameof(IsAppInfoClicked));
                    ExecuteShowAppInfoCommand(null);
                }
            }
        }
        #endregion

        public MainViewModel(int id)
        {
            GuideId = id;

            ShowTodaysToursCommand = new ViewModelCommand(ExecuteShowTodaysToursViewCommand);
            MyToursCommand = new ViewModelCommand(ExecuteShowMyToursViewCommand);
            UserInfoCommand = new ViewModelCommand(ExecuteShowUserInfoCommand);
            TakePartCommand = new ViewModelCommand(ExecuteShowTakePartInViewCommand);
            StatsCommand = new ViewModelCommand(ExecuteShowStatisticsViewCommand);
            LogOutCommand = new ViewModelCommand(ExecuteShowLogoutViewCommand);
            RequestsCommand = new ViewModelCommand(ExecuteShowTourRequestsViewCommand);
            ReportCommand = new ViewModelCommand(ExecuteShowReportCommand);
            AppInfoCommand = new ViewModelCommand(ExecuteShowAppInfoCommand);

            CtrlUCommand = new ViewModelCommand(ExecuteCtrlUCommand);
            CtrlTCommand = new ViewModelCommand(ExecuteCtrlTCommand);
            CtrlMCommand = new ViewModelCommand(ExecuteCtrlMCommand);
            CtrlSCommand = new ViewModelCommand(ExecuteCtrlSCommand);
            CtrlPCommand = new ViewModelCommand(ExecuteCtrlPCommand);
            CtrlRCommand = new ViewModelCommand(ExecuteCtrlRCommand);
            CtrlICommand = new ViewModelCommand(ExecuteCtrlICommand);
            CtrlNCommand = new ViewModelCommand(ExecuteCtrlNCommand);
            Shortcuts = new ViewModelCommand(Shortcuts1);


            ExecuteShowTodaysToursViewCommand(null);
            IsShowTodaysToursClicked = true;

        }

        #region Precice
        public void ResetRadioButtonStates()
        {
            IsUserInfoClicked = false;
            IsShowTodaysToursClicked = false;
            IsMyToursClicked = false;
            IsRequestsClicked = false;
            IsTakePartClicked = false;
            IsStatsClicked = false;
            IsAppInfoClicked = false;
        }

        private void Shortcuts1(object o)
        {
            ShortcutsWindpw shortcutsWindpw = new ShortcutsWindpw();
            shortcutsWindpw.Show();
        }
        private void ExecuteCtrlUCommand(object obj)
        {
            ResetRadioButtonStates();
            IsUserInfoClicked = true;
        }

        private void ExecuteCtrlTCommand(object o)
        {
            ResetRadioButtonStates();
            IsShowTodaysToursClicked = true;
        }

        private void ExecuteCtrlMCommand(object o)
        {
            ResetRadioButtonStates();
            IsMyToursClicked = true;
        }

        private void ExecuteCtrlSCommand(object o)
        {
            ResetRadioButtonStates();
            IsStatsClicked = true;
        }

        private void ExecuteCtrlPCommand(object o)
        {
            ResetRadioButtonStates();
            IsTakePartClicked = true;
        }
        private void ExecuteCtrlRCommand(object o)
        {
            ResetRadioButtonStates();
            IsRequestsClicked = true;
        }
        private void ExecuteCtrlICommand(object o)
        {
            ResetRadioButtonStates();
            IsAppInfoClicked = true;
        }

        private void ExecuteCtrlNCommand(object o)
        {
            AddingTourWindow addingTourWindow = new AddingTourWindow(GuideId);
            addingTourWindow.Show();
        }
        #endregion

        private void HandleStartTourRequested(object sender, TourViewModel tour)
        {
            var startedTourView = new StartedTourViewModel(tour);
            startedTourView.FinnishTourEvent += HandleFinnishTourRequested;
            CurrentChildView = startedTourView;
        }
        
        private void HandleFinnishTourRequested(object sender, EventArgs e)
        {
            ExecuteShowTodaysToursViewCommand(null);
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
            CurrentChildView = new TourRequestsViewModel(GuideId);
            Caption = "Tour Requests";
            Icon = IconChar.EnvelopeOpenText;
        }

        private void ExecuteShowTodaysToursViewCommand(object obj)
        {
            if (guidedTourRepository.HasTourCurrently(GuideId))
            {
                HasTour();
            }
            else
            {
                NoTour();
            }
            
            Caption = "Todays Tours";
            Icon = IconChar.CalendarDay;
        }

        private void NoTour()
        {
            var todaysToursViewModel = new TodaysToursViewModel(GuideId);
            todaysToursViewModel.StartTourRequested += (sender, tour) => HandleStartTourRequested(sender, tour);
            CurrentChildView = todaysToursViewModel;
        }

        private void HasTour()
        {
            Tour tour = tourService.GetTourById(guidedTourRepository.FindTourIdByGuide(GuideId));
            var startedTourView = new StartedTourViewModel(new TourViewModel(tour));
            startedTourView.FinnishTourEvent += HandleFinnishTourRequested;
            CurrentChildView = startedTourView;
        }

        private void ExecuteShowMyToursViewCommand(object obj)
        {
            CurrentChildView = new MyToursViewModel(GuideId); 
            Caption = "My Tours";
            Icon = IconChar.Route;
        }

        private void ExecuteShowUserInfoCommand(object obj)
        {
            CurrentChildView = new UserInfoViewModel(GuideId);
            Caption = "User information";
            Icon = IconChar.Route;
        }

        private void ExecuteShowTakePartInViewCommand(object obj)
        {
            CurrentChildView = new TakePartInViewModel(GuideId);
            Caption = "Take Part in a Tour";
            Icon = IconChar.Signature;
        }

        private void ExecuteShowStatisticsViewCommand(object obj)
        {
            CurrentChildView = new StatisticsViewModel(GuideId);
            Caption = "Statistics";
            Icon = IconChar.ChartPie;
        }

        private void ExecuteShowLogoutViewCommand(object obj)
        {
           

        }

        #endregion
    }

}
