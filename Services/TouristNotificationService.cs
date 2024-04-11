using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TouristNotificationService
    {
        private readonly TouristNotificationRepository _touristNotificationRepository;

        public TouristNotificationService()
        {
            _touristNotificationRepository = new TouristNotificationRepository();
        }

        public List<TouristNotificationViewModel> GetAllNotifications()
        {
            return ToTouristNotificationViewModel(_touristNotificationRepository.GetAllReversed());
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
