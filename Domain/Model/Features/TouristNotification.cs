﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public enum NotificationType { RateTour, TourCanceled, GuideQuit, JoinedTour, RequestAccepted }
    public class TouristNotification : ISerializable, IEquatable<TouristNotification>
    {
        public int Id;
        public int TouristId;
        public int TourId;
        public string GuideName;
        public NotificationType NotificationType;
        public string TourName;
        public int CurrentCheckpoint;
        public string Description;

        public TouristNotification() { }

        public TouristNotification(int touristId, int tourId, NotificationType type, string tourName, string guideName, int currentCheckpoint)
        {
            TouristId = touristId;
            TourId = tourId;
            NotificationType = type;
            TourName = tourName;
            GuideName = guideName;
            CurrentCheckpoint = currentCheckpoint;
        }
        public TouristNotification(int id, int touristId, int tourId, NotificationType type, string tourName, string guideName, int currentCheckpoint, string description)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            NotificationType = type;
            TourName = tourName;
            GuideName = guideName;
            CurrentCheckpoint = currentCheckpoint;
            Description = description;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            ParseNotificationType(values[3]);
            TourName = values[4];
            GuideName = values[5];
            CurrentCheckpoint = Convert.ToInt32(values[6]);
            Description = values[7];

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
            else if (csv_values.Equals("RequestAccepted"))
            {
                NotificationType = NotificationType.RequestAccepted;
            }
        }

        public string[] ToCSV()
        {
            string[] csValues = { Id.ToString(), TouristId.ToString(), TourId.ToString(), NotificationType.ToString(), TourName, GuideName, CurrentCheckpoint.ToString(), Description };
            return csValues;
        }

        public bool Equals(TouristNotification? other)
        {
            if (other == null)
                return false;

            return (TourId, TouristId, CurrentCheckpoint, NotificationType)
                .Equals((other.TourId, other.TouristId, other.CurrentCheckpoint, other.NotificationType));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return Equals(obj as TouristNotification);
        }
        public override int GetHashCode()
        {
            return TourId.GetHashCode() ^ TouristId.GetHashCode() ^
                   CurrentCheckpoint.GetHashCode() ^ NotificationType.GetHashCode();
        }
    }
}
