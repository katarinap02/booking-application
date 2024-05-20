using BookingApp.Domain.Model.Features;
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
        private const string FilePath = "../../../Resources/Data/guided_tour.csv";
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

        public void Add(int guide_id, int tour_id)
        {
            GuidedTour guidedTour = new GuidedTour(guide_id, tour_id);
            _guidedTours.Add(guidedTour);
            _serializer.ToCSV(FilePath, _guidedTours);
        }

        public bool HasTourCurrently(int guide_id)
        {
            return _guidedTours.Find(t => t.GuideId == guide_id) != null;
        }
        
        public int FindTourIdByGuide(int guide_id)
        {
            GuidedTour guidedTour = _guidedTours.Find(t => t.GuideId == guide_id);
            return guidedTour.TourId;
        }

        public bool Exists(int guide_id, int tour_id) {
            return _guidedTours.Find(t => t.GuideId == guide_id && t.TourId == tour_id) != null;
        }

        public void Remove(int guide_id, int tour_id)
        {
            GuidedTour guidedTour = _guidedTours.Find(t => t.GuideId == guide_id && t.TourId == tour_id);
            _guidedTours.Remove(guidedTour);
            _serializer.ToCSV(FilePath, _guidedTours);
        }

        public void Remove(int tour_id)
        {
            GuidedTour guidedTour = _guidedTours.Find(t => t.TourId == tour_id);
            _guidedTours.Remove(guidedTour);
            _serializer.ToCSV(FilePath, _guidedTours);
        }

    }
}
