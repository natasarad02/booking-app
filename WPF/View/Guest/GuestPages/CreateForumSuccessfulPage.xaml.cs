﻿using BookingApp.Domain.Model.Features;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Guest.GuestPages
{
    /// <summary>
    /// Interaction logic for CreateForumSuccessfulPage.xaml
    /// </summary>
    public partial class CreateForumSuccessfulPage : Page
    {
        public User User { get; set; }
        public Frame Frame { get; set; }

        public ForumViewModel NewForum { get; set; }
        public CreateForumSuccessfulViewModel ViewModel { get; set; }
        public CreateForumSuccessfulPage(User user, Frame frame, ForumViewModel newForum)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            NewForum = newForum;
            ViewModel = new CreateForumSuccessfulViewModel(User, Frame, NewForum);
            DataContext = ViewModel;
            Loaded += Page_Loaded;

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));


            Frame.BeginAnimation(Frame.OpacityProperty, fadeInAnimation);

            await Task.Delay(500);
        }
    }
}
