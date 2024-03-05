using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace BookingApp.Model
{
    public enum AccommodationType { APARTMENT = 0, HOUSE, COTTAGE}
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address {  get; set; } // grad i drzava, ako zatreba napravicemo klasu Address
        public AccommodationType Type { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationNumber { get; set; }
        public List<DateTime> AvailableDates { get; set; } // videcemo da li cemo ovo bas ovako
        public List<String> Images { get; set; } //za cuvanje URL-ova slika

        public Accommodation() { }

        public Accommodation(string name, string address, AccommodationType type, int maxGuestNumber, int minReservationNumber) 
        {
            Name = name;
            Address = address;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationNumber = minReservationNumber;
            AvailableDates = new List<DateTime>();
            Images = new List<String>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Address,
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationNumber.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Address = values[2];
            switch(values[3])
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
        }

        
    }
}
