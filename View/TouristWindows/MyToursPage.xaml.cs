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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View.TouristWindows
{
    /// <summary>
    /// Interaction logic for MyToursPage.xaml
    /// </summary>
    public partial class MyToursPage : Page
    {
        public ObservableCollection<Tour> Tours { get; set; }

        public Tour SelectedTour { get; set; }
        private readonly TourReservationRepository _repository;
        public MyToursPage(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _repository = new TourReservationRepository();
            Tours = new ObservableCollection<Tour>(_repository.FindMyTours(userId));
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
