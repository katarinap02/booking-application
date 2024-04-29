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

        public int AccommodationId;

        public DateTime StartDate;

        public DateTime EndDate;

        public string DateRange => StartDate.ToString() + "-" + EndDate.ToString();

        public int Duration;

        public Renovation() { }

        public Renovation(int accommodationId, DateTime startDate, DateTime endDate, int duration)
        {
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            string[] dateParts = values[2].Split('-');
            StartDate = DateTime.Parse(dateParts[0]);
            EndDate = DateTime.Parse(dateParts[1]);
            Duration = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                DateRange,
                Duration.ToString()

            };
            return csvValues;
        }
    }
}
