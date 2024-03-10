using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BookingApp.Model
{
    public enum AccommodationType { APARTMENT, HOUSE, COTTAGE}
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country {  get; set; } // grad i drzava, ako zatreba napravicemo klasu Address
        public string City { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public List<String> Images { get; set; } //za cuvanje URL-ova slika

        public int ReservationDaysLimit { get; set; }

        public List<CalendarDateRange> UnavailableDates { get; set; }
        public Accommodation() { 
        
            UnavailableDates = new List<CalendarDateRange>();

        }


        public Accommodation(string name, string country, string city, AccommodationType type, int maxGuestNumber, int minReservationNumber, int reservationDaysLimit) 
        {
            Name = name;
            Country = country;
            City = city;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationNumber;
            Images = new List<String>();
            ReservationDaysLimit = reservationDaysLimit;
            UnavailableDates = new List<CalendarDateRange>();
        }

        public string ConvertToString(CalendarDateRange range)
        {
            DateTime startDate = range.Start;
            DateTime endDate = range.End;
            return startDate + "-" + endDate;

        }

        public List<CalendarDateRange> ConvertToDateRanges(List<string> values)
        {
            List<CalendarDateRange> result = new List<CalendarDateRange>();

            foreach(string value in values)
            {
                string[] dateParts = value.Split("-");
                DateTime start = Convert.ToDateTime(dateParts[0]);
               
                DateTime end = Convert.ToDateTime(dateParts[1]);
                result.Add(new CalendarDateRange(start, end));

            }

            return result;
        }
        public string[] ToCSV()
        {
            string ImageString = "";
            if (Images != null)
            {
                ImageString = string.Join(",", Images);
            }

            string unavailableDates = "";
            if (UnavailableDates != null)
                unavailableDates = string.Join(",", UnavailableDates.Select(dateRange => ConvertToString(dateRange)));
          



            string[] csvValues =
            {
                Id.ToString(),
                Name,
                City,
                Country,
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                ReservationDaysLimit.ToString(),
                ImageString,
                unavailableDates
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            Country = values[3];
            switch(values[4])
            {
                case "APARTMENT":
                    Type = AccommodationType.APARTMENT;
                    break;
                case "HOUSE":
                    Type = AccommodationType.HOUSE;
                    break;
                case "COTTAGE":
                    Type = AccommodationType.COTTAGE;
                    break;



            }
            MaxGuestNumber = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            ReservationDaysLimit = Convert.ToInt32(values[7]);

            if (!string.IsNullOrEmpty(values[8]))
            {
                string image = values[8];
                Images = image.Split(",").ToList();
            }

            if (values.Length > 9 && !string.IsNullOrEmpty(values[9]))
            {
                string unavailableDates = values[9];
                UnavailableDates = ConvertToDateRanges(values[9].Split(",").ToList());
            }


        }

        
    }
}
