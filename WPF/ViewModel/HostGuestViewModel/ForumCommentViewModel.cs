using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
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
            get { return isSpecial; }
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
            get { return isHost; }
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

        private int reports;
        public int Reports
        {
            get { return reports; }
            set
            {
                if (reports != value)
                {
                    reports = value;
                    OnPropertyChanged("Reports");
                }
            }
        }

        private bool isReported;
        public bool IsReported
        {
            get { return isReported; }
            set
            {
                if (isReported != value)
                {
                    isReported = value;
                    OnPropertyChanged("IsReported");
                }
            }

        }



        public ForumService forumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        public string DateString => Date.ToString("MM/dd/yyyy");
        public String Location => forumService.GetById(ForumId).City + ", "+ forumService.GetById(ForumId).Country;

        

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
            reports = forumComment.Reports;
            isReported = forumComment.IsReported;
        }

        public ForumComment ToForum()
        {
            ForumComment f = new ForumComment(userId, comment, isSpecial, isHost, date, forumId, reports, isReported);
            f.Id = id;

            return f;
        }

    }
}
