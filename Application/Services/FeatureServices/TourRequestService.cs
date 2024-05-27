using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.HostPages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.Services.FeatureServices
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository;
        private static readonly TourService _tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
        private static readonly TouristNotificationService _touristNotificationService = new TouristNotificationService(Injector.Injector.CreateInstance<ITouristNotificationRepository>());
        private static readonly ComplexTourRequestService _complexTourRequestService = new ComplexTourRequestService(Injector.Injector.CreateInstance<IComplexTourRequestRepository>());

        public TourRequestService(ITourRequestRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }

        public void SaveComplexRequest(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequestService.Add(complexTourRequest);
        }

        public void AcceptRequest(TourRequest request, int GuideId, DateTime acceptedDate)
        {
            TourRequest tourRequest = _tourRequestRepository.GetById(request.Id);
            tourRequest.Status = TourRequestStatus.Accepted;
            tourRequest.AcceptedDate = acceptedDate;
            tourRequest.GuideId = GuideId;
            _tourRequestRepository.UpdateRequest(tourRequest);
        }
        public List<TourRequest> filterRequests(TourRequest searchCriteria, int ParticipantNumber) 
        {
            List<TourRequest> tourRequests = _tourRequestRepository.GetAllPending();
            List<TourRequest> filteredRequests = tourRequests;

            if (!string.IsNullOrEmpty(searchCriteria.City))
            {
                filteredRequests = tourRequests.FindAll(x => x.City.ToLower().Contains(searchCriteria.City.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Country))
            {
                filteredRequests = filteredRequests.FindAll(x => x.Country.ToLower().Contains(searchCriteria.Country.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Language))
            {                
                filteredRequests = filteredRequests.FindAll(x => x.Language.ToLower().Contains(searchCriteria.Language.ToLower())).ToList();                
            }
            if (ParticipantNumber != 0)
            {
                filteredRequests = filteredRequests.FindAll(x => x.ParticipantIds.Count() == ParticipantNumber).ToList();
            }
            if (searchCriteria.StartDate != DateTime.MinValue && searchCriteria.EndDate != DateTime.MaxValue)
            {                
                filteredRequests = tourRequests.FindAll(x => x.StartDate >= searchCriteria.StartDate && x.EndDate <= searchCriteria.EndDate).ToList();
            }
            return filteredRequests;
        }

        public void SaveRequest(TourRequest tourRequest)
        {
            _tourRequestRepository.Add(tourRequest);
        }

        public int NextRequestId()
        {
            return _tourRequestRepository.NextId();
        }
    
        public List<TourRequest> GetRequestsByTouristId(int touristId)
        {
            return _tourRequestRepository.GetByTouristId(touristId);
        }

        public List<ComplexTourRequest> GetComplexRequestsByTouristId(int touristId)
        {
            return _complexTourRequestService.GetByTouristId(touristId);
        }
        
        public List<TourRequest> getRequestsForLocation(string city, string country)
        {
            List<TourRequest> requests = _tourRequestRepository.GetAll();
            return requests.FindAll(x => x.City == city && x.Country == country);
        }

        public List<TourRequest> getRequestsForLanguage(string language)
        {
            List<TourRequest> requests = _tourRequestRepository.GetAll();
            return requests.FindAll(x => x.Language == language);
        }        

        public List<int> GetYearlyStatistic(List<TourRequest> requests)
        {
            // kao pretpostavku smo uzeli da ova aplikacija postoji od 2023 godine
            List<int> yearly_requests = new List<int>() { 0, 0 };
            foreach (var request in requests)
            {
                if (request.DateRequested.Year == 2023)
                {
                    yearly_requests[0] = yearly_requests[0] + 1;
                }
                else if (request.DateRequested.Year == 2024)
                {
                    yearly_requests[1] = yearly_requests[1] + 1;
                }
            }

            return yearly_requests;
        }

        public List<int> GetMonthlyStatistics(List<TourRequest> requests1, int year)
        {
            List<TourRequest> requests = requests1.FindAll(x => x.DateRequested.Year == year);
            List<int> monthly_requests = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            foreach (var request in requests)
            {
                for(int j  = 0; j < 12; j++) 
                {
                    if(request.DateRequested.Month == j+1) // za svaki mesec puni listu
                    {
                        monthly_requests[j]++;
                    }
                }
            }

            return monthly_requests;
        }

        public List<TourRequest> GetRequestsForLastYear()
        {
            DateTime startDate = DateTime.Now.Date.AddYears(-1);
            DateTime endDate = DateTime.Now.Date;
            return _tourRequestRepository.GetRequestsBetweenDates(startDate, endDate);
        }

        public string GetLanguageSuggestion()
        {
            List<TourRequest> requestsLastYear = GetRequestsForLastYear();

            Dictionary<string, int> languageCounts = CountLanguageRequests(requestsLastYear);

            return GetMostRequestedLanguage(languageCounts);
        }

        private Dictionary<string, int> CountLanguageRequests(List<TourRequest> requests)
        {
            Dictionary<string, int> languageCounts = new Dictionary<string, int>();

            foreach (var request in requests)
            {
                string language = request.Language;
                if (languageCounts.ContainsKey(language))
                {
                    languageCounts[language]++;
                }
                else
                {
                    languageCounts[language] = 1;
                }
            }

            return languageCounts;
        }

        private string GetMostRequestedLanguage(Dictionary<string, int> languageCounts)
        {
            string mostRequestedLanguage = null;
            int maxCount = 0;

            foreach (var kvp in languageCounts)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostRequestedLanguage = kvp.Key;
                }
            }

            return mostRequestedLanguage;
        }        

        public string GetLocationSuggestion()
        {
            List<TourRequest> requestsLastYear = GetRequestsForLastYear();

            Dictionary<string, int> locationCounts = CountLocationRequests(requestsLastYear);

            return GetMostRequestedLocation(locationCounts);
        }

        private Dictionary<string, int> CountLocationRequests(List<TourRequest> requests)
        {
            Dictionary<string, int> locationCounts = new Dictionary<string, int>();

            foreach (var request in requests)
            {
                string location = $"{request.City}, {request.Country}";
                if (locationCounts.ContainsKey(location))
                {
                    locationCounts[location]++;
                }
                else
                {
                    locationCounts[location] = 1;
                }
            }

            return locationCounts;
        }

        private string GetMostRequestedLocation(Dictionary<string, int> locationCounts)
        {
            string mostRequestedLocation = null;
            int maxCount = 0;

            foreach (var kvp in locationCounts)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostRequestedLocation = kvp.Key;
                }
            }
            return mostRequestedLocation;
        }
        public double GetAcceptedRequestPercentage(int touristId, int? year = 0)
        {
            var requests = _tourRequestRepository.GetByTouristId(touristId);
            if (year != 0)
            {
                foreach(var req in requests)
                {
                    var p = req.DateRequested.Year;
                }
                requests = requests.Where(r => r.DateRequested.Year == year.Value).ToList();
            }

            var totalRequests = requests.Count;
            var acceptedRequests = requests.Count(r => r.Status == TourRequestStatus.Accepted);
            if(acceptedRequests == 0)
            {
                return 0;
            }
            return (double)acceptedRequests / totalRequests * 100;
        }

        public double GetRejectedRequestPercentage(int touristId, int? year = 0)
        {
            var requests = _tourRequestRepository.GetByTouristId(touristId);
            if (year != 0)
            {
                requests = requests.Where(r => r.DateRequested.Year == year.Value).ToList();
            }

            var totalRequests = requests.Count;
            var rejectedRequests = requests.Count(r => r.Status == TourRequestStatus.Invalid);
            if(rejectedRequests == 0)
            {
                return 0;
            }
            return (double)rejectedRequests / totalRequests * 100;
        }
        public double GetAverageNumberOfPeopleInAcceptedRequests(int touristId, int? year = 0)
        {
            var requests = _tourRequestRepository.GetByTouristId(touristId)
                .Where(r => r.Status == TourRequestStatus.Accepted);
            if (year != 0)
            {
                requests = requests.Where(r => r.DateRequested.Year == year.Value).ToList();
            }
            if(requests.Count() == 0)
            {
                return 0;
            }
            return requests.Average(r => r.ParticipantIds.Count);
        }
        public Dictionary<string, int> GetRequestCountByLanguage(int touristId)
        {
            var requests = _tourRequestRepository.GetByTouristId(touristId);
            return requests.GroupBy(r => r.Language)
                .ToDictionary(g => g.Key, g => g.Count());
        }
        public Dictionary<string, int> GetRequestCountByCity(int touristId)
        {
            var requests = _tourRequestRepository.GetByTouristId(touristId);
            return requests.GroupBy(r => r.City)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public void CreateTourByStatistics(Tour tour) 
        {
            _tourService.Add(tour);
            SendNotifications(tour);
        }

        public List<string> getAllLocations()
        {
            List<TourRequest> requests = _tourRequestRepository.GetAll();
            List<string> locations = new List<string>();
            foreach (TourRequest request in requests)
            {
                string location = request.City+", "+request.Country;
                if (!locations.Contains(location))
                {
                    locations.Add(location);
                }
            }
            return locations;
        }

        public List<string> getAllLanguages()
        {
            List<TourRequest> requests = _tourRequestRepository.GetAll();
            List<string> languages = new List<string>();
            foreach (TourRequest request in requests)
            {                
                if (!languages.Contains(request.Language))
                {
                    languages.Add(request.Language);
                }
            }
            return languages;
        }

        public void SendNotifications(Tour tour)
        {
            var unfulfilledRequests = _tourRequestRepository.GetAllPendingOrInvalid();
            List<int> notifiedTouristIds = new List<int>();
            foreach (var request in unfulfilledRequests)
            {
                if(notifiedTouristIds.Contains(request.TouristId))
                {
                    continue;
                }

                if(tour.Language == request.Language || tour.City == request.City)
                {
                    notifiedTouristIds.Add(request.TouristId);
                    var Notification = new TouristNotification(request.TouristId, tour.Id, NotificationType.RequestAccepted, tour.Name,
                        "", 0);
                    _touristNotificationService.Add(Notification);
                }
            }
        }
        public void UpdateTourRequests()
        {
            List<TourRequest> tourRequests = _tourRequestRepository.GetAll();
            foreach(TourRequest request in tourRequests)
            {
                if(request.Status != TourRequestStatus.Accepted && DateTime.Now >= request.StartDate.AddHours(-48))
                {
                    request.Status = TourRequestStatus.Invalid;
                    _tourRequestRepository.UpdateRequest(request);
                }
            }
        }

        public List<TourRequest> GetTourRequestsByComplexId(int complexId)
        {
            return _complexTourRequestService.GetTourRequestsByComplexId(complexId);
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }
    }
}
