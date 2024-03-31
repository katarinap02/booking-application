using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class AccommodationRate : ISerializable
    {
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int HostId { get; set; }
        public int Cleanliness { get; set; }

        public int Correctness { get; set; }

        public string AdditionalComment { get; set; }

        public List<string> Images { get; set; }

        public AccommodationRate()
        {
            Images = new List<string>();
        }

        public AccommodationRate(int reservationId, int guestId, int hostId, int cleanliness, int correctness, string additionalComment)
        {
            ReservationId = reservationId;
            GuestId = guestId;
            HostId = hostId;
            Cleanliness = cleanliness;
            Correctness = correctness;
            AdditionalComment = additionalComment;
            Images = new List<string>();

        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            HostId = Convert.ToInt32(values[2]);
            Cleanliness = Convert.ToInt32(values[3]);
            Correctness = Convert.ToInt32(values[4]);
            AdditionalComment = values[5];
            Images = MakeListPictures(values[6]);


        }

        public string[] ToCSV()
        {
            string[] csvValues = { ReservationId.ToString(), GuestId.ToString(), HostId.ToString(), Cleanliness.ToString(), Correctness.ToString(),
            AdditionalComment, MakeStringFromPictures(Images)};
            return csvValues;
        }

        private List<String> MakeListPictures(string value)
        {
            List<String> list = new List<String>();
            if (!string.IsNullOrEmpty(value))
            {
                list = value.Split(",").ToList();
            }

            return list;
        }

        private string MakeStringFromPictures(List<string> pictures)
        {
            string PictureString = "";
            if (pictures != null)
            {
                PictureString = string.Join(",", Images);
            }
            return PictureString;
        }

       
    }
}
