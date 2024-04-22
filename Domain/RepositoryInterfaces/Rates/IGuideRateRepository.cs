using BookingApp.Domain.Model.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Rates
{
    public interface IGuideRateRepository
    {
        List<GuideRate> GetAll();
        void Add(GuideRate guideRate);
        int NextId();
        bool IsRated(int tourId);
        List<GuideRate> getRatesForGuide(int guide_id);
        List<GuideRate> getRatesForTour(int tour_id);
        GuideRate getById(int id);
        void markAsInvalid(int id);
    }
}
