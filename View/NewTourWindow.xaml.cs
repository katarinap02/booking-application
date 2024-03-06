using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Model;
using BookingApp.DTO;
using System.Windows;

namespace BookingApp.View
{
    public partial class NewTourWindow
    {
        private readonly TourRepository _tourRepository;
        public TourDTO Tour { get; set; }
        public List<DateTime> Dates = new List<DateTime>();

        public NewTourWindow() {
            InitializeComponent();
            DataContext = this;
            _tourRepository = new TourRepository();
            Tour = new TourDTO();
        }

        public void AddTour_Click(object sender, EventArgs e) {
            MessageBox.Show(Dates.Count().ToString());
        }

    }
}
