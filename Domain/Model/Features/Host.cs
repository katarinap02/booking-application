using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public class Host : User, ISerializable
    {
        public bool IsSuperHost;
        public double RateAverage;
        public int RateCount;
        public Host()
        {
            Type = UserType.host;
        }

        public Host(string username, string password, bool SuperHost, double rate, int count)
        {
            IsSuperHost = SuperHost;
            Username = username;
            Password = password;
            Type = UserType.host;
            RateAverage = rate;
            RateCount = count;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, IsSuperHost.ToString(), RateCount.ToString(), RateAverage.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            IsSuperHost = Convert.ToBoolean(values[3]);
            RateCount = Convert.ToInt32(values[4]);
            RateAverage = Convert.ToDouble(values[5]);


        }
    }
}
