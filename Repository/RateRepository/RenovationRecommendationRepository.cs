using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace BookingApp.Repository.RateRepository
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovation_recommendations.csv";
        private readonly Serializer<RenovationRecommendation> _serializer;
        private List<RenovationRecommendation> _recommendations;

        public Subject RecommendationSubject { get; set; }
        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<RenovationRecommendation>();
            _recommendations = _serializer.FromCSV(FilePath);
            RecommendationSubject = new Subject();
        }

        public int NextId()
        {
            _recommendations = _serializer.FromCSV(FilePath);
            if (_recommendations.Count < 1)
                return 1;

            return _recommendations.Max(a => a.Id) + 1;
        }
        public RenovationRecommendation Add(RenovationRecommendation recommendation)
        {
            recommendation.Id = NextId();
            _recommendations = _serializer.FromCSV(FilePath);
            _recommendations.Add(recommendation);
            _serializer.ToCSV(FilePath, _recommendations);
            RecommendationSubject.NotifyObservers();
            return recommendation;
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public RenovationRecommendation GetById(int id)
        {
            foreach (RenovationRecommendation recommendation in _recommendations)
            {
                if (recommendation.Id == id) return recommendation;
            }

            return null;
        }
    }
}
