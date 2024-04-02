using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class GuideRate : ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int GuideId { get; set; }
        public int Knowledge {  get; set; }
        public int Language { get; set; }
        public int TourInterest {  get; set; }
        public string AdditionalComment {  get; set; }
        public List<string> Pictures {  get; set; }

        public GuideRate() { }
        public GuideRate(int id, int touristId, int guideId, int knowledge, int language, int tourinterest, string additionalComment, List<string> pictures)
        {
            Id = id;
            TouristId = touristId;
            GuideId = guideId;
            Knowledge = knowledge;
            Language = language;
            TourInterest = tourinterest;
            AdditionalComment = additionalComment;
            Pictures = pictures;
        }


        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId= Convert.ToInt32(values[1]);
            GuideId= Convert.ToInt32(values[2]);
            Knowledge = Convert.ToInt32(values[3]);
            Language = Convert.ToInt32(values[4]);
            TourInterest= Convert.ToInt32(values[5]);
            AdditionalComment = values[6];
            if (!string.IsNullOrEmpty(values[7]))
            {
                string picture = values[7];
                Pictures = picture.Split(",").ToList();
            }

        }

        public string[] ToCSV()
        {
            string pictureString = "";
            if (Pictures != null)
            {
                pictureString = string.Join(",", Pictures);
            }

            string[] CSVvalues = {Id.ToString(), TouristId.ToString(), GuideId.ToString(), Knowledge.ToString(),
            Language.ToString(), TourInterest.ToString(), AdditionalComment, pictureString};
            return CSVvalues;
        }
    }
}
