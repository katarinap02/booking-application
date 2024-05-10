using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;

namespace BookingApp.Application.Services.FeatureServices
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository AccommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            this.AccommodationRepository = accommodationRepository;
        }

        public List<Accommodation> GetAll()
        {
            return AccommodationRepository.GetAll();
        }

        public Accommodation Add(Accommodation accommodation)
        {
            return AccommodationRepository.Add(accommodation);
        }

        public void Delete(Accommodation accommodation)
        {

            AccommodationRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return AccommodationRepository.Update(accommodation);
        }

        public Accommodation GetById(int id)
        {
            return AccommodationRepository.GetById(id);
        }

     /*  public void Subscribe(IObserver observer)
        {
            AccommodationRepository.AccommodationSubject.Subscribe(observer);
        }*/

        public void FreeDateRange(Accommodation accommodation, AccommodationReservationViewModel selectedReservation)
        {
            foreach (CalendarDateRange dateRange in accommodation.UnavailableDates)
            {
                if (dateRange.Start == selectedReservation.StartDate && dateRange.End == selectedReservation.EndDate)
                {
                    accommodation.UnavailableDates.Remove(dateRange);
                    break;
                }

            }

            AccommodationRepository.Update(accommodation);
        }

        
    }
}
