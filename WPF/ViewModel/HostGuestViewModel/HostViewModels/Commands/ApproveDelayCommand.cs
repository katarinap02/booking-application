using BookingApp.Model;
using BookingApp.Services;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands
{
    public class ApproveDelayCommand : CommandBase
    {
        public DelayRequestService _DelayRequestService { get; set; }

        public UserService _UserService { get; set; }
        public AccommodationReservationService _AccommodationReservationService { get; set; }
        public AccommodationService _AccommodationService { get; set; }

        public DelayRequestViewModel _SelectedDelay { get; set; }

        public DelayRequestViewModel _Delay { get; set; }





        public ApproveDelayCommand(DelayRequestViewModel SelectedDelay,
            DelayRequestViewModel Delay, AccommodationReservationService AccommodationReservationService,
            AccommodationService AccommodationService, UserService UserService, DelayRequestService DelayRequestService)
        {
            _SelectedDelay = SelectedDelay;
            _Delay = Delay;
            _AccommodationReservationService = AccommodationReservationService;
            _AccommodationService = AccommodationService;
            _UserService = UserService;
            _DelayRequestService = DelayRequestService;


        }

        public override void Execute(object? parameter)
        {
            if (_SelectedDelay != null)
            {
                _Delay = _SelectedDelay;
                _Delay.Status = RequestStatus.APPROVED;
                _AccommodationReservationService.UpdateReservation(_Delay.ReservationId, _Delay.StartDate, _Delay.EndDate);
                _DelayRequestService.Update(_Delay.ToDelayRequest());
                Accommodation accommodation = _AccommodationService.GetById(_AccommodationReservationService.GetById(_Delay.ReservationId).AccommodationId);
                ReplaceDates(accommodation, _Delay);
                _AccommodationService.Update(accommodation);
                // _DelayPageViewModel.Update();
            }
        }

        private void ReplaceDates(Accommodation accommodation, DelayRequestViewModel delay)
        {
            foreach (CalendarDateRange dateRange in accommodation.UnavailableDates)
                if (dateRange.Start == delay.StartLastDate && dateRange.End == delay.EndLastDate)
                {
                    dateRange.Start = delay.StartDate;
                    dateRange.End = delay.EndDate;
                }
        }
    }
}
