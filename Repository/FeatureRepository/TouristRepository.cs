using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.Repository.FeatureRepository
{
    public class TouristRepository : ITouristRepository
    {
        private const string FilePath = "../../../Resources/Data/tourists.csv";

        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;

        public TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourists = _serializer.FromCSV(FilePath);
        }

        public List<Tourist> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tourist FindTouristById(int touristId)
        {
            return _tourists.Find(t => t.Id == touristId);
        }

    }
}
