using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Repository.FeatureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.FeatureServices
{   
    class GuideInfoService
    {
        private readonly GuideInformationRepository informationRepository;

        public GuideInfoService()
        {
            informationRepository = new GuideInformationRepository();
        }

        public GuideInformation GetByGuideId(int id)
        {
            return informationRepository.GetByGuideId(id);
        }

        public void quit(int id)
        {
            GuideInformation guide = GetByGuideId(id);
            // pronadji sve ture za guide-a
            // cancel-uj ih -> status cancel
            // podeli vaucere
            // HasQuit = true
            // dodati proveru u SignIn da li je obrisan
        }
    }
}
