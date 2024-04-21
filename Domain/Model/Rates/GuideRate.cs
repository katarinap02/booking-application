using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Rates
{
    public class GuideRate : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        public int Knowledge { get; set; }
        public int Language { get; set; }
        public int TourInterest { get; set; }
        public string AdditionalComment { get; set; }
        public List<string> Pictures { get; set; }

        public bool IsValid { get; set; }

        public GuideRate() { }
        public GuideRate(int id, int touristId, int tourId, int guideId, int knowledge, int language, int tourinterest, string additionalComment, List<string> pictures)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            GuideId = guideId;
            Knowledge = knowledge;
            Language = language;
            TourInterest = tourinterest;
            AdditionalComment = additionalComment;
            Pictures = pictures;
            IsValid = true;
        }

        public GuideRate(int id, int touristId, int tourId, int guideId, int knowledge, int language, int tourinterest, string additionalComment, List<string> pictures, bool valid)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            GuideId = guideId;
            Knowledge = knowledge;
            Language = language;
            TourInterest = tourinterest;
            AdditionalComment = additionalComment;
            Pictures = pictures;
            IsValid = valid;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            GuideId = Convert.ToInt32(values[3]);
            Knowledge = Convert.ToInt32(values[4]);
            Language = Convert.ToInt32(values[5]);
            TourInterest = Convert.ToInt32(values[6]);
            AdditionalComment = values[7];
            if (!string.IsNullOrEmpty(values[8]))
            {
                string picture = values[8];
                Pictures = picture.Split(",").ToList();
            }
            IsValid = Boolean.Parse(values[9]);
        }

        public string[] ToCSV()
        {
            string pictureString = "";
            if (Pictures != null)
            {
                pictureString = string.Join(",", Pictures);
            }

            string[] CSVvalues = {Id.ToString(), TouristId.ToString(), TourId.ToString(), GuideId.ToString(), Knowledge.ToString(),
            Language.ToString(), TourInterest.ToString(), AdditionalComment, pictureString, IsValid.ToString()};
            return CSVvalues;
        }
    }
}
