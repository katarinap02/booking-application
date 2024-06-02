using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model;
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
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class CreateForumViewModel : INotifyPropertyChanged
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {

                    city = value;
                    OnPropertyChanged("City");

                }
                SaveCommand.RaiseCanExecuteChanged();
            }
               
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {

                    country = value;
                    OnPropertyChanged("Country");
                   
                }
                SaveCommand.RaiseCanExecuteChanged();
               
            }
        }

        private string firstComment;
        public string FirstComment
        {
            get { return firstComment; }
            set
            {
                if (firstComment != value)
                {

                    firstComment = value;
                    OnPropertyChanged("FirstComment");
                   
                }
                SaveCommand.RaiseCanExecuteChanged();
               
            }
        }

        public ForumService ForumService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ForumViewModel NewForum { get; set; }
        public CreateForumPage Page { get; set; }

        // KOMANDE
        public GuestICommand SaveCommand { get; set; }
        public CreateForumViewModel(User user, Frame frame, CreateForumPage page) { 
            
            User = user;
            Frame = frame;
            SaveCommand = new GuestICommand(OnSave, CanSave);
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Page = page;
        
        }

        private bool CanSave()
        {
            
           ToggleCountryValidationMessages();
            ToggleCityValidationMessages();
            ToggleCommentValidationMessages();
           

            if (string.IsNullOrEmpty(FirstComment) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Country))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ToggleCountryValidationMessages()
        {
            if (string.IsNullOrEmpty(Country))
            {

                Page.countryValidator.Visibility = Visibility.Visible;
              
                Page.txtCountry.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtCountry.BorderThickness = new Thickness(2);
            }
            else
            {
                Page.countryValidator.Visibility = Visibility.Hidden;
              
                Page.txtCountry.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtCountry.BorderThickness = new Thickness(1);

            }
        }

        private void ToggleCityValidationMessages()
        {
            if (string.IsNullOrEmpty(City))
            {

                Page.cityValidator.Visibility = Visibility.Visible;
              
                Page.txtCity.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtCity.BorderThickness = new Thickness(2);
            }
            else
            {
                Page.cityValidator.Visibility = Visibility.Hidden;
               
                Page.txtCity.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtCity.BorderThickness = new Thickness(1);

            }
        }

            private void ToggleCommentValidationMessages()
        {
            if (string.IsNullOrEmpty(FirstComment))
            {

                Page.commentValidator.Visibility = Visibility.Visible;
               

                Page.txtComment.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtComment.BorderThickness = new Thickness(2);
            }
            else
            {
                Page.commentValidator.Visibility = Visibility.Hidden;
               
                Page.txtComment.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtComment.BorderThickness = new Thickness(1);
            }
        }

        private void OnSave()
        {
            Forum forum = new Forum(User.Id, City, Country, FirstComment, false, false, DateTime.Now);
            
            ForumService.Add(forum);
            NewForum = new ForumViewModel(forum);
            Frame.Content = new CreateForumSuccessfulPage(User, Frame, NewForum);
           

        }
    }
}
