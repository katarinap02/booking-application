using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model.Features
{
    public class Tourist : User, ISerializable
    {
        public int Id;
        public string Name;
        public string LastName;
        public int Age;

        public Tourist() { }

        public Tourist(int id, string name, string lastName, int age, string username, string password)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Age = age;
            Username = username;
            Password = password;
        }

        public override string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, LastName, Age.ToString(), Username, Password};
            return csvValues;
        }

        public override void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LastName = values[2];
            Age = Convert.ToInt32(values[3]);
            Username = values[4];
            Password = values[5];
        }
    }
}
