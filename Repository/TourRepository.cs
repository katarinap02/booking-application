using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    public class TourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        //add za sada

        public void Add(Tour tour) 
        {
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
        }

<<<<<<< Updated upstream
=======
        public int NextPersonalId() {
            List<int> personalIds = new List<int>();
            foreach (Tour tour in _tours)
            {
                personalIds.Add(tour.Id);
            }

            int max = 0;
            if(personalIds.Count == 0) {
                max = -1;
            }

            return max + 1;
        }

>>>>>>> Stashed changes
        public int NextId() //generise ID za novu grupu tura
        {
            List<int> groupIds = new List<int>();
            foreach(Tour tour in _tours)
            {
                groupIds.Add(tour.GroupId);
            }
            int max = 0;
            if (groupIds.Count == 0)
            {
                max = -1;
            }
            return max + 1 ;
        }

    }
}
