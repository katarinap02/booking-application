using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.RateServices
{
    public class RenovationRecommendationService
    {
        private readonly IRenovationRecommendationRepository RenovationRecommendationRepository;

        public RenovationRecommendationService(IRenovationRecommendationRepository renovationRecommendationRepository)
        {
            this.RenovationRecommendationRepository = renovationRecommendationRepository;
        }

        public RenovationRecommendation Add(RenovationRecommendation recommendation)
        {
            return RenovationRecommendationRepository.Add(recommendation);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return RenovationRecommendationRepository.GetAll();
        }

        public RenovationRecommendation GetById(int id)
        {
            return RenovationRecommendationRepository.GetById(id);
        }
    }
}
