using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
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

        public SeriesCollection SeriesCollectionCancel { get; set; }

        public SeriesCollection SeriesCollectionRecommendation { get; set; }

        public string[] Years { get; set; }

        public int MostBusyYear { get; set; }

        public List<int> YearsList { get; set; }

        public Func<int, string> NumOfReservations {  get; set; }

        public Func<int, string> NumOfCancellations { get; set; }

        public Func<int, string> NumOfRecommendation { get; set; }

        public AccommodationReservationService AccommodationReservationService { get; set; }

        public ReservationCancellationService ReservationCancellationService { get; set; }

        public DelayRequestService DelayRequestService { get; set; }

        public RenovationRecommendationService RenovationRecommendationService { get; set; }

        public User User { get; set; }
        public StatisticYearsPageViewModel(User user, AccommodationViewModel acc) {
            AccommodationViewModel = acc;
            AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
            DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
            RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
            User = user;
            YearsList = AccommodationReservationService.GetAllYearsForAcc(acc.Id);
            SeriesCollection = new SeriesCollection();
            SeriesCollectionCancel = new SeriesCollection();
            SeriesCollectionRecommendation = new SeriesCollection();
            AddYChart();
            Years = YearsList.Select(i => i.ToString()).ToArray();
            NumOfReservations = value => value.ToString("N");
            NumOfCancellations = value => value.ToString("N");
            NumOfRecommendation = value => value.ToString("N");
            MostBusyYear = AccommodationReservationService.GetMostBusyYearForAcc(acc.Id);
        }

        private void AddYChart()
        {
            SeriesCollection.Add(new LineSeries { //ColumnSeries for other
                Title = "Number of reservations",
                Values = new ChartValues<int>(AccommodationReservationService.GetAllReservationsForYears(AccommodationViewModel.Id))
            });

            SeriesCollectionCancel.Add(new ColumnSeries
            {
                Title = "Number of reservation's delay",
                Values = new ChartValues<int>(DelayRequestService.GetAllDelaysForYears(AccommodationViewModel.Id))
            });

            SeriesCollectionCancel.Add(new ColumnSeries
            {
                Title = "Number of cancelling reservations",
                Values = new ChartValues<int>(ReservationCancellationService.GetAllCancellationsForYears(AccommodationViewModel.Id))
            });

            SeriesCollectionRecommendation.Add(new ColumnSeries
            {
                Title = "Number of renovation recommendation",
                Values = new ChartValues<int>(RenovationRecommendationService.GetAllRecommendationsForYears(AccommodationViewModel.Id))
            });


        }

       

        
    }
}
