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
        private int _id;
        private List<int> _participantIds;
        private int _tourId;
        private int _startCheckpoint;

        public TourReservation() { }

        public TourReservation(int id, int tourId, int startCheckpoint)
        {
            _id = id;
            _tourId = tourId;
            _startCheckpoint = startCheckpoint;
            List<int> _participantIds = new List<int>();
        }

        public void FromCSV(string[] values)
        {
            _id = Convert.ToInt32(values[0]);
            _tourId = Convert.ToInt32(values[1]);
            _startCheckpoint = Convert.ToInt32(values[2]);
            if (!string.IsNullOrEmpty(values[3]))
            {
                string participantIds = values[3];
                List<string> participantIdsSplit = participantIds.Split(',').ToList();
                // pretvaranje sa List<string> u List<int>
                _participantIds = participantIdsSplit.Select(int.Parse).ToList();
            }
        }

        public string[] ToCSV()
        {
            string participantIds = _participantIds != null ? string.Join(",", _participantIds) : "";

            string[] csValue = { _id.ToString(), _tourId.ToString(), _startCheckpoint.ToString(), participantIds };

            return csValue;
        }
    }
}
