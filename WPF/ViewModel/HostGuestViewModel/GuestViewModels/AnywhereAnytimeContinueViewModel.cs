﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AnywhereAnytimeContinueViewModel: IObserver
    {
        public ObservableCollection<AccommodationViewModel> Accommodations { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }

        public AccommodationService AccommodationService { get; set; }

        public User User { get; set; }
        public Frame Frame { get; set; }

        public int DayNumber { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestNumber { get; set; }

        public string DateRange {get; set; }

        public GuestICommand<object> ReserveCommand { get; set; }

        public AnywhereAnytimeContinueViewModel(User user, Frame frame, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
            User = user;
            Frame = frame;
            DayNumber = dayNumber;
            StartDate = startDate;
            GuestNumber = guestNumber;
            EndDate = endDate;
            Accommodations = new ObservableCollection<AccommodationViewModel>();
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            ReserveCommand = new GuestICommand<object>(OnReserve);
            DateRange = StartDate.ToString("MM/dd/yyyy") + " -> " + EndDate.ToString("MM/dd/yyyy");
            Update();
            
        }

        private void OnReserve(object sender)
        {
           
                Button button = sender as Button;
                SelectedAccommodation = button.DataContext as AccommodationViewModel;
                Frame.Content = new AnywhereAnytimeCalendarPage(User, Frame, DayNumber, GuestNumber, StartDate, EndDate, SelectedAccommodation);
            
        }

        public void Update()
        {
           Accommodations.Clear();
           List<Accommodation> tmpAccommodations = AccommodationService.FindAvailableAccommodations(DayNumber, GuestNumber, StartDate, EndDate);
           foreach(Accommodation accommodation in tmpAccommodations)
            {
                Accommodations.Add(new AccommodationViewModel(accommodation));
            }
           
        }
    }
}
