using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class ForumCommentViewModel : INotifyPropertyChanged
    {

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

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
        private bool isSpecial;
        public bool IsSpecial
        {
            get { return IsSpecial; }
            set
            {
                if (isSpecial != value)
                {
                    isSpecial = value;
                    OnPropertyChanged("IsVeryUseful");
                }
            }

        }

        private bool isHost;
        public bool IsHost
        {
            get { return IsHost; }
            set
            {
                if (isHost != value)
                {
                    isHost = value;
                    OnPropertyChanged("IsHost");
                }
            }

        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {

                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        public string DateString => Date.ToString("MM/dd/yyyy");

        private int forumId;
        public int ForumId
        {
            get { return forumId; }
            set
            {
                if (forumId != value)
                {
                    id = value;
                    OnPropertyChanged("ForumId");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public UserService userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
        public string Username => userService.GetById(UserId).Username;
        public string UserType => userService.GetById(UserId).Type.ToString();
        public ForumCommentViewModel(ForumComment forumComment)
        {
            id = forumComment.Id;
            userId = forumComment.UserId;
            comment = forumComment.Comment;
            isSpecial = forumComment.IsSpecial;
            isHost = forumComment.IsHost;
            date = forumComment.Date;
            forumId = forumComment.ForumId;
        }

        public ForumComment ToForum()
        {
            ForumComment f = new ForumComment(userId, comment, isSpecial, isHost, date, forumId);
            f.Id = id;

            return f;
        }

    }
}
