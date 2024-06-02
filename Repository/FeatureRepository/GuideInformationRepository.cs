using BookingApp.Domain.Model.Features;
using BookingApp.Serializer;
using BookingApp.WPF.View.GuideTestWindows.GuideControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    class GuideInformationRepository
    {
        private const string FilePath = "../../../Resources/Data/guide_info.csv";
        private readonly Serializer<GuideInformation> _serializer;
        private List<GuideInformation> _guideInformation;

        public GuideInformationRepository()
        {
            _serializer = new Serializer<GuideInformation>();
            _guideInformation = _serializer.FromCSV(FilePath);
        }

        public List<GuideInformation> GetAll()
        {
            _guideInformation = _serializer.FromCSV(FilePath);
            return _guideInformation;
        }

        public GuideInformation GetByGuideId(int id)
        {
            return _guideInformation.Find(x => x.UserId ==  id);
        }

        public void save(GuideInformation guide)
        {
            _guideInformation = _serializer.FromCSV(FilePath);
            GuideInformation current = _guideInformation.Find(t => t.Id == guide.Id);
            int index = _guideInformation.IndexOf(current);
            _guideInformation.Remove(current);
            _guideInformation.Insert(index, guide);
            _serializer.ToCSV(FilePath, _guideInformation);
        }
    }
}
