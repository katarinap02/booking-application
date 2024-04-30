using BookingApp.Domain.Model.Features;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ISerializable = BookingApp.Serializer.ISerializable;

namespace BookingApp.Domain.Model.Reservations
{
    public class Renovation : ISerializable
    {
        public int Id;

        public int HostId;

        public int AccommodationId;

        public DateTime StartDate;

        public DateTime EndDate;

        public string Description;

        public string DateRange => StartDate.ToString() + "-" + EndDate.ToString();

        public int Duration;

        public Renovation() { }

        public Renovation(int accommodationId, int hostId, DateTime startDate, DateTime endDate, int duration, string description)
        {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            HostId = hostId;
            Description = description;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            HostId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            string[] dateParts = values[3].Split('-');
            StartDate = DateTime.Parse(dateParts[0]);
            EndDate = DateTime.Parse(dateParts[1]);
            Duration = Convert.ToInt32(values[4]);
            Description = Convert.ToString(values[5]);  
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                HostId.ToString(),
                AccommodationId.ToString(),
                DateRange,
                Duration.ToString(),
                Description

            };
            return csvValues;
        }
    }
}
