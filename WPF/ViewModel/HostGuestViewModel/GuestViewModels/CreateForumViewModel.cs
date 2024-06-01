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
using System.Windows.Controls;

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
                    SaveCommand.RaiseCanExecuteChanged();
                }
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
                    SaveCommand.RaiseCanExecuteChanged();
                }
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
                    SaveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public ForumService ForumService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ForumViewModel NewForum { get; set; }

        // KOMANDE
        public GuestICommand SaveCommand { get; set; }
        public CreateForumViewModel(User user, Frame frame) { 
            
            User = user;
            Frame = frame;
            SaveCommand = new GuestICommand(OnSave, CanSave);
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            
        
        }

        private bool CanSave()
        {
            if(string.IsNullOrEmpty(FirstComment) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(Country))
            {
                return false;
            }
            else
            {
                return true;
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
