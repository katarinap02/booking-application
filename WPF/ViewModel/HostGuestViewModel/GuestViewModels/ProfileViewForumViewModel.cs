using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ProfileViewForumViewModel : INotifyPropertyChanged, IObserver
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public ForumViewModel SelectedForum { get; set; }

        public string Username => User.Username;
        public string UserType => User.Type.ToString();

        public ForumComment ForumComment { get; set; }

        public ForumCommentService ForumCommentService { get; set; }

        public ForumService ForumService { get; set; }

        public ObservableCollection<ForumCommentViewModel> ForumComments { get; set; }
        

        

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
                PostCommentCommand.RaiseCanExecuteChanged();
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
            PostCommentCommand = new GuestICommand(OnPostComment, CanPost);
            ForumCommentService = new ForumCommentService(Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            ForumComments = new ObservableCollection<ForumCommentViewModel>();
            Update();
            
        }

        private bool CanPost()
        {
            if(string.IsNullOrEmpty(Comment))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnPostComment()
        {
            if (SelectedForum.IsClosed)
                MessageBox.Show("This forum is closed");
            else
            {
                ForumComment = new ForumComment();
                ForumComment = ForumCommentService.CreateComment(User.Id, Comment, SelectedForum.City, SelectedForum.Country, SelectedForum.Id);
                SelectedForum.Comments.Add(ForumComment.Id);
                ForumService.Update(SelectedForum.ToForum());

                Update();
                Comment = "";
            }
           


        }

        public void Update()
        {
            ForumComments.Clear();
            List<ForumCommentViewModel> tmpComments = new List<ForumCommentViewModel>();
            foreach(ForumComment comment in ForumCommentService.GetAll())
            {
                if (comment.ForumId == SelectedForum.Id)
                    tmpComments.Add(new ForumCommentViewModel(comment));
            }

            tmpComments = tmpComments.OrderByDescending(x => x.Date).ToList();
            foreach (ForumCommentViewModel comment in tmpComments)
                ForumComments.Add(comment);

        }
    }
}
