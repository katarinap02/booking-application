using BookingApp.WPF.ViewModel;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Repository
{
    public class DelayRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/delay_requests.csv";
        private readonly Serializer<DelayRequest> _serializer;
        private List<DelayRequest> _requests;

        public Subject RequestSubject { get; set; }

        public DelayRequestRepository()
        {
            _serializer = new Serializer<DelayRequest>();
            _requests = new List<DelayRequest>();
            RequestSubject = new Subject();
        }

        public List<DelayRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public DelayRequest Add(DelayRequest request)
        {
            request.Id = NextId();
            _requests = _serializer.FromCSV(FilePath);
            _requests.Add(request);
            _serializer.ToCSV(FilePath, _requests);
            RequestSubject.NotifyObservers();
            return request;
        }

        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
                return 1;

            return _requests.Max(a => a.Id) + 1;
        }

        public void Delete(DelayRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            DelayRequest found = _requests.Find(r => r.Id == request.Id);
            _requests.Remove(found);
            _serializer.ToCSV(FilePath, _requests);
            RequestSubject.NotifyObservers();

        }

        public DelayRequest Update(DelayRequest request)
        {
            _requests = _serializer.FromCSV(FilePath);
            DelayRequest current = _requests.Find(r => r.Id == request.Id);
            int index = _requests.IndexOf(current);
            _requests.Remove(current);
            _requests.Insert(index, request);
            _serializer.ToCSV(FilePath, _requests);
            RequestSubject.NotifyObservers();
            return request;
        }

    }
}
