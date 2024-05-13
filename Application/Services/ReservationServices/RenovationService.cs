using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository;
using BookingApp.Repository.ReservationRepository;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Services.ReservationServices
{
    public class RenovationService
    {
        private readonly IRenovationRepository RenovationRepository;

        public RenovationService(IRenovationRepository renovationRepository)
        {
            RenovationRepository = renovationRepository;
        }

        public List<Renovation> GetAll()
        {
            return RenovationRepository.GetAll();
        }

        public Renovation Add(Renovation renovation)
        {
            return RenovationRepository.Add(renovation);
        }


        public void Delete(Renovation selectedRenovation)
        {
            RenovationRepository.Delete(selectedRenovation);
        }

        public void CancelRenovation(Renovation selectedRenovation)
        {
            DateTime todayPlus5Days = DateTime.Today.AddDays(5);
            if(selectedRenovation.StartDate >  todayPlus5Days)
            {
                Delete(selectedRenovation);
            }
        }
    }
}
