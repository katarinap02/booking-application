using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id;
        public List<int> ParticipantIds;
        public int TourId;
        public int TouristId;
        public int StartCheckpoint;
        public bool HasJoinedTour;

        public TourReservation() { }

        public TourReservation(int id, int tourId, int touristId, int startCheckpoint)
        {
            Id = id;
            TourId = tourId;
            TouristId = touristId;
            StartCheckpoint = startCheckpoint;
            List<int> _participantIds = new List<int>();
            HasJoinedTour = false;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            TouristId = Convert.ToInt32(values[2]);
            StartCheckpoint = Convert.ToInt32(values[3]);
            if (!string.IsNullOrEmpty(values[4]))
            {
                string participantIds = values[4];
                List<string> participantIdsSplit = participantIds.Split(',').ToList();
                // pretvaranje sa List<string> u List<int>
                ParticipantIds = participantIdsSplit.Select(int.Parse).ToList();
            }
            HasJoinedTour = Convert.ToBoolean(values[5]);
        }

        public string[] ToCSV()
        {
            string participantIds = ParticipantIds != null ? string.Join(",", ParticipantIds) : "";

            string[] csValue = { Id.ToString(), TourId.ToString(), TouristId.ToString(), StartCheckpoint.ToString(), participantIds, HasJoinedTour.ToString()};

            return csValue;
        }
    }
}
