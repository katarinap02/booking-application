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
        private List<int> _touristIds;
        private int _tourId;
        private int _startCheckpoint;

        TourReservation() { }

        TourReservation(int id, int tourId, int startCheckpoint)
        {
            _id = id;
            _tourId = tourId;
            _startCheckpoint = startCheckpoint;
        }

        public void FromCSV(string[] values)
        {
            _id = Convert.ToInt32(values[0]);
            _tourId = Convert.ToInt32(values[1]);
            _startCheckpoint = Convert.ToInt32(values[2]);
            if (!string.IsNullOrEmpty(values[3]))
            {
                string touristIds = values[3];
                List<string> touristIdsList = touristIds.Split(',').ToList();
                // pretvaranje sa List<string> u List<int>
                _touristIds = touristIdsList.Select(int.Parse).ToList();
            }
        }

        public string[] ToCSV()
        {
            string touristIds = _touristIds != null ? string.Join(",", _touristIds) : "";

            string[] csValue = { _id.ToString(), _tourId.ToString(), _startCheckpoint.ToString(), touristIds };

            return csValue;
        }
    }
}
