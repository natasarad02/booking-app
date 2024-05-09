﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class RateFormViewModel : IObserver, INotifyPropertyChanged
    {
        public AccommodationReservationViewModel SelectedReservation { get; set; }
        public AccommodationViewModel SelectedAccommodation { get; set; }
        public User User { get; set; }
        public AccommodationService AccommodationService { get; set; }

        public HostService HostService { get; set; }

        public Frame Frame { get; set; }

        public ObservableCollection<string> Images { get; set; }

        public AccommodationRateViewModel AccommodationRate { get; set; }

        public AccommodationRateService AccommodationRateService { get; set; }

        public RateAccommodationForm Page { get; set; }

        private int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (cleanliness != value)
                {

                    cleanliness = value;
                    OnPropertyChanged("Cleanliness");

                }

                SaveRateCommand.RaiseCanExecuteChanged();
                RecommendCommand.RaiseCanExecuteChanged();
            }
        }

        private int correctness;
        public int Correctness
        {
            get { return correctness; }
            set
            {
                if (correctness != value)
                {

                    correctness = value;
                    OnPropertyChanged("Correctness");

                }

                SaveRateCommand.RaiseCanExecuteChanged();
                RecommendCommand.RaiseCanExecuteChanged();
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

                SaveRateCommand.RaiseCanExecuteChanged();
                RecommendCommand.RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // KOMANDE
        public GuestICommand AddPicturesCommand { get; set; }
        public GuestICommand SaveRateCommand { get; set; }
        public GuestICommand RecommendCommand { get; set; }

        public RateFormViewModel(User user, Frame frame, AccommodationReservationViewModel selectedReservation, RateAccommodationForm page)
        {
            Images = new ObservableCollection<string>();
            AccommodationRateService = new AccommodationRateService(Injector.Injector.CreateInstance<IAccommodationRateRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            AccommodationService = new AccommodationService(Injector.Injector.CreateInstance<IAccommodationRepository>());
            HostService = new HostService(Injector.Injector.CreateInstance<IHostRepository>(), Injector.Injector.CreateInstance<IAccommodationRateRepository>());
            AccommodationRate = new AccommodationRateViewModel();
            User = user;
            Frame = frame;
            Page = page;
            SelectedReservation = selectedReservation;
            SelectedAccommodation = new AccommodationViewModel(AccommodationService.GetById(SelectedReservation.AccommodationId));
            AddPicturesCommand = new GuestICommand(OnAddPictures);
            SaveRateCommand = new GuestICommand(OnSaveRate, CanSaveOrRecommend);
            RecommendCommand = new GuestICommand(OnRecommend, CanSaveOrRecommend);
            


        }

        private bool CanSaveOrRecommend()
        {
            if (Cleanliness == 0 || Correctness == 0 || string.IsNullOrEmpty(Comment))
                return false;
            else
                return true;
        }

        private void OnRecommend()
        {
            CreateRate();
            Frame.Content = new RecommendationPage(User, Frame, SelectedReservation, SelectedAccommodation, AccommodationRate);
        }

        private void OnSaveRate()
        {
            CreateRate();
          
            AccommodationRateService.Add(AccommodationRate.ToAccommodationRate());
            AccommodationRate rate = AccommodationRate.ToAccommodationRate();
            Host host = HostService.GetById(rate.HostId);
            //MessageBox.Show(host.Id.ToString());
            HostService.BecomeSuperHost(host);
            Frame.Content = new RateAccommodationSuccessfulPage(User, Frame, SelectedAccommodation, host.Username);
        }

        private void OnAddPictures()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                try
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(selectedFileName));

                    string imageUrl = selectedFileName;

                    imageUrl = ConvertToRelativePath(imageUrl);
                    AddPicture(imageUrl);


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
        }

       
        private void CreateRate()
        {
            AccommodationRate.Cleanliness = Cleanliness;
            AccommodationRate.Correctness = Correctness;
            AccommodationRate.AdditionalComment = Comment;
            AccommodationRate.ReservationId = SelectedReservation.Id;
            AccommodationRate.GuestId = User.Id;
            AccommodationRate.HostId = AccommodationService.GetById(SelectedReservation.AccommodationId).HostId;
            AccommodationRate.RecommendationId = -1;
            
        }

    
        public string ConvertToRelativePath(string input)
        {
            int index = input.IndexOf("Resources");
            if (index != -1)
            {
                return input.Substring(index);
            }
            else
            {
                MessageBox.Show("Please select an image from the resources provided within this app!");
                return input;
            }
        }

        private void AddPicture(string pictureUrl)
        {
            if (!string.IsNullOrEmpty(pictureUrl))
            {

                AccommodationRate.Images.Add(pictureUrl);

                Update();

            }
        }

        public void Update()
        {
            Images.Clear();
            foreach (string image in AccommodationRate.Images)
            {
                Images.Add(image);
            }
        }

       
    }
}
