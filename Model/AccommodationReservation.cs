using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
       // public int Id {  get; set; } mislim da ovo nece trebati
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DateRange => StartDate.ToString() + "-" + EndDate.ToString();

        public int NumberOfPeople { get; set; }
        
        public AccommodationReservation() { }
    
        public string[] ToCSV()
        {
            string[] csvValues =
            {

                GuestId.ToString(),
                AccommodationId.ToString(),
                DateRange,
                NumberOfPeople.ToString()

            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);   
            AccommodationId = Convert.ToInt32(values[1]);
            string[] dateParts = values[2].Split('-');
            StartDate = DateTime.Parse(dateParts[0]);
            EndDate = DateTime.Parse(dateParts[1]);
            NumberOfPeople = Convert.ToInt32(values[3]);

        }

    }
}
