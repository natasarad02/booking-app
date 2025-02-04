﻿using BookingApp.Domain.Model.Rates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel
{
    public class RenovationRecommendationViewModel : INotifyPropertyChanged
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

        private int reservationId;
        public int ReservationId
        {
            get { return reservationId; }
            set
            {
                if (reservationId != value)
                {
                    reservationId = value;
                    OnPropertyChanged("ReservationId");
                }
            }
        }

        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }
            set
            {
                if (accommodationId != value)
                {
                    accommodationId = value;
                    OnPropertyChanged("AccommodationId");
                }
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }

        private RecommendationLevel level;
        public RecommendationLevel Level
        {
            get { return level; }
            set
            {
                if (level != value)
                {
                    level = value;
                    OnPropertyChanged("Level");
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RenovationRecommendationViewModel() { }

        public RenovationRecommendationViewModel(RenovationRecommendation ren)
        {
            Id = ren.Id;
            ReservationId = ren.ReservationId;
            AccommodationId = ren.AccommodationId;
            Comment = ren.Comment;
            Level = ren.Level;
        }

        public RenovationRecommendation ToRecommendation()
        {
            RenovationRecommendation r = new RenovationRecommendation(reservationId, accommodationId, level, comment);
            r.Id = Id;
            return r;
        }
    }
}
