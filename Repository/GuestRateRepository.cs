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
    public class GuestRateRepository { 
     private const string FilePath = "../../../Resources/Data/guest_rate.csv";
    private readonly Serializer<GuestRate> _serializer;
    private List<GuestRate> _rates;

    public Subject RateSubject { get; set; }

    public GuestRateRepository()
    {
        _serializer = new Serializer<GuestRate>();
            _rates = new List<GuestRate>();
            RateSubject = new Subject();
    }

    public List<GuestRate> GetAll()
    {
        return _serializer.FromCSV(FilePath);
    }

    public GuestRate Add(GuestRate rate)
    {
            _rates = _serializer.FromCSV(FilePath);
            _rates.Add(rate);
        _serializer.ToCSV(FilePath, _rates);
            RateSubject.NotifyObservers();
        return rate;
    }


}
}
