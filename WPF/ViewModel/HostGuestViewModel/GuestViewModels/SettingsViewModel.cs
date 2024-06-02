using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class SettingsViewModel: INotifyPropertyChanged
    {
        public User User { get; set; }
        public GuestService GuestService { get; set; }
        public UserService UserService { get; set; }
        public Guest Guest { get; set; }
        private string newUsername;
        public string NewUsername
        {
            get { return newUsername; }
            set
            {
                if (newUsername != value)
                {

                    newUsername = value;
                    OnPropertyChanged("NewUsername");
                    
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {

                    password = value;
                    OnPropertyChanged("Password");
                   
                }
            }
        }

        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                if (oldPassword != value)
                {

                    oldPassword = value;
                    OnPropertyChanged("OldPassword");
                   
                }
            }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {

                    newPassword = value;
                    OnPropertyChanged("NewPassword");
                  
                }
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {

                    confirmPassword = value;
                    OnPropertyChanged("ConfirmPassword");
                    
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestICommand SaveCommand { get; set;  }
        public SettingsPage Page { get; set; }
        public SettingsViewModel(User user, SettingsPage page) { 
            User = user;
            GuestService = new GuestService(Injector.Injector.CreateInstance<IGuestRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            UserService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            SaveCommand =  new GuestICommand(OnSave);
            Guest = GuestService.GetById(User.Id);
            Page = page;
        
        }

        private bool CanSave()
        {
            if(CanSaveUsername() || CanSavePassword())
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }

        private bool CanSavePassword()
        {
            
            if(Page.oldPassword.Password.Equals(User.Password))
            {
                if (Page.confirmNewPassword.Password != null && Page.newPassword.Password == Page.confirmNewPassword.Password)
                {
                   
                    User.Password = Page.confirmNewPassword.Password;
                    Guest.Password = Page.confirmNewPassword.Password;
                    return true;
                }
                else
                    return false;
            }
            else if(Page.oldPassword.Password == "" && Page.newPassword.Password == "" && Page.confirmNewPassword.Password == "")
            {
                return true;
            }
            return false;
        }

        private bool CanSaveUsername()
        {
            if (!string.IsNullOrEmpty(NewUsername))
            {

                if (User.Password == Page.passwordCheck.Password)
                {
                  
                    User.Username = NewUsername;
                    Guest.Username = NewUsername;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(NewUsername))
                return true;

            return false;
        }

        private void OnSave()
        {
            if(Page.oldPassword.Password == "" && Page.newPassword.Password == "" && Page.confirmNewPassword.Password == "" && Page.passwordCheck.Password == "" && string.IsNullOrEmpty(NewUsername))
            {
                Page.SuccessMessage.Visibility = Visibility.Hidden;
                Page.ErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
              
                if (CanSaveUsername() && CanSavePassword())
                {
                    UserService.Update(User);
                    GuestService.Update(Guest);
                    Page.SuccessMessage.Visibility = Visibility.Visible;
                    Page.ErrorMessage.Visibility = Visibility.Hidden;
                    ResetInput();
                }
                else
                {
                    Page.SuccessMessage.Visibility = Visibility.Hidden;
                    Page.ErrorMessage.Visibility = Visibility.Visible;
                }
            }
            
         
        }

        private void ResetInput()
        {
            NewUsername = "";
            Page.oldPassword.Password = "";
            Page.newPassword.Password = "";
            Page.confirmNewPassword.Password = "";
            Page.passwordCheck.Password = "";
        }
    }
}
