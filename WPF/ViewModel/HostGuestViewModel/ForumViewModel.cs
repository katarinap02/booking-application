using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model.Features;
namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class ForumViewModel : INotifyPropertyChanged
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

        private List<int> comments = new List<int>();
        public List<int> Comments
        {
            get { return comments; }
            set
            {
                if (comments != value)
                {
                    comments = value;
                    OnPropertyChanged("Comments");
                }
            }
        }

        private bool isClosed;
        public bool IsClosed
        {
            get { return IsClosed; }
            set
            {
                if(isClosed != value)
                {
                    isClosed = value;
                    OnPropertyChanged("IsClosed");
                }
            }

        }

        private bool isVeryUseful;
        public bool IsVeryUseful
        {
            get { return IsVeryUseful; }
            set
            {
                if (isVeryUseful != value)
                {
                    isVeryUseful = value;
                    OnPropertyChanged("IsVeryUseful");
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
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ForumViewModel()
        {

        }
       public ForumViewModel(Forum forum)
        {
            id = forum.Id;
            userId = forum.UserId;
            city = forum.City;
            country = forum.Country;
            firstComment = forum.FirstComment;
            comments = forum.Comments;
            isClosed = forum.IsClosed;
            isVeryUseful = forum.IsVeryUseful;
            date = forum.Date;
        }

        public Forum ToForum()
        {
            Forum f = new Forum(userId, city, country, firstComment, isClosed, isVeryUseful, date);
            f.Id = id;
            f.Comments = comments;

            return f;
        }
    }
}
