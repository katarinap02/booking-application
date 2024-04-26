using BookingApp.Serializer;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Model.Features
{
    public enum TourRequestStatus { Pending, Accepted, Invalid }
    public class TourRequest : ISerializable
    {
        public string City { get; set; }
        public string Country { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public TourRequestStatus Status { get; set; }
        public string Language {  get; set; }
        public List<int> ParticipantIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AcceptedDate { get; set; }
        public int GuideId { get; set; }
        public DateTime DateRequested { get; set; }

        public TourRequest() { }

        public TourRequest(string description, string language, List<int> ids, DateTime startDate, DateTime endDate, string city, string country) { // kada se pravi zahtev
            City = city;
            Country = country;
            Description = description;
            Status = TourRequestStatus.Pending;
            ParticipantIds = ids;
            StartDate = startDate;
            EndDate = endDate;
            Language = language;
            GuideId = -1;
            AcceptedDate = DateTime.MinValue;
            DateRequested = DateTime.Now;
        }

        public TourRequest(TourRequest tourRequest) 
        { 
            Id = tourRequest.Id;
            City = tourRequest.City;
            Country = tourRequest.Country;
            Description = tourRequest.Description;
            Language = tourRequest.Language;
            ParticipantIds = tourRequest.ParticipantIds;
            Status = tourRequest.Status;
            StartDate = tourRequest.StartDate;
            EndDate = tourRequest.EndDate;
            AcceptedDate = tourRequest.AcceptedDate;            
            GuideId = tourRequest.GuideId;
            DateRequested = tourRequest.DateRequested;
        }

        public string[] ToCSV()
        {
            string idString = "";
            if (ParticipantIds != null)
            {
                idString = string.Join(",", ParticipantIds);
            }

            string[] CSVvalues = { Id.ToString(), Status.ToString(), City, Country, Description, Language, 
                                   StartDate.ToString(), EndDate.ToString(), AcceptedDate.ToString(), GuideId.ToString(), idString, DateRequested.ToString()};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ParseRequestStatus(values[1]);
            City = values[2];
            Country = values[3];
            Description = values[4];
            Language = values[5];
            StartDate = ParseDate(values[6]);
            EndDate = ParseDate(values[7]);
            AcceptedDate = ParseDate(values[8]);
            GuideId = int.Parse(values[9]);
            if (!string.IsNullOrEmpty(values[10]))
            {
                string ids = values[10];
                List<string> participantIds = ids.Split(",").ToList();
                ParticipantIds = participantIds.Select(int.Parse).ToList();
            }
            DateRequested = ParseDate(values[11]);

        }

        public DateTime ParseDate( string csv_values)
        {
            DateTime date = DateTime.MinValue;
            string dateFormat = "M/d/yyyy h:mm:ss tt";
            if (DateTime.TryParseExact(csv_values, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                date =  parsedDate;
            }
            return date;
        }

        public void ParseRequestStatus(string csv_values)
        {
            if (csv_values.Equals("Pending"))
            {
                Status = TourRequestStatus.Pending;
            }
            else if (csv_values.Equals("Accepted"))
            {
                Status = TourRequestStatus.Accepted;
            }
            else if (csv_values.Equals("Invalid"))
            {
                Status = TourRequestStatus.Invalid;
            }
        }



    }
}
