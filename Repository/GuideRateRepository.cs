using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuideRateRepository
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
}
}
