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

            ExecuteShowTodaysToursViewCommand(null);

        }

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
           

        }

        #endregion
    }

}
