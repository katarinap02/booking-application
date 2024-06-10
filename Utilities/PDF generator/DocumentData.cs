using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Utilities.PDF_generator
{
    public class DocumentData
    {
        public string ApplicationLogoPath { get; set; }
        public string ApplicationName { get; set; }
        public string GuideName { get; set; }
        public DateTime GeneratedTime { get; set; }
        public byte[] PieChart { get; set; }
        public MostVisitedTours MostVisitedTours { get; set; }
        public int Below18 { get; set; }
        public int Between18And50 { get; set; }  // Add this line if it's missing
        public int Above50 { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string TourName { get; set; }
    }

    public class MostVisitedTours
    {
        public string AllTime { get; set; }
        public string Year2024 { get; set; }
        public string Year2023 { get; set; }
    }
}
