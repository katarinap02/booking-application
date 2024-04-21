﻿using BookingApp.Domain.Model.Features;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TouristNotificationService
    {
        private readonly TouristNotificationRepository _touristNotificationRepository;

        public TouristNotificationService()
        {
            _touristNotificationRepository = new TouristNotificationRepository();
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
