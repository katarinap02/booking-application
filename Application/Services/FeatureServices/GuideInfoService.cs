using BookingApp.Application.Services.RateServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Repository.FeatureRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.Services.FeatureServices
{   
    public class GuideInfoService
    {
        private readonly GuideInformationRepository informationRepository;
        private readonly GuideRateService guideRateService;
        private readonly TourService tourService;

        public GuideInfoService()
        {
            informationRepository = new GuideInformationRepository();
            guideRateService = new GuideRateService(Injector.Injector.CreateInstance<IGuideRateRepository>());
            tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
        }

        public GuideInformation GetByGuideId(int id)
        {
            GuideInformation guideInformation =  informationRepository.GetByGuideId(id);
            guideInformation.AverageGrade = getAverageGrade(id);
            guideInformation.TourNumber = tourService.GetNonCanceledToursGyGuide(id);
            guideInformation.MostUsedLanguage = tourService.FindMostUsedLanguageForGuide(id);
            return guideInformation;
        }

        // ako je sad superGuide, proveriti
        public void UpdateSuperGuide(int id)
        {
            GuideInformation guideInformation = informationRepository.GetByGuideId(id);

            bool isSuperGuideExpired = guideInformation.Status == GuideStatus.Super && DateTime.Now >= guideInformation.EndSuperGuide;

            if (isSuperGuideExpired || guideInformation.Status != GuideStatus.Super)
            {
                CheckSuperGuide(id);
            }
        }


        public void CheckSuperGuide(int id)
        {
            List<string> languages = tourService.FindLanguagesWithMoreThan20ToursInPastYear(id); // poslednjih 365 dana + preko 20 tura
            if (languages.Count > 0)
            {
                foreach(string language in languages)
                {                    
                    List<Tour> tours = tourService.findToursByLanguageAndGuide(language, id); 
                    if(guideRateService.findAverageRateByTours(tours) > 4.0) // prosek preko 4
                    {
                        GuideInformation guideInformation = informationRepository.GetByGuideId(id);
                        guideInformation.Status = GuideStatus.Super;
                        guideInformation.PreviousSuperGuides.Add("SuperGuide for " + language + " " + DateTime.Now.Year.ToString());
                        guideInformation.EndSuperGuide = DateTime.Now.AddYears(1);
                        informationRepository.save(guideInformation);
                    }
                }
            }
            // nije potrebno staviti da je regular, jer je to defaultno
        }

        public double getAverageGrade(int guideId)
        {
            List<GuideRate> rates = guideRateService.GetGuideRates(guideId);
            List<int> ints = new List<int>();
            foreach (GuideRate rate in rates) {
                ints.Add(rate.Knowledge);
                ints.Add(rate.TourInterest);
                ints.Add(rate.Language);
            }
            if(ints.Count == 0)
            {
                return 0;
            }
            return ints.Average();
        }

        public void Quit(int id)
        {
            GuideInformation guide = GetByGuideId(id);
            // pronadji sve ture za guide-a
            List<Tour> tours = tourService.getPendingToursByGuide(id);
            // cancel-uj ih -> status cancel i podeli vaucere
            tourService.CancelMultipleForQuitting(tours, id);
            // HasQuit = true
            guide.HasQuit = true;
            // update repository
            informationRepository.save(guide);
        }

        public bool CanLogIn(int user_id)
        {
            GuideInformation guide = GetByGuideId(user_id);
            return !guide.HasQuit;
        }
        
    }
}
