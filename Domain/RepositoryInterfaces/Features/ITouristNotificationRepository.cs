using BookingApp.Domain.Model.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Features
{
    public interface ITouristNotificationRepository
    {
        List<TouristNotification> GetAll();
        List<TouristNotification> GetMyNotifications(int touristId);
        void Add(TouristNotification notification);
        int NextId();
    }
}
