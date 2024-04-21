using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model.Rates
{
    public class GuestRate : ISerializable
    {

        public int ReservationId { get; set; }
        public int UserId { get; set; } //dogovoriti se da li cuvamo id ili celog user-a

        public int AcommodationId { get; set; } //dogovoriti se da li cuvamo id ili celog user-a

        public int Cleanliness { get; set; } // staviti ogranicenje od 1 do 5

        public int RulesFollowing { get; set; } // staviti ogranicenje od 1 do 5

        public string AdditionalComment { get; set; } //mozda 




        public GuestRate() { }

        public GuestRate(int reservationId, int userId, int acommodationId, int cleanliness, int rulesFollowing, string additionalComment)
        {
            ReservationId = reservationId;
            UserId = userId;
            AcommodationId = acommodationId;
            Cleanliness = cleanliness;
            RulesFollowing = rulesFollowing;
            AdditionalComment = additionalComment;

        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            AcommodationId = Convert.ToInt32(values[2]);
            Cleanliness = Convert.ToInt32(values[3]);
            RulesFollowing = Convert.ToInt32(values[4]);
            AdditionalComment = values[5];


        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), UserId.ToString(), AcommodationId.ToString(), Cleanliness.ToString(), RulesFollowing.ToString(),
            AdditionalComment};
            return csvValues;
        }
    }
}
