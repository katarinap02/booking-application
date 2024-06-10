using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class MyToursViewModel : ViewModelBase
    {
        private int GuideId;

        private ViewModelBase _currentChildView;
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

        private bool _isUpcomingChecked = true;
        public bool IsUpcomingChecked
        {
            get { return _isUpcomingChecked; }
            set
            {
                if (_isUpcomingChecked != value)
                {
                    _isUpcomingChecked = value;
                    OnPropertyChanged(nameof(IsUpcomingChecked));
                    ExecuteShowUpcoming(null);
                }
            }
        }

        private bool _isPastChecked;
        public bool IsPastChecked
        {
            get { return _isPastChecked; }
            set
            {
                if (_isPastChecked != value)
                {
                    _isPastChecked = value;
                    OnPropertyChanged(nameof(IsPastChecked));
                    ExecuteShowPast(null);
                }
            }
        }

        public ICommand ShowUpcoming { get; }
        public ICommand ShowPast { get; }
        public ICommand Qcommand {get;}
        public ICommand Wcommand {get;}

        public MyToursViewModel(int id) {
            GuideId = id;
            ShowUpcoming = new ViewModelCommand(ExecuteShowUpcoming);
            ShowPast = new ViewModelCommand(ExecuteShowPast);
            Qcommand = new ViewModelCommand(Q);
            Wcommand = new ViewModelCommand(W);
            ExecuteShowUpcoming(null);
        }

        public void Q(object obj)
        {
            IsUpcomingChecked = true;
        }
        public void W(object obj)
        {
            IsPastChecked = true;
        }

        public void ExecuteShowUpcoming(object obj)
        {
            CurrentChildView = new UpcomingTourViewModel(GuideId);
        }

        public void ExecuteShowPast(object obj)
        {
            CurrentChildView = new PastToursViewModel(GuideId);
        }

    }
}
