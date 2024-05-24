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

        public ComplexTourRequest() { }
        public ComplexTourRequest(int id, string name, ComplexTourRequestStatus status)
        {
            this.Id = id;
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

            string[] CSVvalues = { Id.ToString(), Name, Status.ToString(), idString };
            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            ParseStatus(values[2]);
            if (!string.IsNullOrEmpty(values[3]))
            {
                string ids = values[3];
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
