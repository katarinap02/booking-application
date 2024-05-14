using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public class GuestForum : ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstComment { get; set; }

        public List<string> Comments { get; set; }

        public GuestForum() { 
             Comments = new List<string>(); 
        
        }
        public GuestForum(string city, string country, string firstComment)
        {
            City = city;
            Country = country;
            FirstComment = firstComment;
            Comments = new List<string>();

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), City, Country, FirstComment, MakeCommentsList(Comments) };
            return csvValues;
        }

        private string MakeCommentsList(List<string> comments)
        {
            string CommentString = "";
            if (comments.Count > 0)
                CommentString = string.Join(",", Comments);

            return CommentString;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
            FirstComment = values[3];
            Comments = FromCommentsList(values[4]);
        }

        private List<string> FromCommentsList(string value)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(value))
                list = value.Split(",").ToList();

            return list;
        }
    }
}
