using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Reservations
{
    public class ReservationCancellation : ISerializable
    {
        public int GuestId { get; set; }
        public int HostId { get; set; }

        public int AccommodationId { get; set; }

        public DateTime CancellationDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReservationCancellation() { }
        public ReservationCancellation(int guestId, int hostId, int accommodationId, DateTime cancellationDate, DateTime startDate, DateTime endDate)
        {
            GuestId = guestId;
            HostId = hostId;
            AccommodationId = accommodationId;
            CancellationDate = cancellationDate;
            StartDate = startDate;
            EndDate = endDate;
        }



        public string[] ToCSV()
        {
            string[] csvValues = { GuestId.ToString(), HostId.ToString(), AccommodationId.ToString(), CancellationDate.ToString(), StartDate.ToString(), EndDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            HostId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            CancellationDate = Convert.ToDateTime(values[3]);
            StartDate = Convert.ToDateTime(values[4]);
            EndDate = Convert.ToDateTime(values[5]);
        }
    }
}
