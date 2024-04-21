using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    class GuideRateService
    {
        private readonly GuideRateRepository guideRateRepository;
        private readonly TourReservationRepository tourReservationRepository;
        private readonly TourRepository tourRepository;

        public GuideRateService()
        {
            guideRateRepository = new GuideRateRepository();
            tourReservationRepository = new TourReservationRepository();
            tourRepository = new TourRepository();
        }

        public void SaveRate(GuideRateViewModel rate)
        {
            guideRateRepository.Add(rate.toGuideRate());
        }

        public bool IsRated(int tourId)
        {
            return guideRateRepository.IsRated(tourId);
        }

        public bool CanBeRated(int tourId, int userId)
        {
            // is tour finished
            if (tourRepository.isTourFinished(tourId))
            {
                // is my tour
                return tourReservationRepository.FindMyEndedTours(userId).Any(t => t.Id == tourId);
            }
            return false;
        }
        
        public List<GuideRateViewModel> getRatesByTour(int tour_id)
        {
            List < GuideRateViewModel > rates = new List < GuideRateViewModel >();
            List <GuideRate> guideRates = guideRateRepository.getRatesForTour(tour_id);
            foreach (var guideRate in guideRates)
            {
                rates.Add(new GuideRateViewModel(guideRate));
            }

            return rates;
        }

    }
}
