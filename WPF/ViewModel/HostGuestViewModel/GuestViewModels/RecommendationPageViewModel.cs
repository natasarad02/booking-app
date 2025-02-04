﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RecommendationPageViewModel : INotifyPropertyChanged
    {
        public User User { get; }
        public Frame Frame { get; }
        public AccommodationReservationViewModel SelectedReservation { get; }
        public AccommodationViewModel SelectedAccommodation { get; }
        public AccommodationRateViewModel AccommodationRate { get; }

        public RenovationRecommendationService RenovationRecommendationService { get; set;  }
        public HostService HostService { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }
        public RenovationRecommendationViewModel Recommendation {  get; set; }
        public RecommendationPage Page { get; set; }
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

                SaveRateCommand.RaiseCanExecuteChanged();
               
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // KOMANDE
        public GuestICommand SaveRateCommand { get; set; }
        public GuestICommand BackCommand { get; set; }
        public NavigationService NavigationService { get; set; }
        public RecommendationPageViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation, AccommodationViewModel selectedAccommodation, AccommodationRateViewModel accommodationRate, RecommendationPage page)
        {
            User = user;
            Frame = frame;
            SelectedReservation = selectedReservation;
            SelectedAccommodation = selectedAccommodation;
            AccommodationRate = accommodationRate;
            Recommendation = new RenovationRecommendationViewModel();
            AccommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
            SaveRateCommand = new GuestICommand(OnSaveRate, CanSaveRate);
            BackCommand = new GuestICommand(OnBack);
            NavigationService = Frame.NavigationService;
            Page = page;
        }

        private void OnBack()
        {
            NavigationService.GoBack();
        }

        private bool CanSaveRate()
        {
            ToggleCommentValidationMessage();
            if (string.IsNullOrEmpty(Comment))
            {
                
                return false;
            }
            else
            {
                
                return true;
            }
        }

        private void ToggleCommentValidationMessage()
        {
            if (string.IsNullOrEmpty(Comment))
            {

                Page.commentValidator.Visibility = Visibility.Visible;
                var showHint = (Storyboard)Page.FindResource("ShowTextBlock");
                showHint.Begin(Page.commentValidator);

                Page.txtComment.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Page.txtComment.BorderThickness = new Thickness(2);
            }
            else
            {

                var hideHint = (Storyboard)Page.FindResource("HideTextBlock");
                hideHint.Completed += (s, a) => Page.commentValidator.Visibility = Visibility.Hidden;
                hideHint.Begin(Page.commentValidator);
                Page.txtComment.BorderBrush = SystemColors.ControlDarkBrush;
                Page.txtComment.BorderThickness = new Thickness(1);
            }
        }

        private void OnSaveRate()
        {
            Recommendation.Comment = Comment;
            Recommendation.ReservationId = SelectedReservation.Id;
            Recommendation.AccommodationId = SelectedAccommodation.Id;
            RenovationRecommendationService.Add(Recommendation.ToRecommendation());
            AccommodationRate.RecommendationId = RenovationRecommendationService.GetAll().Last().Id;
            AccommodationRateService.Add(AccommodationRate.ToAccommodationRate());
            AccommodationRate rate = AccommodationRate.ToAccommodationRate();
            Host host = HostService.GetById(rate.HostId);
            Frame.Content = new RateAccommodationSuccessfulPage(User, Frame, SelectedAccommodation, host.Username);
        }

      
    }
}
