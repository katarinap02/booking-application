using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.RateRepository
{
    public class GuideRateRepository : IGuideRateRepository
    {
        private const string FilePath = "../../../Resources/Data/guide_rate.csv";

        private readonly Serializer<GuideRate> _serializer;

        private List<GuideRate> _guideRates;

        public GuideRateRepository()
        {
            _serializer = new Serializer<GuideRate>();
            _guideRates = GetAll();
        }

        public List<GuideRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(GuideRate guideRate)
        {
            guideRate.Id = NextId();
            _guideRates.Add(guideRate);
            _serializer.ToCSV(FilePath, _guideRates);
        }

        public int NextId()
        {
            _guideRates = GetAll();
            if (_guideRates.Count < 1)
                return 1;
            return _guideRates.Max(g => g.Id) + 1;
        }

        public bool IsRated(int tourId)
        {
            List<GuideRate> rates = GetAll();
            if (rates.FindAll(r => r.TourId == tourId).Count != 0)
            {
                return true;
            }
            return false;
        }

        public List<GuideRate> getRatesForGuide(int guide_id)
        {
            List < GuideRate > rates = GetAll();
            return rates.FindAll(r => r.GuideId == guide_id);
        }

        public List<GuideRate> getRatesForTour(int tour_id)
        {
            List<GuideRate> rates = GetAll();
            return rates.FindAll(r => r.TourId == tour_id);
        }

        public GuideRate getById(int id)
        {
            return _guideRates.Find(g => g.Id == id);
        }

        public void markAsInvalid(int id)
        {
            GuideRate guideRate = getById(id);
            guideRate.IsValid = false;
            _serializer.ToCSV(FilePath, _guideRates);
        }

    }
}
