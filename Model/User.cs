using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public enum UserType{ host, tourist, guest, guide }

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Years { get; set; }

        public UserType Type { get; set; }

        public User() { }

        public User(string username, string password, UserType type, int years)
        {
            Username = username;
            Password = password;
            Type = type;
            Years = years;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Years.ToString(), Type.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Years = Convert.ToInt32(values[3]);
            if (values[4].Equals("host"))
            { 
                Type = UserType.host;
            }
            else if (values[4].Equals("tourist")){
                Type = UserType.tourist;
            }
            else if (values[4].Equals("guest")){
                Type = UserType.guest;
            }
            else if (values[4].Equals("guide")){
                Type = UserType.guide;
            }
        }
    }
}
