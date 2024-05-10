using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
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

        public TourRequestService(ITourRequestRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }

        public void AcceptRequest(TourRequest request, int GuideId, DateTime acceptedDate)
        {
            TourRequest tourRequest = _tourRequestRepository.GetById(request.Id);
            tourRequest.Status = TourRequestStatus.Accepted;
            tourRequest.AcceptedDate = acceptedDate;
            tourRequest.GuideId = GuideId;
            _tourRequestRepository.UpdateRequest(tourRequest);
        }
        public List<TourRequest> filterRequests(TourRequest searchCriteria)
        {
            List<TourRequest> tourRequests = _tourRequestRepository.GetAllPending();
            List<TourRequest> filteredRequests = tourRequests;

            if (!string.IsNullOrEmpty(searchCriteria.City))
            {
                MessageBox.Show("usao", "City");
                filteredRequests = tourRequests.FindAll(x => x.City.ToLower().Contains(searchCriteria.City.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Country))
            {
                MessageBox.Show("usao", "Country");
                filteredRequests = filteredRequests.FindAll(x => x.Country.ToLower().Contains(searchCriteria.Country.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(searchCriteria.Language))
            {
                MessageBox.Show("usao", "Language");
                MessageBox.Show(searchCriteria.Language.ToLower());
                MessageBox.Show(filteredRequests.Count().ToString());
                filteredRequests = filteredRequests.FindAll(x => x.Language.ToLower().Contains(searchCriteria.Language.ToLower())).ToList();
                MessageBox.Show(filteredRequests.Count().ToString());
            }
            /*
            if (searchCriteria.StartDate != null && searchCriteria.EndDate != null)
            {
                MessageBox.Show("usao", "datumi");
                filteredRequests = tourRequests.FindAll(x => x.StartDate >= searchCriteria.StartDate && x.EndDate <= searchCriteria.EndDate).ToList();
            }*/
            MessageBox.Show(filteredRequests.Count().ToString());
            MessageBox.Show(filteredRequests[0].Language);
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
        public List<int> GetYearlyStatistic()
        {
            List<TourRequest> requests = _tourRequestRepository.GetAll();
            // kao pretpostavku smo uzeli da ova aplikacija postoji od 2023 godine
            List<int> yearly_requests = new List<int>();
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

        public List<int> GetMonthlyStatistics(int year)
        {
            List<TourRequest> requests = _tourRequestRepository.GetAllForYear(year);
            List<int> monthly_requests = new List<int>();
            foreach(var request in requests)
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

        public string GetLanguageSuggestion()
        {
            List<TourRequest> requestsLastYear = _tourRequestRepository.GetAllForYear(DateTime.Now.Year-1);

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

        public List<TourRequest> GetRequestsForLastYear()
        {
            DateTime startDate = DateTime.Now.Date.AddYears(-1); 
            DateTime endDate = DateTime.Now.Date; 
            return _tourRequestRepository.GetRequestsBetweenDates(startDate, endDate);
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

        public void CreateTourByStatistics(Tour tour, string parameter) // parameter ce biti ili lokacija ili jezik (tura se pravi ili spram jezika ili spram lokacije)
                                                                        // slobodno dodati jos prosledjenih vrednosti
        {
            _tourService.Add(tour);
            SendNotifications();
        }

        public void SendNotifications()
        {
            // PROSIRITI
        }
    }
}
