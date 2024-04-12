using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class GuidedTour : ISerializable
    {
        public int GuideId { get; set; }
        public int TourId { get; set; }

        public GuidedTour() { }

        public GuidedTour(int guideId, int tourId)
        {
            GuideId = guideId;
            TourId = tourId;
        }

        public GuidedTour(User guide, Tour tour) 
        { 
            GuideId = guide.Id;
            TourId = tour.Id;
        }

        public string[] ToCSV()
        {
            string[] CSVvalues = { GuideId.ToString(), TourId.ToString() };
            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            GuideId = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
        }
    }
}
