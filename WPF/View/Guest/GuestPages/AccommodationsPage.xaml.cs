﻿using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using BookingApp.Observer;
using System.Security.Cryptography;
using BookingApp.Application.Services;
using BookingApp.WPF.ViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
using BookingApp.Domain.Model.Features;
using System.Windows.Media.Animation;
using Wpf.Ui.Controls;

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for AccommodationsPage.xaml
    /// </summary>
    public partial class AccommodationsPage : Page
    {
        

        public User User { get; set; }
    
        public Frame Frame { get; set; }    

        public ShowAccommodationsViewModel ViewModel { get; set; }
      

        public AccommodationsPage(User user, Frame frame)
        {
            InitializeComponent();

           
            this.User = user;
            //AccommodationsDataGrid.ItemsSource = Accommodations;
            DataContext = this;
            this.Frame = frame;
            ViewModel = new ShowAccommodationsViewModel(User, Frame, this);
            DataContext = ViewModel;
        
            ViewModel.Update();
            Loaded += AccommodationPage_Loaded;
          
        }

        private async void AccommodationPage_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }



    }
}
