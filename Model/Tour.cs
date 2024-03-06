using BookingApp.Serializer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.Model
{
    public enum TourStatus { inPreparation, Active, Finnished, Canceled }
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<string> Checkpoints { get; set; }
        public DateTime Date { get; set; }
        public float Duration { get; set; }
        public List<string> Pictures { get; set; }
        public TourStatus Status { get; set; }
        public int GroupId { get; set; }
        public int currentCheckpoint { get; set; }

        public Tour() { }

        public Tour(string name, string location, string description, string language, int maxTourists, List<string> checkpoints, DateTime date, float duration, List<string> pictures)
        {
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            Checkpoints = checkpoints;
            Date = date;
            Duration = duration;
            Pictures = pictures;
            Status = TourStatus.inPreparation; //kad se pravi noava tura, ona ne moze biti zavrsena ili u toku
            currentCheckpoint = 0;
            // + u dao napraviti da dodeljuje jedinstven groupId
        }

        public string[] ToCSV()
        {
            string checkpointsString;
            if (Checkpoints != null)
            {
                checkpointsString = string.Join(",", Checkpoints);
            }
            else
            {
                checkpointsString = "";
            }

            string pictureString = "";
            if(Pictures != null)
            {
                pictureString = string.Join(",", Pictures);
            }

            string[] CSVvalues = { Id.ToString(), Status.ToString(), Name, Location, Description, Language, MaxTourists.ToString(), Duration.ToString(), Date.ToString(), GroupId.ToString(), currentCheckpoint.ToString(), checkpointsString, pictureString};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);

            if (values[1].Equals("inPreparation"))
            {
                Status = TourStatus.inPreparation;
            }
            else if (values[1].Equals("Active"))
            {
                Status = TourStatus.Active;
            }
            else if (values[1].Equals("Finnished"))
            {
                Status = TourStatus.Finnished;
            }
            else if (values[1].Equals("Canceled"))
            {
                Status = TourStatus.Canceled;
            }

            Name = values[2];
            Location = values[3];
            Description = values[4];
            Language = values[5];
            MaxTourists = int.Parse(values[6]);
            Duration = float.Parse(values[7]);
            string dateFormat = "MM/dd/yyyy hh:mm:ss tt";
            if (DateTime.TryParseExact(values[8], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                Date = parsedDate.Date;
            }

            GroupId = int.Parse(values[9]);
            currentCheckpoint = int.Parse(values[10]);

            if (!string.IsNullOrEmpty(values[11]))
            {
                string checkpoints = values[11];
                Checkpoints = checkpoints.Split(",").ToList();
            }

            if (!string.IsNullOrEmpty(values[12]))
            {
                string picture = values[12];
                Pictures = picture.Split(",").ToList();
            }



        }
    }
}
