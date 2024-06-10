using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.Model.Rates;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Repository.RateRepository;
using BookingApp.WPF.View.GuideTestWindows.TestViewModels;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace BookingApp.WPF.View.GuideTestWindows
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window, INotifyPropertyChanged
    {
        private int GuideId;
        private TourViewModel tour;
        private static readonly GuideRateService guideRateService = new GuideRateService(Injector.Injector.CreateInstance<IGuideRateRepository>());
        private static readonly TourParticipantService tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
        private readonly UserService userService;
        public List<GuideRateViewModel> guideRates { get; set; }

        public string Text { get; set; }
        private string grade;
        public string Grade
        {
            get => grade;
            set
            {
                grade = value;
                OnPropertyChanged();
            }
        }

        public List<int> ints { get; set; }

        public ReviewsWindow(int guideId, TourViewModel tour)
        {
            ints = new List<int>();
            Text = "Reviews for tour: " + tour.Name;
            userService = new UserService(Injector.Injector.CreateInstance<IUserRepository>());
            GuideId = guideId;
            this.tour = tour;
            guideRates = new List<GuideRateViewModel>();
            DataContext = this;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            int Id = tour.Id;
            List<GuideRateViewModel> rates = guideRateService.getRatesByTour(Id);
            foreach (var guideRate in rates)
            {
                int tourist_id = guideRate.TouristId;
                guideRate.TouristName = userService.GetById(tourist_id).Username;
                int check = tourParticipantService.getjoinedCheckpoint(Id);
                guideRate.Checkpoint = tour.Checkpoints[check];
                ints.Add(guideRate.Language);
                ints.Add(guideRate.Knowledge);
                ints.Add(guideRate.TourInterest);
                guideRate.Language1 = ConvertNumberToRate(guideRate.Language);
                guideRate.Knowledge1 = ConvertNumberToRate(guideRate.Knowledge);
                guideRate.Interest1 = ConvertNumberToRate(guideRate.TourInterest);
                if (guideRate.Pictures != null && guideRate.Pictures.Count() > 0)
                {
                    int index = -1;
                    foreach (string pic in guideRate.Pictures.ToList())
                    {
                        index++;
                        guideRate.Pictures[index] = ConvertToRelativePath(pic);
                    }
                }
                else
                {
                    if (guideRate.Pictures == null)
                    {
                        guideRate.Pictures = new List<string>();
                    }
                    guideRate.Pictures.Add(ConvertToRelativePath("Resources/Images/no_image.jpg"));
                }
                guideRates.Add(guideRate);
            }
            Grade = "Average grade for tour: " + Math.Round(ints.Average(), 2).ToString();
            UpdatePieChart(ints);
            TouristListView.ItemsSource = guideRates;
        }

        private void UpdatePieChart(List<int> grades)
        {
            var gradeCounts = grades.GroupBy(g => g)
                                    .Select(g => new { Grade = g.Key, Count = g.Count() })
                                    .OrderBy(g => g.Grade)
                                    .ToList();

            var series = new SeriesCollection();

            foreach (var gradeCount in gradeCounts)
            {
                series.Add(new PieSeries
                {
                    Title = $"Grade {gradeCount.Grade}",
                    Values = new ChartValues<int> { gradeCount.Count },
                    DataLabels = true
                });
            }

            ReviewsPieChart.Series = series;
        }

        public string ConvertToRelativePath(string inputPath)
        {
            string pattern = @"\\";
            string replacedPath = Regex.Replace(inputPath, pattern, "/");
            if (replacedPath.StartsWith("Resources/Images/"))
            {
                replacedPath = "../../" + replacedPath;
            }
            return replacedPath;
        }

        public string ConvertNumberToRate(int num)
        {
            return num.ToString() + "/5";
        }

        private void Invalid(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Review set as invalid!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
