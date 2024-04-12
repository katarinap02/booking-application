using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class ReservationCancellation : ISerializable
    {
        public int GuestId { get; set; }
        public int HostId { get; set; }

        public int ReservationId { get; set; }

        public DateTime CancellationDate { get; set; }

        public ReservationCancellation() { }
        public ReservationCancellation(int guestId, int hostId, int reservationId, DateTime cancellationDate)
        {
            GuestId = guestId;
            HostId = hostId;
            ReservationId = reservationId;
            CancellationDate = cancellationDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestId.ToString(), HostId.ToString(), ReservationId.ToString(), CancellationDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            HostId = Convert.ToInt32(values[1]);
            ReservationId = Convert.ToInt32(values[2]);
            CancellationDate = Convert.ToDateTime(values[3]);
        }
    }
}
