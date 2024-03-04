using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.Model
{
    public class Tour : ISerializable
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int MaxTourists { get; set; }
        public List<string> Checkpoints { get; set; }
        public List<DateTime> Date { get; set; }
        public float Duration { get; set; }
        public List<string> Pictures { get; set; }

        public Tour() { }

        public Tour(string name, string location, string description, string language, int maxTourists, List<string> checkpoints, List<DateTime> date, float duration, List<string> pictures)
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

            string DatesString;
            if(Date != null) {
                DatesString = string.Join(",", Date.ToString());
            }
            else
            {
                DatesString = "";
            }

            string pictureString = "";
            if(Pictures != null)
            {
                pictureString = string.Join(",", Pictures);
            }

            string[] CSVvalues = { Name, Location, Description, Language, MaxTourists.ToString(), Duration.ToString(), checkpointsString, DatesString, pictureString};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0]; 
            Location = values[1];
            Description = values[2];
            Language = values[3];
            MaxTourists = int.Parse(values[4]);
            Duration = float.Parse(values[5]);

            if (!string.IsNullOrEmpty(values[6])) 
            {
                string checkpoints = values[6];
                Checkpoints = checkpoints.Split(",").ToList();
            }

            if (!string.IsNullOrEmpty(values[7]))
            {
                string datesString = values[7];
                //Date = datesString.Split(",").ToDateTime().ToList(); //posle
                /*if (DateTime.TryParseExact(values[1], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            grading_day = parsedDate.Date;
                        }*/
            }

            if (!string.IsNullOrEmpty(values[8]))
            {
                string picture = values[8];
                Pictures = picture.Split(",").ToList();
            }

        }
    }
}
