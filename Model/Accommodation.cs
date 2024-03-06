using BookingApp.Serializer;
using System;
using System.Collections.Generic;
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
        public int MinReservationNumber { get; set; }
        public List<String> Images { get; set; } //za cuvanje URL-ova slika

        public int ReservationDaysLimit { get; set; }

        public Accommodation() { }


        public Accommodation(string name, string country, string city, AccommodationType type, int maxGuestNumber, int minReservationNumber, int reservationDaysLimit) 
        {
            Name = name;
            Country = country;
            City = city;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationNumber = minReservationNumber;
            Images = new List<String>();
            ReservationDaysLimit = reservationDaysLimit;
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
                MinReservationNumber.ToString(),
                ReservationDaysLimit.ToString()
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
            MaxGuestNumber = Convert.ToInt32(values[4]);
            MinReservationNumber = Convert.ToInt32(values[5]);
            ReservationDaysLimit = Convert.ToInt32(values[6]);

        }

        
    }
}
