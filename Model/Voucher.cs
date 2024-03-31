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
        public int Id;
        public int TouristId;
        public bool HasBeenUsed;
        public string Reason;
        public DateOnly ExpireDate;

        public Voucher() { }
        public Voucher(int id, int touristId, bool hasBeenUsed, string reason, DateOnly expireDate)
        {
            Id = id;
            TouristId = touristId;
            HasBeenUsed = hasBeenUsed;
            Reason = reason;
            ExpireDate = expireDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            HasBeenUsed = Convert.ToBoolean(values[2]);
            Reason = values[3];
            if (DateOnly.TryParse(values[4], out DateOnly expireDate))
            {
                ExpireDate = expireDate;
            }

        }

        public string[] ToCSV()
        {
            string[] CSVvalues = { Id.ToString(), TouristId.ToString(), HasBeenUsed.ToString(), Reason, ExpireDate.ToString() };
            return CSVvalues;
        }
    }
}
