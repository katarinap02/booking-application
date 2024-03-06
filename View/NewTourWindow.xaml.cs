using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;

namespace BookingApp.View
{
    public partial class NewTourWindow
    {
        private readonly TourRepository _tourRepository;
        public static ObservableCollection<Tour> Tours;
        public List<DateTime> TourDates = new List<DateTime>();

        public NewTourWindow() {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            Tours = new ObservableCollection<Tour>();
        }



    }
}
