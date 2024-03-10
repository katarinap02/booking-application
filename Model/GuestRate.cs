using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Model
{
    public class GuestRate : ISerializable
    {

        public int UserId { get; set; } //dogovoriti se da li cuvamo id ili celog user-a

        public int AcommodationId { get; set; } //dogovoriti se da li cuvamo id ili celog user-a

        public int Cleanliness {  get; set; } // staviti ogranicenje od 1 do 5

        public int RulesFollowing {  get; set; } // staviti ogranicenje od 1 do 5

        public string AdditionalComment { get; set; } //mozda 

        public int DaysLeft { get; set; } //potrebno je nesto sto ce da meri vreme, videti jos


        public GuestRate() { }

        public GuestRate(int userId, int acommodationId, int cleanliness, int rulesFollowing, string additionalComment)
        {
            UserId = userId;
            AcommodationId = acommodationId;
            Cleanliness = cleanliness;
            RulesFollowing = rulesFollowing;
            AdditionalComment = additionalComment;
            DaysLeft = 5; //videti jos
        }

        public void FromCSV(string[] values)
        {
            UserId = Convert.ToInt32(values[0]);
            AcommodationId = Convert.ToInt32(values[1]);
            Cleanliness = Convert.ToInt32(values[2]);
            RulesFollowing = Convert.ToInt32(values[3]);
            AdditionalComment = values[4];
            DaysLeft = Convert.ToInt32(values[5]);
            
        }

        public string[] ToCSV()
        {
            string[] csvValues = { UserId.ToString(), AcommodationId.ToString(), Cleanliness.ToString(), RulesFollowing.ToString(),
            AdditionalComment, DaysLeft.ToString()};
            return csvValues;
        }
    }
}
