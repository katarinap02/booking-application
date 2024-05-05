using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class StatisticMonthsPageViewModel
    { 
    public AccommodationViewModel AccommodationViewModel { get; set; }

    public SeriesCollection SeriesCollection { get; set; }

    public SeriesCollection SeriesCollectionCancel { get; set; }

    public SeriesCollection SeriesCollectionRecommendation { get; set; }

    public int Year;

    public string[] Months { get; set; }

    public string[] MonthsC { get; set; }

    public string[] MonthsR { get; set; }

    public string[] MonthsD { get; set; }

    public string[] AllMonths { get; set; }

     public int MostBusyYear { get; set; }

    public Func<int, string> NumOfReservations { get; set; }

    public Func<int, string> NumOfCancellations { get; set; }

    public Func<int, string> NumOfRecommendation { get; set; }

    public AccommodationReservationService AccommodationReservationService { get; set; }

    public ReservationCancellationService ReservationCancellationService { get; set; }

    public DelayRequestService DelayRequestService { get; set; }

    public RenovationRecommendationService RenovationRecommendationService { get; set; }

    public User User { get; set; }
    public StatisticMonthsPageViewModel(User user, AccommodationViewModel acc, string yearString)
    {
        AccommodationViewModel = acc;
        AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
        DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
        RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
        User = user;
        //Year = Convert.ToInt32(yearString);
        Year = 2024;
        SeriesCollection = new SeriesCollection();
        SeriesCollectionCancel = new SeriesCollection();
        SeriesCollectionRecommendation = new SeriesCollection();
        AddYChart();
        Months = AccommodationReservationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => i.ToString()).ToArray();
        MonthsD = DelayRequestService.GetAllMonthsForAcc(acc.Id, Year).Select(i => i.ToString()).ToArray();
        MonthsR = RenovationRecommendationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => i.ToString()).ToArray();
        MonthsC = ReservationCancellationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => i.ToString()).ToArray();
        AllMonths = MonthsC.Concat(MonthsD).ToArray();
        NumOfReservations = value => value.ToString("N");
        NumOfCancellations = value => value.ToString("N");
        NumOfRecommendation = value => value.ToString("N");
        MostBusyYear = AccommodationReservationService.GetMostBusyYearForAcc(acc.Id);
    }

    private void AddYChart()
    {
        SeriesCollection.Add(new LineSeries
        { 
            Title = "Number of reservations",
            Values = new ChartValues<int>(AccommodationReservationService.GetAllReservationsForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionCancel.Add(new ColumnSeries
        {
            Title = "Number of reservation's delay",
            Values = new ChartValues<int>(DelayRequestService.GetAllDelaysForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionCancel.Add(new ColumnSeries
        {
            Title = "Number of cancelling reservations",
            Values = new ChartValues<int>(ReservationCancellationService.GetAllCancellationsForMonths(AccommodationViewModel.Id, Year))
        });

        SeriesCollectionRecommendation.Add(new ColumnSeries
        {
            Title = "Number of renovation recommendation",
            Values = new ChartValues<int>(RenovationRecommendationService.GetAllRecommendationForMonths(AccommodationViewModel.Id, Year))
        });


    }




}
}
