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
        public int StartCheckpoint;

        public TourReservation() { }

        public TourReservation(int id, int tourId, int startCheckpoint)
        {
            Id = id;
            TourId = tourId;
            StartCheckpoint = startCheckpoint;
            List<int> _participantIds = new List<int>();
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            StartCheckpoint = Convert.ToInt32(values[2]);
            if (!string.IsNullOrEmpty(values[3]))
            {
                string participantIds = values[3];
                List<string> participantIdsSplit = participantIds.Split(',').ToList();
                // pretvaranje sa List<string> u List<int>
                ParticipantIds = participantIdsSplit.Select(int.Parse).ToList();
            }
        }

        public string[] ToCSV()
        {
            string participantIds = ParticipantIds != null ? string.Join(",", ParticipantIds) : "";

            string[] csValue = { Id.ToString(), TourId.ToString(), StartCheckpoint.ToString(), participantIds };

            return csValue;
        }
    }
}
