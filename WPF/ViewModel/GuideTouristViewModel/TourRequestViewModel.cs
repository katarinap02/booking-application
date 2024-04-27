using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.RepositoryInterfaces.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.GuideTouristViewModel
{
    public class TourRequestViewModel
    {
        public TourRequestService _tourRequestService {  get; set; }
        public ObservableCollection<TourRequestViewModel> TourRequests { get; set; }

        public TourRequestViewModel()
        {
            _tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
            TourRequests = new ObservableCollection<TourRequestViewModel>();
        }
    }
}
