using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Domain.Model.Features
{
    public enum ComplexTourRequestStatus { Pending, Accepted, Invalid}
    public class ComplexTourRequest : ISerializable
    {
        public int Id;
        public ComplexTourRequestStatus Status;
        public List<int> TourRequests;
        public string Name;
        public int TouristId;

        public ComplexTourRequest() { }
        public ComplexTourRequest(int id, int touristId, string name, ComplexTourRequestStatus status)
        {
            this.Id = id;
            this.TouristId = touristId;
            this.Status = status;
            this.Name = name;
            TourRequests = new List<int>();
        }

        public string[] ToCSV()
        {
            string idString = "";
            if (TourRequests != null)
            {
                idString = string.Join(",", TourRequests);
            }

            string[] CSVvalues = { Id.ToString(), TouristId.ToString(), Name, Status.ToString(), idString };
            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TouristId = int.Parse(values[1]);
            Name = values[2];
            ParseStatus(values[3]);
            if (!string.IsNullOrEmpty(values[4]))
            {
                string ids = values[4];
                List<string> tourRequestIds = ids.Split(",").ToList();
                TourRequests = tourRequestIds.Select(int.Parse).ToList();
            }
        }
        private void ParseStatus(string csv_value)
        {
            if (csv_value.Equals("Pending"))
                Status = ComplexTourRequestStatus.Pending;
            else if (csv_value.Equals("Accepted"))
                Status = ComplexTourRequestStatus.Accepted;
            else if (csv_value.Equals("Invalid"))
                Status = ComplexTourRequestStatus.Invalid;
        }
    }
}
