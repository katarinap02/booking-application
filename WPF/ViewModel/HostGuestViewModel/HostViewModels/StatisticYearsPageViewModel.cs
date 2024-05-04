using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class StatisticYearsPageViewModel
    {
        public AccommodationViewModel AccommodationViewModel { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public string[] Years { get; set; }

        public List<int> YearsList { get; set; }

        public Func<int, string> NumOfReservations {  get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public User User { get; set; }
        public StatisticYearsPageViewModel(User user, AccommodationViewModel acc) {
            AccommodationViewModel = acc;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            User = user;
            //Years1 = new[] { AccommodationReservationService.getAllYearsForAcc(acc.Id) };
            YearsList = AccommodationReservationService.getAllYearsForAcc(acc.Id);
            SeriesCollection = new SeriesCollection();
            AddYChart();
            Years = YearsList.Select(i => i.ToString()).ToArray();
            NumOfReservations = value => value.ToString("N");
        }

        private void AddYChart()
        {
            SeriesCollection.Add(new LineSeries { //ColumnSeries for other
                Title = "Number of reservations",
                Values = new ChartValues<int>(AccommodationReservationService.getAllReservationsForYears(AccommodationViewModel.Id))
            });
           

        }

       

        
    }
}
