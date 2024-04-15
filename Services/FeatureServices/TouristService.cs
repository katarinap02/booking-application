using BookingApp.Model.Features;
using BookingApp.Repository.FeatureRepository;
using BookingApp.View.ViewModel;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.FeatureServices
{
    public class TouristService
    {
        private readonly TouristRepository _touristRepository;

        public TouristService()
        {
            _touristRepository = new TouristRepository();
        }

        public TourParticipantViewModel ToTourParticipantViewModel(Tourist tourist)
        {
            TourParticipantViewModel viewModel = new TourParticipantViewModel();
            viewModel.Name = tourist.Name;
            viewModel.LastName = tourist.LastName;
            viewModel.Years = tourist.Age;
            return viewModel;
        }

        public TourParticipantViewModel FindTouristById(int touristId)
        {
            return ToTourParticipantViewModel(_touristRepository.FindTouristById(touristId));
        }
        public Tourist GetTouristById(int touristId)
        {
            return _touristRepository.FindTouristById(touristId);
        }
    }
}
