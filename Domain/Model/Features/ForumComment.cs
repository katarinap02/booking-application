using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model.Features
{
    public class ForumComment : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Comment { get; set; }
        public bool IsSpecial { get; set; }

        public bool IsHost { get; set; }

        public DateTime Date { get; set; }

        public int ForumId { get; set; }

        public int Reports { get; set; }

        public bool IsReported {  get; set; } 
       

        public ForumComment()
        {

        }

        public ForumComment(int userId, string comment, bool isSpecial, bool isHost, DateTime date, int forumId, int reports, bool isReported)
        {
            
            UserId = userId;
            Comment = comment;
            IsSpecial = isSpecial;
            IsHost = isHost;
            Date = date;
            ForumId = forumId;
            Reports = reports;
            IsReported = isReported;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), Comment,
                IsSpecial.ToString(), IsHost.ToString(), Date.ToString(), ForumId.ToString(), Reports.ToString(), IsReported.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Comment = values[2];
            IsSpecial = Convert.ToBoolean(values[3]);
            IsHost = Convert.ToBoolean(values[4]);
            Date = Convert.ToDateTime(values[5]);
            ForumId = Convert.ToInt32(values[6]);
            Reports = Convert.ToInt32(values[7]);
            IsReported = Convert.ToBoolean(values[8]);
           
        }
    }
}
