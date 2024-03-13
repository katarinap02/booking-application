using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class TourParticipant : ISerializable
    {
        public int Id;
        public string Name;
        public string LastName;
        public int Years;

        public TourParticipant() { }
        public TourParticipant(int id, string name, string lastName, int years)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Years = years;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LastName = values[2];
            Years = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),  Name, LastName, Years.ToString() };
            return csvValues;
        }
    }
}
