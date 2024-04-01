using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Host : User, ISerializable
    {
        public bool IsSuperHost;
        public Host() {
            Type = UserType.host;
        }

        public Host(string username, string password, bool SuperHost)
        {
            IsSuperHost = SuperHost;
            Username = username;
            Password = password;
            Type = UserType.host;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, IsSuperHost.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            IsSuperHost = Convert.ToBoolean(values[3]);
            /*
            if (values[3].Equals("True"))
            {
                IsSuperHost = true;
            }
            else
            {
                IsSuperHost = false;
            }
            */
            
        }
    }
}
