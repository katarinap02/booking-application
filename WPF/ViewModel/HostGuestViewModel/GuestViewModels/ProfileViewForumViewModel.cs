using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
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
    public class ProfileViewForumViewModel : INotifyPropertyChanged
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public ForumViewModel SelectedForum { get; set; }

        public string Username => User.Username;
        public string UserType => User.Type.ToString();

        public ForumComment ForumComment { get; set; }

        public ForumCommentService ForumCommentService { get; set; }

        public ForumService ForumService { get; set; }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {

                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //KOMANDE
        public GuestICommand PostCommentCommand { get; set; }
        public ProfileViewForumViewModel(User user, Frame frame, ForumViewModel selectedForum)
        {
            User = user;
            Frame = frame;
            SelectedForum = selectedForum;
            PostCommentCommand = new GuestICommand(OnPostComment);
            ForumCommentService = new ForumCommentService(Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>());
            
        }

        private void OnPostComment()
        {
           ForumComment = new ForumComment();
            ForumComment = ForumCommentService.CreateComment(User.Id, Comment, SelectedForum.City, SelectedForum.Country);
           SelectedForum.Comments.Add(ForumComment.Id);
           ForumService.Update(SelectedForum.ToForum());

            MessageBox.Show("Komentar dodat");


        }
    }
}
