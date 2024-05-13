using BookingApp.Domain.Model.Features;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TouristNotificationService
    {
        private readonly ITouristNotificationRepository _touristNotificationRepository;
        private readonly TourService _tourService;

        public TouristNotificationService(ITouristNotificationRepository touristNotificationRepository)
        {
            _touristNotificationRepository = touristNotificationRepository;
            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
        }

        public List<TouristNotification> GetMyNotifications(int touristId)
        {
            return _touristNotificationRepository.GetMyNotifications(touristId);
        }

        public void Add(TouristNotification touristNotification)
        {
            _touristNotificationRepository.Add(touristNotification);
        }
        public Tour GetTourById(int TourId)
        {
            return _tourService.GetTourById(TourId);
        }
    }
}
