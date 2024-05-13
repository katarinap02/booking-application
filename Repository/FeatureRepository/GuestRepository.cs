using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.FeatureRepository
{
    public class GuestRepository : IGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<Guest> _serializer;
        private List<Guest> _guests;

        public Subject GuestSubject { get; set; }

        public GuestRepository()
        {
            _serializer = new Serializer<Guest>();
            _guests = _serializer.FromCSV(FilePath);
            GuestSubject = new Subject();
        }
        public List<Guest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guest GetById(int guestId)
        {
            foreach (Guest guest in _guests)
            {
                if (guestId == guest.Id) return guest;
            }

            return null;
        }

        public Guest Update(Guest guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest current = _guests.Find(a => a.Id == guest.Id);
            int index = _guests.IndexOf(current);
            _guests.Remove(current);
            _guests.Insert(index, guest);
            _serializer.ToCSV(FilePath, _guests);
            GuestSubject.NotifyObservers();
            return guest;
        }

     
    }
}
