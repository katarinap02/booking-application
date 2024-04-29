using BookingApp.Domain.Model.Reservations;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.Serializer;
using BookingApp.WPF.ViewModel.HostGuestViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.ReservationRepository
{
    public class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovation.csv";
        private readonly Serializer<Renovation> _serializer;
        private List<Renovation> _renovations;

        public Subject RenovationSubject { get; set; }

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = new List<Renovation>();  
            RenovationSubject = new Subject();
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation Add(Renovation reservation)
        {
            reservation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(reservation);
            _serializer.ToCSV(FilePath, _renovations);
            RenovationSubject.NotifyObservers();
            return reservation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
                return 1;

            return _renovations.Max(a => a.Id) + 1;
        }

        public void Delete(Renovation selectedReservation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation found = _renovations.Find(r => r.Id == selectedReservation.Id);
            if (found != null)
            _renovations.Remove(found);
            _serializer.ToCSV(FilePath, _renovations);
            RenovationSubject.NotifyObservers();
        }
    }
}
