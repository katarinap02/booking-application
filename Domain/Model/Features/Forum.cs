using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public class Forum : ISerializable
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FirstComment { get; set; }

        public List<int> Comments { get; set; }

        public bool IsClosed { get; set; }

        public bool IsVeryUseful { get; set; }

        public DateTime Date { get; set; }

        public Forum() { 
             Comments = new List<int>(); 
        
        }
        public Forum(int userId, string city, string country, string firstComment, bool isClosed, bool isVeryUseful, DateTime date)
        {
            UserId = userId;
            City = city;
            Country = country;
            FirstComment = firstComment;
            Comments = new List<int>();
            IsClosed = isClosed;
            IsVeryUseful = isVeryUseful;
            Date = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), City, Country, FirstComment, MakeCommentsList(Comments), IsClosed.ToString(), IsVeryUseful.ToString(), Date.ToString() };
            return csvValues;
        }

        private string MakeCommentsList(List<int> comments)
        {
            string CommentString = "";
            CommentString += comments[0];
            int i;
            for(i = 1; i <= comments.Count-1; i++)
            {
                CommentString += "," + comments[i];

            }

            

            return CommentString;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            City = values[2];
            Country = values[3];
            FirstComment = values[4];
            Comments = FromCommentsList(values[5]);
            IsClosed = Convert.ToBoolean(values[6]);
            IsVeryUseful = Convert.ToBoolean(values[7]);
            Date = Convert.ToDateTime(values[8]);
        }

        private List<int> FromCommentsList(string value)
        {
            List<string> list = new List<string>();
            List<int> result = new List<int>();
            if (!string.IsNullOrEmpty(value))
                list = value.Split(",").ToList();
            foreach(String item in list)
            {
                result.Add(Convert.ToInt32(item));
            }
            return result;
        }
    }
}
