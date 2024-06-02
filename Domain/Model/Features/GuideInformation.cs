using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Domain.Model.Features
{
    public enum GuideStatus { Regular, Super }
    public class GuideInformation : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GuideStatus Status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public List<string> PreviousSuperGuides { get; set; }
        public string Username { get; set; }
        public double AverageGrade { get; set; }
        public int TourNumber {  get; set; }
        public string MostUsedLanguage { get; set; }
        public string LanguageByTour { get; set; }
        public bool HasQuit { get; set; }
        public DateTime EndSuperGuide { get; set; }

        public GuideInformation() { }
        public GuideInformation(int id, int userId, GuideStatus status, string name, string surname, string phoneNumber, string email, int age , List<string> previousSuperGuides, string username)
        {
            Id = id;
            UserId = userId;
            Status = status;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
            Age = age;
            PreviousSuperGuides = previousSuperGuides;
            Username = username;
        }

        public GuideInformation(double avg, DateTime date, string language, int tn, int id, int userId, GuideStatus status, string name, string surname, string phoneNumber, string email, int age, List<string> previousSuperGuides, string username)
        {
            Id = id;
            UserId = userId;
            Status = status;
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
            Age = age;
            PreviousSuperGuides = previousSuperGuides;
            Username = username;
            AverageGrade = avg;
            TourNumber = tn;
            MostUsedLanguage = language;
            EndSuperGuide = date;
        }

        public string[] ToCSV()
        {
            string PreviousSuperGuidesString = "";
            if (PreviousSuperGuides != null)
            {
                PreviousSuperGuidesString = string.Join(",", PreviousSuperGuides);
            }
            string[] CSVvalues = { Id.ToString(), UserId.ToString(), Status.ToString(), Name, Surname, PhoneNumber, Email, Age.ToString(), Username, PreviousSuperGuidesString, HasQuit.ToString(), EndSuperGuide.ToString()};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Status = (GuideStatus)Enum.Parse(typeof(GuideStatus), values[2]);
            Name = values[3];
            Surname = values[4];
            PhoneNumber = values[5];
            Email = values[6];
            Age = int.Parse(values[7]);
            Username = values[8];

            if (values.Length > 9)
            {
                PreviousSuperGuides = values[9].Split(',').ToList();
            }
            else
            {
                PreviousSuperGuides = null;
            }

            HasQuit = bool.Parse(values[10]);
            string dateFormat = "M/d/yyyy h:mm:ss tt";
            if (DateTime.TryParseExact(values[10], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                EndSuperGuide = parsedDate;
            }
        }

    }
}
