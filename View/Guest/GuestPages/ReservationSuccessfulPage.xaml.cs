﻿using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.View.ViewModel;
using BookingApp.ViewModel;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for AccommodationSuccessfulPage.xaml
    /// </summary>
    public partial class ReservationSuccessfulPage : Page
    {
      
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public ReservationSuccessfulViewModel ViewModel { get; set; }
       
        public AccommodationReservationViewModel Reservation { get; set; }
        public CalendarDateRange SelectedDateRange { get; set; }
        public User User { get; set; }

        public Frame Frame { get; set; }

        public int GuestNumber { get; set; }
        public ReservationSuccessfulPage(AccommodationReservationViewModel reservation, AccommodationViewModel selectedAccommodation, CalendarDateRange selectedDateRange, int guestNumber, User user, Frame frame)
        {
            InitializeComponent();

            
            this.SelectedAccommodation = selectedAccommodation;
            this.SelectedDateRange = selectedDateRange;
            this.GuestNumber = guestNumber;
            this.User = user;
            this.Frame = frame;
            Reservation = reservation;

            ViewModel = new ReservationSuccessfulViewModel(SelectedAccommodation, User, Frame);
            DataContext = Reservation;

          
            
            
        }


        private void HomePage_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.HomePage_Click(sender, e);

        }

        private void ProfilePage_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.ProfilePage_Click(sender, e);

        }
    }
}
