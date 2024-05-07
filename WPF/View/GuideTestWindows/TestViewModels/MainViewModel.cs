using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand { get; }

        public MainViewModel()
        {
            //CurrentUserAccount = new UserAccountModel();

            //Initialize commands
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowMyToursViewCommand); // verovatno treba izmena

            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowTodaysToursViewCommand);

            //Default view
            ExecuteShowMyToursViewCommand(null);


        }

        private void ExecuteShowTodaysToursViewCommand(object obj)
        {
            CurrentChildView = new TodaysToursViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup; // za svaki izmeniti Ikonu
        }

        private void ExecuteShowMyToursViewCommand(object obj)
        {
            CurrentChildView = new MyToursViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }
    }

}
