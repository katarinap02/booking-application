using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ReservationCancellationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservation_cancellations.csv";

        private readonly Serializer<ReservationCancellation> _serializer;

        private List<ReservationCancellation> _cancellations;

        public Subject CancellationSubject { get; set; }
        public ReservationCancellationRepository()
        {
            _serializer = new Serializer<ReservationCancellation>();
            _cancellations = new List<ReservationCancellation>();
            CancellationSubject = new Subject();
        }

        public List<ReservationCancellation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ReservationCancellation Add(ReservationCancellation cancellation)
        {

            _cancellations = _serializer.FromCSV(FilePath);
            _cancellations.Add(cancellation);
            _serializer.ToCSV(FilePath, _cancellations);
            CancellationSubject.NotifyObservers();
            return cancellation;
        }
    }
}
