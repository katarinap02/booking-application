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

                    country = value;
                    OnPropertyChanged("FirstComment");
                }
            }
        }

        private List<string> comments = new List<string>();
        public List<string> Comments
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

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       public ForumViewModel(GuestForum forum)
        {
            id = forum.Id;
            city = forum.City;
            country = forum.Country;
            firstComment = forum.FirstComment;
            comments = forum.Comments;
        }

        public GuestForum ToForum()
        {
            GuestForum f = new GuestForum(city, country, firstComment);
            f.Id = id;
            f.Comments = comments;

            return f;
        }
    }
}
