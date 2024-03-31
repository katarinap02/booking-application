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
        public List<String> Pictures { get; set; } //za cuvanje URL-ova slika

        public int ReservationDaysLimit { get; set; }

        public List<CalendarDateRange> UnavailableDates { get; set; }

        public int HostId { get; set; } 
        public Accommodation() { 
        
            UnavailableDates = new List<CalendarDateRange>();
            Pictures = new List<String>();

        }


        public Accommodation(string name, string country, string city, AccommodationType type, int maxGuestNumber, int minReservationNumber, int reservationDaysLimit, int hostId) 
        {
            Name = name;
            Country = country;
            City = city;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationNumber;
            Pictures = new List<String>();
            ReservationDaysLimit = reservationDaysLimit;
            UnavailableDates = new List<CalendarDateRange>();
            HostId = hostId;
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
                MakeStringFromPictures(Pictures),
                FindUnavailableDates(UnavailableDates),
                HostId.ToString()

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            City = values[2];
            Country = values[3];

            Type = TypeFromCsv(values[4]);

            
            MaxGuestNumber = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            ReservationDaysLimit = Convert.ToInt32(values[7]);

            Pictures = MakeListPictures(values[8]);
            if(values.Length > 9) {
                UnavailableDates = MakeListDates(values[9]);
            }

            HostId = Convert.ToInt32(values[10]);

        }

        List<CalendarDateRange> MakeListDates(string value)
        {
            List<CalendarDateRange> list = new List<CalendarDateRange>();
            if (!string.IsNullOrEmpty(value)) {
                list = ConvertToDateRanges(value.Split(",").ToList());

            }
            return list;
        }

        private List<String> MakeListPictures(string value)
        {
            List<String> list = new List<String>(); 
            if (!string.IsNullOrEmpty(value))
            {
                list = value.Split(",").ToList();
            }

            return list;
        }

        private AccommodationType TypeFromCsv(string value)
        {
            AccommodationType type = AccommodationType.APARTMENT;
            switch (value)
            {
                case "HOUSE":
                    type = AccommodationType.HOUSE;
                    break;
                case "COTTAGE":
                    type = AccommodationType.COTTAGE;
                    break;

            }
            return type;
        }

        private string MakeStringFromPictures(List<string> pictures)
        {
            string PictureString = "";
            if (pictures != null)
            {
                PictureString = string.Join(",", Pictures);
            }
            return PictureString;
        }

        private string FindUnavailableDates(List<CalendarDateRange> dates)
        {

            string unavailableDates = "";
            if (dates != null)
                unavailableDates = string.Join(",", dates.Select(dateRange => ConvertToString(dateRange)));
            return unavailableDates;
        }


    }
}
