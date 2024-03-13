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
        private List<GuidedTour> _guidedTours;

        public GuidedTourRepository()
        {
            _serializer = new Serializer<GuidedTour>();
            _guidedTours = _serializer.FromCSV(FilePath);
        }

        public List<GuidedTour> GetAll() {
            return _serializer.FromCSV(FilePath);
        }

        public void Add(User guide, Tour tour) {
            GuidedTour guidedTour = new GuidedTour(guide, tour);
            _guidedTours.Add(guidedTour);
            _serializer.ToCSV(FilePath, _guidedTours);
        }

    }
}
