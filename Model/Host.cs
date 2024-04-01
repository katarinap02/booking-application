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
            string[] csvValues = { Id.ToString(), Username, Password, Type.ToString(), IsSuperHost.ToString() };
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            if (values[3].Equals("host"))
            { 
                Type = UserType.host;
            }
            else if (values[3].Equals("tourist")){
                Type = UserType.tourist;
            }
            else if (values[3].Equals("guest")){
                Type = UserType.guest;
            }
            else if (values[3].Equals("guide")){
                Type = UserType.guide;
            }
            IsSuperHost = Convert.ToBoolean(values[4]);
        }
    }
}
