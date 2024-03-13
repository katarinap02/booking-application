using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Available tours window in the same city as the selected one
    /// Interaction logic for AvailableToursWindow.xaml
    /// </summary>
    public partial class AvailableToursWindow : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour NewSelectedTour { get; set; }
        private readonly TourRepository _repository;
        private int InsertedNumberOfParticipants;
        public AvailableToursWindow(Tour selectedTour, int insertedNumberOfParticipans)
        {
            InitializeComponent();
            DataContext = this;
            NewSelectedTour = selectedTour;
            InsertedNumberOfParticipants = insertedNumberOfParticipans;
            _repository = new TourRepository();
            Tours = new ObservableCollection<Tour>(_repository.GetTourByCountryWithAvailablePlaces(NewSelectedTour.Country));
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            TourReservationWindow tourReservationWindow = new TourReservationWindow(NewSelectedTour, InsertedNumberOfParticipants);
            tourReservationWindow.ShowDialog();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
