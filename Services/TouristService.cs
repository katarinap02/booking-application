using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TouristService
    {
        private readonly TourRepository tourRepository;
        private readonly TourReservationRepository tourReservationRepository;
        private readonly VoucherRepository voucherRepository;

        public TouristService()
        {
            tourRepository = new TourRepository();
            tourReservationRepository = new TourReservationRepository();
            voucherRepository = new VoucherRepository();
        }

        public List<TourViewModel> GetAll()
        {
            return ToTourViewModel(tourRepository.GetAll());
        }

        public int FindMaxNumberOfParticipants()
        {
            return tourRepository.FindMaxNumberOfParticipants();
        }

        public List<TourViewModel> ToTourViewModel(List<Tour> Tours)
        {
            // creating list from Tour to TourViewModel
            List<TourViewModel> ToursViewModel = new List<TourViewModel>();
            foreach (Tour tour in Tours)
            {
                ToursViewModel.Add(new TourViewModel(tour));
            }
            return ToursViewModel;
        }

        public List<VoucherViewModel> ToVoucherViewModel(List<Voucher> Vouchers)
        {
            List<VoucherViewModel> VouchersViewModel = new List<VoucherViewModel>();
            foreach(Voucher voucher in Vouchers)
            {
                VouchersViewModel.Add(new VoucherViewModel(voucher));
            }
            return VouchersViewModel;
        }
        public List<TourViewModel>? SearchTours(Tour searchCriteria)
        {
            return ToTourViewModel(tourRepository.SearchTours(searchCriteria));
        }

        public int ToursCount()
        {
            return tourRepository.ToursCount();
        }

        public Tour UpdateAvailablePlaces(TourViewModel tour, int reducer)
        {
            return tourRepository.UpdateAvailablePlaces(tour.ToTour(), reducer);
        }

        public List<TourViewModel> FindMyTours(int id)
        {
            return ToTourViewModel(tourReservationRepository.FindMyTours(id));
        }

        public List<TourViewModel> FindMyEndedTours(int id)
        {
            return ToTourViewModel(tourReservationRepository.FindMyEndedTours(id));
        }

        public List<VoucherViewModel> FindVouchersByUser(int id)
        {
            return ToVoucherViewModel(voucherRepository.FindVouchersByUser(id));
        }
    }
}
