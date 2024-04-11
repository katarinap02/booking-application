using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
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

namespace BookingApp.View.GuestPages
{
    /// <summary>
    /// Interaction logic for RatesByHostPage.xaml
    /// </summary>
    /// 
    
    public partial class RatesByHostPage : Page, IObserver
    {
        public ObservableCollection<GuestRateViewModel> GuestRates {  get; set; }
        public User User { get; set; }
        public GuestRateRepository GuestRateRepository { get; set; }
        public AccommodationRateService AccommodationRateService { get; set; }
        public Frame Frame { get; set; }

        

        public RatesByHostPage(User user, AccommodationRateService accommodationRateService, Frame frame)
        {
            InitializeComponent();
            this.User = user;
            this.AccommodationRateService = accommodationRateService;
            this.Frame = frame;
            this.GuestRateRepository = new GuestRateRepository();
            this.GuestRates = new ObservableCollection<GuestRateViewModel>();
            DataContext = this;
            Update();

        }
        
        public void Update()
        {
           GuestRates.Clear();
         /*  foreach(GuestRate guestRate in GuestRateRepository.GetAll()) { 
                if(IsAccommodationRated(guestRate.ReservationId, AccommodationRateService))
                {
                    GuestRates.Add(new GuestRateDTO(guestRate));
                }
            
           }*/
        }
        /*
        private bool IsAccommodationRated(int reservationId, AccommodationRateRepository accommodationRateRepository)
        {
            foreach(AccommodationRate accommodationRate in accommodationRateRepository.GetAll())
            {
                if(accommodationRate.ReservationId == reservationId)
                {
                    return true;
                }
            }
            return false;
        }*/
    }
}
