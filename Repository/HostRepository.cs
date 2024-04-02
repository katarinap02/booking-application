using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookingApp.Repository
{
     public class HostRepository
    {
        private const string FilePath = "../../../Resources/Data/hosts.csv";

        private readonly Serializer<Host> _serializer;
        private readonly Serializer<AccommodationRate> _serializerRate;

        private List<Host> _hosts;

        private List<AccommodationRate> _rates;
        private const string FilePathRate = "../../../Resources/Data/accommodation_rate.csv";
        

        public Subject HostSubject { get; set; }

        public HostRepository()
        {
            _serializer = new Serializer<Host>();
            _serializerRate = new Serializer<AccommodationRate>();
            _hosts = _serializer.FromCSV(FilePath);
            HostSubject = new Subject();
            _rates = _serializerRate.FromCSV(FilePathRate);

        }

        public List<Host> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Host GetByUsername(string username)
        {
           
            return _hosts.FirstOrDefault(u => u.Username == username);
        }

        public Host Update(Host host)
        {
            _hosts = _serializer.FromCSV(FilePath);
            Host current = _hosts.Find(a => a.Id == host.Id);
            int index = _hosts.IndexOf(current);
            _hosts.Remove(current);
            _hosts.Insert(index, host);
            _serializer.ToCSV(FilePath, _hosts);
            HostSubject.NotifyObservers();
            return host; //vraca proslo
        }

        public void BecomeSuperHost(Host host)
        {
            int counter = 0;
            double gradeSum = 0;
            foreach(AccommodationRate rate in _rates)
            {
                if(rate.HostId == host.Id)
                {
                    counter++;
                    gradeSum += (rate.Correctness + rate.Cleanliness) / 2;
                }
            }

            if(counter < 10)
            {
                host.IsSuperHost = false;
                Update(host);
                return;
            }

            double average = gradeSum / Convert.ToDouble(counter);
            if(average >= 4.5)
            {
                host.IsSuperHost = true; ;
                
            }
            else
            {
                host.IsSuperHost = false;
            }

            Update(host);
            return;
        }

        public Host GetById(int hostId)
        {
            foreach (Host host in _hosts)
            {
                if (hostId == host.Id) return host;
            }

            return null;
        }

    }
}
