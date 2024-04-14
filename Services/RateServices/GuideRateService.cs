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
    class GuideRateService
    {
        private readonly GuideRateRepository guideRateRepository;

        public GuideRateService()
        {
            guideRateRepository = new GuideRateRepository();
        }

        public void SaveRate(GuideRateViewModel rate)
        {
            guideRateRepository.Add(rate.toGuideRate());
        }

        public bool IsRated(int tourId)
        {
            return guideRateRepository.IsRated(tourId);
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
