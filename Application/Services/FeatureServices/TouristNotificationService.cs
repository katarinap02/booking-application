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

        public TouristNotificationService(ITouristNotificationRepository touristNotificationRepository)
        {
            _touristNotificationRepository = touristNotificationRepository;
        }

        public List<TouristNotificationViewModel> GetMyNotifications(int touristId)
        {
            return ToTouristNotificationViewModel(_touristNotificationRepository.GetMyNotifications(touristId));
        }

        public List<TouristNotificationViewModel> ToTouristNotificationViewModel(List<TouristNotification> touristNotifications)
        {
            List<TouristNotificationViewModel> NotificaionViewModel = new List<TouristNotificationViewModel>();
            foreach (TouristNotification notification in touristNotifications)
            {
                NotificaionViewModel.Add(new TouristNotificationViewModel(notification));
            }
            return NotificaionViewModel;
        }

    }
}
