﻿using BookingApp.Serializer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace BookingApp.Domain.Model.Features
{
    public enum TourStatus { inPreparation, Active, Finnished, Canceled, gotGuide }
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
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
        public string Country { get; set; }
        public int AvailablePlaces { get; set; }
        public int GuideId { get; set; }

        public Tour() { }

        public Tour(string name, string city, string country, string description, string language, int maxTourists, List<string> checkpoints, DateTime date, float duration, List<string> pictures)
        {
            Name = name;
            City = city;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            Checkpoints = checkpoints;
            Date = date;
            Duration = duration;
            Pictures = pictures;
            Status = TourStatus.inPreparation; //kad se pravi noava tura, ona ne moze biti zavrsena ili u toku
            currentCheckpoint = 0;
            Country = country;
            AvailablePlaces = maxTourists;
            // + u dao napraviti da dodeljuje jedinstven groupId
        }

        public Tour(string name, string city, string country, string description, string language, int maxTourists, List<string> checkpoints, DateTime date, float duration, List<string> pictures, int guideId)
        {
            Name = name;
            City = city;
            Description = description;
            Language = language;
            MaxTourists = maxTourists;
            Checkpoints = checkpoints;
            Date = date;
            Duration = duration;
            Pictures = pictures;
            Status = TourStatus.inPreparation;
            currentCheckpoint = 0;
            Country = country;
            AvailablePlaces = maxTourists;
            GuideId = guideId;
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
            if (Pictures != null)
            {
                pictureString = string.Join(",", Pictures);
            }

            string[] CSVvalues = { Id.ToString(), Status.ToString(), Name, City, Description, Language, MaxTourists.ToString(), Duration.ToString(), Date.ToString(),
                GroupId.ToString(), currentCheckpoint.ToString(), Country, checkpointsString, pictureString, AvailablePlaces.ToString(), GuideId.ToString()};

            return CSVvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);

            ParseTourStatus(values[1]);

            Name = values[2];
            City = values[3];
            Description = values[4];
            Language = values[5];
            MaxTourists = int.Parse(values[6]);
            Duration = float.Parse(values[7]);

            string dateFormat = "M/d/yyyy h:mm:ss tt";
            if (DateTime.TryParseExact(values[8], dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                Date = parsedDate;
            }


            GroupId = int.Parse(values[9]);
            currentCheckpoint = int.Parse(values[10]);
            Country = values[11];


            if (!string.IsNullOrEmpty(values[12]))
            {
                string checkpoints = values[12];
                Checkpoints = checkpoints.Split(",").ToList();
            }

            if (!string.IsNullOrEmpty(values[13]))
            {
                string picture = values[13];
                Pictures = picture.Split(",").ToList();
            }

            AvailablePlaces = int.Parse(values[14]);
            GuideId = int.Parse(values[15]);
        }

        public void ParseTourStatus(string csv_values)
        {
            if (csv_values.Equals("inPreparation"))
            {
                Status = TourStatus.inPreparation;
            }
            else if (csv_values.Equals("Active"))
            {
                Status = TourStatus.Active;
            }
            else if (csv_values.Equals("Finnished"))
            {
                Status = TourStatus.Finnished;
            }
            else if (csv_values.Equals("Canceled"))
            {
                Status = TourStatus.Canceled;
            }
            else if (csv_values.Equals("gotGuide"))
            {
                Status = TourStatus.gotGuide;
            }
        }
    }
}
