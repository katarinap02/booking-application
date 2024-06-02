using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class GuideQuitWindowViewModel : ViewModelBase
    { 
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(_password));
            }
        }

        private string _typedPassword;
        public string TypedPassword
        {
            get => _typedPassword;
            set
            {
                _typedPassword = value;
                OnPropertyChanged(nameof(TypedPassword));
            }
        }
        private int GuideID;
        private readonly UserService _userService;
        private readonly GuideInfoService _infoService;
        public MyICommand Quit {  get; set; }
        public GuideQuitWindowViewModel(int guide_id) { 
            GuideID = guide_id;
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            _infoService = new GuideInfoService();
            Password = _userService.GetById(guide_id).Password;
            Quit = new MyICommand(ExecuteQuitting);
        }

        public void ExecuteQuitting() {
            if(_typedPassword == _password)
            {
                _infoService.Quit(GuideID);
                MessageBox.Show("Thank you for using this application. The app will shut down immediately after closing this window.", "Quitting successfull", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Wrong password");
            }
            
        }
    }
}
