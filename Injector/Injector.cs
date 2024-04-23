using BookingApp.Repository;
using BookingApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Domain.Model.Features;
using BookingApp.Repository.FeatureRepository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.Repository.RateRepository;

namespace BookingApp.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
        {
            { typeof(IUserRepository), new UserRepository() },
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IAccommodationRateRepository), new AccommodationRateRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository() },
            { typeof(IDelayRequestRepository), new DelayRequestRepository() },
            { typeof(IReservationCancellationRepository), new ReservationCancellationRepository() },

            { typeof(IGuestRateRepository), new GuestRateRepository() },
            { typeof(IHostRepository), new HostRepository() },

            { typeof(ITouristNotificationRepository), new TouristNotificationRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(ITouristRepository), new TouristRepository() },
            { typeof(ITourParticipantRepository), new TourParticipantRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(IGuideRateRepository), new GuideRateRepository() },
            { typeof(IRenovationRecommendationRepository), new RenovationRecommendationRepository() },
            { typeof(IGuestRepository), new GuestRepository() }

            /*{ typeof(IUserService), new UserService() },*/
            // Add more implementations here
            
    };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
