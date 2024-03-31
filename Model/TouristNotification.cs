using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum NotificationType { RateTour, TourCanceled, GuideQuit, JoinedTour }
    class TouristNotification : ISerializable
    {
        public int Id;
        public int TouristId;
        public int TourId;
        public NotificationType NotificationType;
        public string TourName;


        public TouristNotification(int id, int touristId, int tourId, NotificationType type, string tourName)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            NotificationType = type;
            TourName = tourName;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            ParseNotificationType(values[3]);
            TourName = values[4];

        }

        public void ParseNotificationType(string csv_values)
        {
            if (csv_values.Equals("RateTour"))
            {
                NotificationType = NotificationType.RateTour;
            }
            else if (csv_values.Equals("TourCanceled"))
            {
                NotificationType = NotificationType.TourCanceled;
            }
            else if (csv_values.Equals("GuideQuit"))
            {
                NotificationType = NotificationType.GuideQuit;
            }
            else if (csv_values.Equals("JoinedTour"))
            {
                NotificationType = NotificationType.JoinedTour;
            }
        }

        public string[] ToCSV()
        {
            string[] csValues = { Id.ToString(), TouristId.ToString(), TourId.ToString(), NotificationType.ToString(), TourName };
            return csValues;
        }
    }
}
