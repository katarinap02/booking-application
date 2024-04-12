using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourist_notifications.csv";

        private readonly Serializer<TouristNotification> _serializer;

        private List<TouristNotification> _notifications;

        public TouristNotificationRepository()
        {
            _serializer = new Serializer<TouristNotification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public List<TouristNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<TouristNotification> GetMyNotifications(int touristId)
        {
            List<TouristNotification> notifications = GetAll();
            notifications = notifications.FindAll(n => n.TouristId == touristId);
            notifications.Reverse();
            return notifications;
        }
        public void Add(TouristNotification notification)
        {
            notification.Id = NextId();
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public int NextId()
        {
            _notifications = GetAll();
            if (_notifications.Count < 1)
                return 1;
            return _notifications.Max(n => n.Id) + 1;
        }

    }
}
