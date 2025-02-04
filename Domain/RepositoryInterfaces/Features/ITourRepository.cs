﻿using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        List<Tour> GetAllNotFinishedAndNotCancelled();
        void Add(Tour tour);
        void Save();
        int NextPersonalId();
        int NextId();
        List<Tour>? SearchTours(Tour searchCriteria);
        List<Tour> GetTourByCityWithAvailablePlaces(string city);
        Tour? GetTourById(int id);
        int ToursCount();
        void finnishTour(int id);
        void activateTour(int id);
        void nextCheckpoint(int id);
        List<Tour> findToursByGuideId(int guideId);
        void save(Tour tour);
        List<Tour> findFinnishedToursByGuide(int guide_id);
        bool isTourFinished(int tourId);
        List<string> GetCheckpointsByTour(int tourId);
    }
}
