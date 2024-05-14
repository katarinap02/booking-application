using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
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
                }
            }
        }

        public ForumService ForumService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // KOMANDE
        public GuestICommand SaveCommand { get; set; }
        public CreateForumViewModel(User user, Frame frame) { 
            
            User = user;
            Frame = frame;
            SaveCommand = new GuestICommand(OnSave);
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>());
            
        
        }

        private void OnSave()
        {
            Forum forum = new Forum(City, Country, FirstComment, false);
            
            ForumService.Add(forum);
            MessageBox.Show("Forum added");

        }
    }
}
