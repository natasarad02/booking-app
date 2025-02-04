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
    /// Interaction logic for ProfileViewForum.xaml
    /// </summary>
    public partial class ProfileViewForum : Page
    {
        public User User { get; set; }

        public Frame Frame { get; set; }

        public ForumViewModel SelectedForum { get; set; }

        public ProfileViewForumViewModel ViewModel { get; set; }

        
        public ProfileViewForum(User user,Frame frame, ForumViewModel selectedForum)
        {
            InitializeComponent();
            User = user;
            Frame = frame;
            SelectedForum = selectedForum;
            ViewModel = new ProfileViewForumViewModel(User, Frame, SelectedForum);
            DataContext = ViewModel;

            if (SelectedForum.IsClosed)
            { postCommentSection.IsEnabled = false; closedMessage.Visibility = Visibility.Visible; }
            else
            { postCommentSection.IsEnabled = true; closedMessage.Visibility = Visibility.Hidden; }
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
