using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace BookingApp.Domain.Model.Reservations
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DateRange => StartDate.ToString() + "-" + EndDate.ToString();

        public int NumberOfPeople { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public DateTime DateCreated { get; set; }


        public AccommodationReservation() { }

        public AccommodationReservation(int guestId, int accommodationId, DateTime startDate, DateTime endDate, int numberOfPeople, string name, string city, string country, DateTime dateCreated)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfPeople = numberOfPeople;
            Name = name;
            City = city;
            Country = country;
            DateCreated = dateCreated;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                GuestId.ToString(),
                AccommodationId.ToString(),
                DateRange,
                NumberOfPeople.ToString(),
                Name,
                City,
                Country,
                DateCreated.ToString()

            };
            return csvValues;

        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            string[] dateParts = values[3].Split('-');
            StartDate = DateTime.Parse(dateParts[0]);
            EndDate = DateTime.Parse(dateParts[1]);
            NumberOfPeople = Convert.ToInt32(values[4]);
            Name = values[5];
            City = values[6];
            Country = values[7];
            DateCreated = Convert.ToDateTime(values[8]);

        }

    }
}
