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

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields        
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;


        //Properties


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

        //--> Commands
        public ICommand ShowTodaysToursCommand { get; }
        public ICommand ShowMyToursCommand { get; }

        public MainViewModel()
        {
            //CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowTodaysToursCommand = new ViewModelCommand(ExecuteShowTodaysToursViewCommand);

            ShowMyToursCommand = new ViewModelCommand(ExecuteShowMyToursViewCommand);

            //Default view
            CurrentChildView = new TodaysToursViewModel();
            Caption = "Todays Tours";
            Icon = IconChar.CalendarDay;

        }
        
        private void ExecuteShowTodaysToursViewCommand(object obj)
        {
            CurrentChildView = new TodaysToursViewModel(); // breakpoint
            Caption = "Todays Tours";
            Icon = IconChar.CalendarDay; // za svaki izmeniti Ikonu
            MessageBox.Show("ExecuteShowTodaysToursViewCommand");
        }

        private void ExecuteShowMyToursViewCommand(object obj)
        {
            CurrentChildView = new MyToursViewModel(); // breakpoint
            Caption = "My Tours";
            Icon = IconChar.Route;
            MessageBox.Show("ExecuteShowMyToursViewCommand");
        }
    } 

}
