using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class TakePartInViewModel : ViewModelBase
    {
        public ObservableCollection<TourRequestDTOViewModel> tourRequestViewModels { get; set; }
        private static readonly ComplexTourRequestService requestService = new ComplexTourRequestService(Injector.Injector.CreateInstance<IComplexTourRequestRepository>());
        private int GuideId;

        public TourRequestDTOViewModel Selected {  get; set; }
        public MyICommand Accept { get; set; }
        public TakePartInViewModel(int guideId)
        {
            Selected = new TourRequestDTOViewModel();
            Accept = new MyICommand(Accepting);
            List<TourRequest> tourRequests = requestService.GetAllPending(guideId);
            tourRequestViewModels = new ObservableCollection<TourRequestDTOViewModel>();
            GuideId = guideId;
            GetGridData(tourRequests);
        }

        public void GetGridData(List<TourRequest> tourRequests)
        {
            foreach(var tourRequest in tourRequests)
            {
                tourRequestViewModels.Add(new TourRequestDTOViewModel(tourRequest));
            }
        }

        private void Accepting()
        {   if(Selected == null)
            {
                MessageBox.Show("error");
                return;
            }
            CalendarGuideWindow calendarGuideWindow = new CalendarGuideWindow(GuideId, Selected);
            calendarGuideWindow.Show();
        }

    }
}
