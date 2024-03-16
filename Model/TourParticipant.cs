﻿using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public class TourParticipant : ISerializable
    {
        public int Id;
        public int ReservationId;
        public string Name;
        public string LastName;
        public int Years;

        public TourParticipant() { }
        public TourParticipant(int id, int reservationId, string name, string lastName, int years)
        {
            Id = id;
            ReservationId = reservationId;
            Name = name;
            LastName = lastName;
            Years = years;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ReservationId= Convert.ToInt32(values[1]);
            Name = values[2];
            LastName = values[3];
            Years = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), Name, LastName, Years.ToString() };
            return csvValues;
        }
    }
}