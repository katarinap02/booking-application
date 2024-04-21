using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.View.Guest.GuestTools
{
    public class AccommodationSearcher
    {
        public AccommodationService AccommodationService { get; set; }

        public AccommodationSearcher(AccommodationService accommodationService)
        {
            AccommodationService = accommodationService;
        }

        public List<AccommodationViewModel> SearchAccommodations(List<string> queries)
        {


            ObservableCollection<AccommodationViewModel> totalAccommodations = new ObservableCollection<AccommodationViewModel>();
            foreach (Accommodation accommodation in AccommodationService.GetAll())
                totalAccommodations.Add(new AccommodationViewModel(accommodation));

            List<AccommodationViewModel> searchResults = FilterAccommodations(totalAccommodations, queries);


            int totalItems = searchResults.Count;
            List<AccommodationViewModel> results = new List<AccommodationViewModel>();
            foreach (AccommodationViewModel accommodation in searchResults)
                results.Add(accommodation);

            return results;




        }

        private List<AccommodationViewModel> FilterAccommodations(ObservableCollection<AccommodationViewModel> totalAccommodations, List<string> queries)
        {
            List<AccommodationViewModel> filteredAccommodations = totalAccommodations.Where(accommodation => (string.IsNullOrEmpty(queries[0]) || accommodation.Name.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || accommodation.City.ToUpper().Contains(queries[1].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[2]) || accommodation.Country.ToUpper().Contains(queries[2].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[3]) || accommodation.Type.ToString().ToUpper().Contains(queries[3].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[4]) || Convert.ToInt32(queries[4]) <= accommodation.MaxGuestNumber) &&
                                                                           (string.IsNullOrEmpty(queries[5]) || Convert.ToInt32(queries[5]) >= accommodation.MinReservationDays)
                                                                           ).ToList();

            return filteredAccommodations;
        }

    }
}
