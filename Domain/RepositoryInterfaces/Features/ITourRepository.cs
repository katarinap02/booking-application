using BookingApp.Domain.Model.Features;
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

        List<Tour> GetAllNotFinished();
        void Add(Tour tour);
        int NextPersonalId();
        int NextId();
        List<Tour>? SearchTours(Tour searchCriteria);
        List<Tour> GetTourByCityWithAvailablePlaces(string city);
        List<Tour>? findToursNeedingGuide();
        Tour? UpdateAvailablePlaces(Tour tour, int reducer);
        Tour? GetTourById(int id);
        int ToursCount();
        int FindMaxNumberOfParticipants();
        void finnishTour(int id);
        void activateTour(int id);
        void nextCheckpoint(int id);
        List<Tour> findToursByGuideId(int guideId);
        void save(Tour tour);
        List<Tour> findFinnishedToursByGuide(int guide_id);
        bool isTourFinished(int tourId);
        int FindMaxNumberOfParticipants(List<Tour> tours);
        List<string> GetCheckpointsByTour(int tourId);
    }
}
