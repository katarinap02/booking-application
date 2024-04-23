using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model.Features
{
    public class Guest : User, ISerializable
    {
       
        public int YearlyReservations { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int BonusPoints { get; set; }
        public bool IsSuperGuest { get; set; }

        public Guest() {
            Type = UserType.guest;
        }

        public Guest(string username, string password, int yearlyReservations, DateTime startDate, DateTime endDate, int bonusPoints, bool isSuperGuest)
        {
            IsSuperGuest = isSuperGuest;
            Username = username;
            Password = password;
            Type = UserType.host;
            YearlyReservations = yearlyReservations;
            StartDate = startDate;
            EndDate = endDate;
            BonusPoints = bonusPoints;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, YearlyReservations.ToString(), StartDate.ToString(), EndDate.ToString(), BonusPoints.ToString(), IsSuperGuest.ToString()};
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            YearlyReservations = Convert.ToInt32(values[3]);
            StartDate = Convert.ToDateTime(values[4]);
            EndDate = Convert.ToDateTime(values[5]);
            BonusPoints = Convert.ToInt32(values[6]);
            IsSuperGuest = Convert.ToBoolean(values[7]);



        }
    }
}
