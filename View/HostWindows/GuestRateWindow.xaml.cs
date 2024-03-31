using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
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

namespace BookingApp.View.GuestWindows
{
    /// <summary>
    /// Interaction logic for RateGuestWindow.xaml
    /// </summary>
    public partial class RateGuestWindow : Window
    {
        public AccommodationReservationDTO accommodationReservationDTO {  get; set; }

        public GuestRateDTO guestRateDTO { get; set; }

        public GuestRateRepository guestRateRepository { get; set; }

        
        
        public RateGuestWindow(AccommodationReservationDTO ac)
        {
            InitializeComponent();
            accommodationReservationDTO = ac;
            guestRateDTO = new GuestRateDTO();
            guestRateRepository = new GuestRateRepository();
            DataContext = this;

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(guestRateDTO.IsValid)
            {
                guestRateDTO.ReservationId = accommodationReservationDTO.Id;
                guestRateDTO.GuestId = accommodationReservationDTO.GuestId;
                guestRateDTO.AccommodationId = accommodationReservationDTO.AccommodationId;
                guestRateRepository.Add(guestRateDTO.toGuestRate());
                MessageBox.Show("Guest rate added.");
                Close();
            }
            else
            {

                MessageBox.Show("Please enter rating from 1 - 5.");

            }
        }
    }
}
