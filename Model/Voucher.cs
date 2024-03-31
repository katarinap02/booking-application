using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }
        public bool HasBeenUsed { get; set; }
        public string Reason { get; set; }
        public DateOnly ExpireDate { get; set; }

        public Voucher() { }
        public Voucher(int id, int touristId, int guideId, bool hasBeenUsed, string reason, DateOnly expireDate)
        {
            Id = id;
            TouristId = touristId;
            GuideId = guideId;
            HasBeenUsed = hasBeenUsed;
            Reason = reason;
            ExpireDate = expireDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            HasBeenUsed = Convert.ToBoolean(values[3]);
            Reason = values[4];
            if (DateOnly.TryParse(values[5], out DateOnly expireDate))
            {
                ExpireDate = expireDate;
            }

        }

        public string[] ToCSV()
        {
            string[] CSVvalues = { Id.ToString(), TouristId.ToString(), GuideId.ToString(), HasBeenUsed.ToString(), Reason, ExpireDate.ToString() };
            return CSVvalues;
        }
    }
}
