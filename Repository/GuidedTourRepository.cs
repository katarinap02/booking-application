using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuidedTourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";
        private readonly Serializer<GuidedTour> _serializer;
        private List<GuidedTour> _guidedTour;

        public GuidedTourRepository()
        {
            _serializer = new Serializer<GuidedTour>();
            _guidedTour = _serializer.FromCSV(FilePath);
        }

        public List<GuidedTour> GetAll() {
            return _serializer.FromCSV(FilePath);
        }

    }
}
