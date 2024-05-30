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
using System.Windows;
using Wpf.Ui.Controls;

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


        public List<Accommodation> FindAvailableAccommodations(int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
           List<Accommodation> result = new List<Accommodation>();
           foreach(Accommodation accommodation in this.GetAll())
            {
                if(endDate == DateTime.MaxValue)
                {
                    if (CheckParameters(accommodation, dayNumber, guestNumber, startDate, endDate))
                        result.Add(accommodation);
                }
                else
                {
                    result.Add(accommodation);
                }
               
            }
            return result;
        }

        private bool CheckParameters(Accommodation accommodation, int dayNumber, int guestNumber, DateTime startDate, DateTime endDate)
        {
            if (CheckDayGuestNumber(accommodation, dayNumber, guestNumber))
            {
                List<DateTime> chosenDates = RemoveDates(accommodation, startDate, endDate);
                if (CheckAvailability(chosenDates, dayNumber))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool CheckAvailability(List<DateTime> chosenDates, int dayNumber)
        {
            int dayCounter = 1;
            for(int i = 0; i < chosenDates.Count-1; i++)
            {
               
                if ((chosenDates[i + 1] - chosenDates[i]).Days == 1)
                    dayCounter++;

                if(dayCounter >= dayNumber)
                    return true;
            }

            return false;
        }

        public List<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate.AddDays(-1); date = date.AddDays(1))
                allDates.Add(date);

            allDates.Add(endDate);
            return allDates;
        }

        private List<DateTime> RemoveDates(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            
            List<DateTime> chosenDates = GetDatesBetween(startDate, endDate);
          
            List<DateTime> unavailableDates = new List<DateTime>();
            foreach(CalendarDateRange unavailableRange in accommodation.UnavailableDates)
            {
                unavailableDates = GetDatesBetween(unavailableRange.Start, unavailableRange.End);
                foreach (DateTime unavailableDate in unavailableDates)
                {
                    if (unavailableDate >= startDate && unavailableDate <= endDate)
                        chosenDates.Remove(unavailableDate);
                }
            }

           

            return chosenDates;
        }

    
      

       

        private bool CheckDayGuestNumber(Accommodation accommodation, int dayNumber, int guestNumber)
        {
            if (accommodation.MinReservationDays <= dayNumber && accommodation.MaxGuestNumber >= guestNumber)
                return true;
            else
                return false;
        }

        public void ChangeListOrder(AccommodationViewModel accommodation)
        {
            
            foreach(Accommodation acc in  AccommodationRepository.GetAll())
            {
                if(acc.Id == accommodation.Id)
                {
                    string pom = acc.Pictures[0];
                    acc.Pictures.RemoveAt(0);
                    acc.Pictures.Add(pom);
                    Update(acc);
                    accommodation.OnePicture = acc.Pictures[0];

                }
            }
        }

        public void CloseAccommodation(int Id)
        {
            foreach (Accommodation acc in AccommodationRepository.GetAll())
            {
                if (acc.Id == Id)
                {
                    acc.ClosedAccommodation = true;
                    Update(acc);

                }
            }
        }

        public List<string> GetHostLocations(User user)
        {
            HashSet<string> uniqueCities = new HashSet<string>();
            List<string> list = new List<string>();

            foreach (Accommodation acc in GetAll())
            {
                if (!uniqueCities.Contains(acc.City) && acc.HostId == user.Id)
                {
                    uniqueCities.Add(acc.City);
                    list.Add(acc.City);
                }
            }

            return list;
        }


    }
}
