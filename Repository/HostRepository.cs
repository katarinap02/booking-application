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
     public class HostRepository
    {
        private const string FilePath = "../../../Resources/Data/hosts.csv";

        private readonly Serializer<Host> _serializer;

        private List<Host> _hosts;

        public HostRepository()
        {
            _serializer = new Serializer<Host>();
            _hosts = _serializer.FromCSV(FilePath);
            
        }

        public List<Host> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Host GetByUsername(string username)
        {
           
            return _hosts.FirstOrDefault(u => u.Username == username);
        }

        public bool BecomeSuperHost(Host host)
        {
            return false;
        }
    }
}
