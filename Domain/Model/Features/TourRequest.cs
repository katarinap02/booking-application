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
        public int TouristId { get; set; }
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

        public TourRequest(int touristId, string description, string language, List<int> ids, DateTime startDate, DateTime endDate, string city, string country) { // kada se pravi zahtev
            TouristId = touristId;
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
            TouristId = tourRequest.TouristId;
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

            string[] CSVvalues = { Id.ToString(), TouristId.ToString(), Status.ToString(), City, Country, Description, Language, 
                                   StartDate.ToString(), EndDate.ToString(), AcceptedDate.ToString(), GuideId.ToString(), idString, DateRequested.ToString()};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            ParseRequestStatus(values[2]);
            City = values[3];
            Country = values[4];
            Description = values[5];
            Language = values[6];
            StartDate = ParseDate(values[7]);
            EndDate = ParseDate(values[8]);
            AcceptedDate = ParseDate(values[9]);
            GuideId = int.Parse(values[10]);
            if (!string.IsNullOrEmpty(values[11]))
            {
                string ids = values[11];
                List<string> participantIds = ids.Split(",").ToList();
                ParticipantIds = participantIds.Select(int.Parse).ToList();
            }
            DateRequested = ParseDate(values[12]);

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
