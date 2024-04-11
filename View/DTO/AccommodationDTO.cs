﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using BookingApp.Model;
using static System.Net.Mime.MediaTypeNames;


namespace BookingApp.DTO
{
    public class AccommodationDTO : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {

                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {

                    city = value;
                    OnPropertyChanged("City");
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (country != value)
                {

                    country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        

        private AccommodationType type;
        public AccommodationType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private bool isCheckedHouse;
        public bool IsCheckedHouse
        {
            get { return isCheckedHouse; }
            set
            {
                if (isCheckedHouse != value)
                {
                    isCheckedHouse = value;
                    OnPropertyChanged("IsCheckedHouse");
                }
            }
        }

        private bool isCheckedCottage;
        public bool IsCheckedCottage
        {
            get { return isCheckedCottage; }
            set
            {
                if (isCheckedCottage != value)
                {
                    isCheckedCottage = value;
                    OnPropertyChanged("IsCheckedCottage");
                }
            }
        }

        private int maxGuestNumber;
        public int MaxGuestNumber
        {
            get { return maxGuestNumber; }
            set
            {
                if (maxGuestNumber != value)
                {
                    
                    maxGuestNumber = value;
                    OnPropertyChanged("MaxGuestNumber");
                }
            }
        }

        private int minReservationDays;
        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if (minReservationDays != value)
                {
                    
                    minReservationDays = value;
                    OnPropertyChanged("MinReservationNumber");
                }
            }
        }

        private int reservationDaysLimit;
        public int ReservationDaysLimit
        {
            get { return reservationDaysLimit; }
            set
            {
                if (reservationDaysLimit != value)
                {
                    
                    reservationDaysLimit = value;
                    OnPropertyChanged("ReservationDaysLimit");
                }
            }
        }

        


        private List<string> picture = new List<string>();
        public List<string> Picture
        {
            get { return picture; }
            set
            {
                if (picture != value)
                {
                    picture = value;
                    OnPropertyChanged("Images");
                }
            }
        }

        private string onePicture;
        public string OnePicture
        {
            get { return onePicture; }
            set
            {
                if (onePicture != value)
                {

                    onePicture = value;
                    OnPropertyChanged("OnePicture");
                }
            }
        }


        private List<CalendarDateRange> unavailableDates = new List<CalendarDateRange>();
        public List<CalendarDateRange> UnavailableDates
        {
            get { return unavailableDates; }
            set
            {
                if (unavailableDates != value)
                {
                    unavailableDates = value;
                    OnPropertyChanged(nameof(UnavailableDates));
                }
            }
        }

        private int hostId;
        public int HostId
        {
            get { return hostId; }
            set
            {
                if (hostId != value)
                {
                    hostId = value;
                    OnPropertyChanged("HostId");
                }
            }
        }

        private bool isSuperHost;
        public bool IsSuperHost
        {
            get { return isSuperHost; }
            set
            {
                if (isSuperHost != value)
                {
                    isSuperHost = value;
                    OnPropertyChanged("IsSuperHost");
                }
            }
        }



        public string Location => City + ", " + Country;
        public string Error => null;

        
        private Regex _NumberRegex = new Regex("^[0-9]+$");
        private Regex _ImageRegex = new Regex("^(?:https://(?:[^,]*,)?)*https://(?:[^,]+)?(?:,(?=https://)[^,]*)*$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Name is required";

                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "City is required";

                }
                
                else if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Country is required";

                }
                else if (columnName == "MinReservationDays")
                {
                    if (MinReservationDays < 0)
                        return "Min reservation days must be greater than 0";
                }
                else if (columnName == "MaxGuestNumber")
                {
                    if (MaxGuestNumber < 0)
                        return "Max guest number must be greater than 0";
                }
                else if (columnName == "ReservationDaysLimit")
                {
                    
                    if (ReservationDaysLimit < 0)
                        return "Reservation days limit must be greater than 0";
                }
                

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "City", "Name", "Country", "MinReservationDays", "MaxGuestNumbe", "ReservationDaysLimit" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }



        public string ConvertToRelativePath(string inputPath)
    {
        
        string pattern = @"\\";

        
        string replacedPath = Regex.Replace(inputPath, pattern, "/");

        
        if (replacedPath.StartsWith("Resources/Images/"))
        {
            replacedPath = "../../" + replacedPath;
        }

        return replacedPath;
    }




    public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AccommodationDTO()
        {
            reservationDaysLimit = 1;
        }

        public AccommodationDTO(Accommodation accommodation)
        {
            id = accommodation.Id;
            name = accommodation.Name;
            city = accommodation.City;
            country = accommodation.Country;
            type = accommodation.Type;
            MaxGuestNumber = accommodation.MaxGuestNumber;
            MinReservationDays = accommodation.MinReservationDays;
            ReservationDaysLimit = accommodation.ReservationDaysLimit;
            UnavailableDates = accommodation.UnavailableDates;

            hostId = accommodation.HostId;

            if(accommodation.Pictures.Count != 0) {
                OnePicture = ConvertToRelativePath(accommodation.Pictures[0]);
            }
            else
            {
                OnePicture = "../../Resources/Images/no_image.jpg";
            }
            




        }

        public Accommodation ToAccommodation()
        {
            type = GetAccommodationType();
            
            Accommodation a = new Accommodation(name, country, city, type, maxGuestNumber, minReservationDays, reservationDaysLimit, hostId);
            a.Id = id;
            a.UnavailableDates = unavailableDates;
            a.Pictures = picture;
            

            
            

            return a;

        }

        public AccommodationType GetAccommodationType()
        {
            AccommodationType type = AccommodationType.APARTMENT;

            if (isCheckedHouse)
            { type = AccommodationType.HOUSE; }
            else if (isCheckedCottage)
            {
                type = AccommodationType.COTTAGE;
            }

            return type;
        }
    }

   
}
