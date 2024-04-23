
using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository.RateRepository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.RateServices
{
    public class GuideRateService
    {
        private readonly IGuideRateRepository _guideRateRepository;

        private readonly TourReservationService _tourReservationService;
        private readonly TourService _tourService;
        private TouristService _touristService;

        public GuideRateService(IGuideRateRepository guideRateRepository)
        {
            _guideRateRepository = guideRateRepository;

            _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
            _touristService = new TouristService(Injector.Injector.CreateInstance<ITouristRepository>());
            _tourReservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>());
        }

        public void SaveRate(GuideRateViewModel rate)
        {
            _guideRateRepository.Add(rate.toGuideRate());
        }

        public bool IsRated(int tourId)
        {
            return _guideRateRepository.IsRated(tourId);
        }

        public bool CanBeRated(int tourId, int userId)
        {
            // is tour finished
            if (_tourService.isTourFinished(tourId))
            {
                // is my tour
                Tourist tourist = _touristService.GetTouristById(userId);
                return _tourReservationService.FindMyEndedTours(userId, tourist.Name, tourist.LastName).Any(t => t.Id == tourId);
            }
            return false;
        }

        public List<GuideRateViewModel> getRatesByTour(int tour_id)
        {
            List<GuideRateViewModel> rates = new List<GuideRateViewModel>();
            List<GuideRate> guideRates = _guideRateRepository.getRatesForTour(tour_id);
            foreach (var guideRate in guideRates)
            {
                rates.Add(new GuideRateViewModel(guideRate));
            }

            return rates;
        }
        
        public void markAsInvalid(int id)
        {
            _guideRateRepository.markAsInvalid(id);
        }

    }
}
