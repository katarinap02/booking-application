using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public class RequestedTourParticipant : ISerializable
    {
        public int Id;
        public int TourRequestId;
        public string Name;
        public string LastName;
        public int Years;
        public bool HasJoinedTour;

        public RequestedTourParticipant() { }

        public RequestedTourParticipant(int id, int tourRequestId, string name, string lastName, int years)
        {
            Id = id;
            TourRequestId = tourRequestId;
            Name = name;
            LastName = lastName;
            Years = years;
            HasJoinedTour = false;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourRequestId = Convert.ToInt32(values[1]);
            Name = values[2];
            LastName = values[3];
            Years = Convert.ToInt32(values[4]);
            HasJoinedTour = bool.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourRequestId.ToString(), Name, LastName, Years.ToString(), HasJoinedTour.ToString() };
            return csvValues;
        }
    }
}
