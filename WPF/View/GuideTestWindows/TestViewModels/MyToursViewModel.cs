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

        public ICommand ShowUpcoming { get; }
        public ICommand ShowPast { get; }

        public MyToursViewModel(int id) {
            GuideId = id;
            ShowUpcoming = new ViewModelCommand(ExecuteShowUpcoming);
            ShowPast = new ViewModelCommand(ExecuteShowPast);
            ExecuteShowUpcoming(null);
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
