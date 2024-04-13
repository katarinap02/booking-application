using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class TourParticipant : ISerializable
    {
        public int Id;
        public int ReservationId;
        public string Name;
        public string LastName;
        public int Years;
        public bool HasJoinedTour;
        public int JoinedCheckpointIndex;

        public TourParticipant() { }
        public TourParticipant(int id, int reservationId, string name, string lastName, int years)
        {
            Id = id;
            ReservationId = reservationId;
            Name = name;
            LastName = lastName;
            Years = years;
            HasJoinedTour = false;
            JoinedCheckpointIndex = 0;
        }
        public TourParticipant(int id, int reservationId, string name, string lastName, int years, int checkpointIndex)
        {
            Id = id;
            ReservationId = reservationId;
            Name = name;
            LastName = lastName;
            Years = years;
            HasJoinedTour = false;
            JoinedCheckpointIndex = checkpointIndex;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId= Convert.ToInt32(values[1]);
            Name = values[2];
            LastName = values[3];
            Years = Convert.ToInt32(values[4]);
            HasJoinedTour = bool.Parse(values[5]);
            JoinedCheckpointIndex = int.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Name, LastName, Years.ToString(), HasJoinedTour.ToString(), JoinedCheckpointIndex.ToString()};
            return csvValues;
        }
    }
}
