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

        private readonly UserService _userService;
        public MyICommand Quit {  get; set; }
        public GuideQuitWindowViewModel(int guide_id) { 
            _userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());

            Password = _userService.GetById(guide_id).Password;
            Quit = new MyICommand(ExecuteQuitting);
        }

        public void ExecuteQuitting() {
            //app shutdown
            if(_typedPassword == _password)
            {
                MessageBox.Show("Thank you for using this application. The app will shut down immediately after closing this window.", "Quitting successfull");
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Wrong password");
            }
            
        }
    }
}
