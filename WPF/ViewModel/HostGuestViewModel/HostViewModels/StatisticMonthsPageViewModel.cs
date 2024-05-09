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
using System.Globalization;
using System.ComponentModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using BookingApp.WPF.View.HostPages;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class StatisticMonthsPageViewModel: INotifyPropertyChanged
    {

        private string selectedYear;
        public string SelectedYear
        {
            set
            {
                if (selectedYear != value)
                {

                    selectedYear = value;
                    OnPropertyChanged("SelectedYear");
                }
            }
            get { return selectedYear; }
        }
    public AccommodationViewModel AccommodationViewModel { get; set; }

    public SeriesCollection SeriesCollection { get; set; }

    public SeriesCollection SeriesCollectionCancel { get; set; }

    public SeriesCollection SeriesCollectionRecommendation { get; set; }

    public int Year;
    public NavigationService NavService { get; set; }
    public string[] Months { get; set; }

    public string[] MonthsC { get; set; }

    public string[] MonthsR { get; set; }

    public string[] MonthsD { get; set; }

    public string[] AllMonths { get; set; }

     public string MostBusyMonth { get; set; }

    public Func<int, string> NumOfReservations { get; set; }

    public Func<int, string> NumOfCancellations { get; set; }

    public Func<int, string> NumOfRecommendation { get; set; }

    public MyICommand SelectionChangedCommand { get; set; }

    public AccommodationReservationService AccommodationReservationService { get; set; }

    public ReservationCancellationService ReservationCancellationService { get; set; }

    public DelayRequestService DelayRequestService { get; set; }

    public RenovationRecommendationService RenovationRecommendationService { get; set; }

    public User User { get; set; }
    public StatisticMonthsPageViewModel(User user, AccommodationViewModel acc, string yearString, NavigationService navService)
    {
        AccommodationViewModel = acc;
        AccommodationReservationService = new AccommodationReservationService(Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
        ReservationCancellationService = new ReservationCancellationService(Injector.Injector.CreateInstance<IReservationCancellationRepository>());
        DelayRequestService = new DelayRequestService(Injector.Injector.CreateInstance<IDelayRequestRepository>());
        RenovationRecommendationService = new RenovationRecommendationService(Injector.Injector.CreateInstance<IRenovationRecommendationRepository>());
        User = user;
        NavService = navService;
        Year = Convert.ToInt32(yearString);
        SeriesCollection = new SeriesCollection();
        SeriesCollectionCancel = new SeriesCollection();
        SeriesCollectionRecommendation = new SeriesCollection();
        AddYChart();
        SelectedYear = yearString;
        Months = AccommodationReservationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsD = DelayRequestService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsR = RenovationRecommendationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        MonthsC = ReservationCancellationService.GetAllMonthsForAcc(acc.Id, Year).Select(i => GetMonthName(i)).ToArray();
        AllMonths = MonthsC.Concat(MonthsD).ToArray();
        NumOfReservations = value => value.ToString("N");
        NumOfCancellations = value => value.ToString("N");
        NumOfRecommendation = value => value.ToString("N");
        MostBusyMonth = GetMonthName(AccommodationReservationService.GetMostBusyMonth(acc.Id, Year));
        SelectionChangedCommand = new MyICommand(NavigatePage);
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
        public string GetMonthName(int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12)
            {
                return "Invalid Month";
            }

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            return cultureInfo.DateTimeFormat.GetMonthName(monthNumber);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NavigatePage()
        {

            if (SelectedYear != "All")
            {
                StatisticMonthsPage page = new StatisticMonthsPage(User, AccommodationViewModel, SelectedYear, NavService);
                this.NavService.Navigate(page);
            }
            else
            {
                StatisticYearsPage page = new StatisticYearsPage(User, AccommodationViewModel, NavService);
                this.NavService.Navigate(page);
            }

        }




    }
}
