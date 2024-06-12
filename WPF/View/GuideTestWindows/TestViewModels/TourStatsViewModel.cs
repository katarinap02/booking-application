using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Utilities.PDF_generator;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class TourStatsViewModel: ViewModelBase
    {
        public SeriesCollection series { get; set; }
        public ObservableCollection<TourViewModel> Tours { get; set; } = new ObservableCollection<TourViewModel>();

        private string _caption;
        public string Caption
        {
            get { return _caption; }
            set { _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        private string _combo;
        public string Combo
        {
            get { return _combo; }
            set { _combo = value;
                OnPropertyChanged(nameof(Combo));
                ChangeMostVisited();
            }
        }

        private string _tourName;
        public string TourName
        {
            get { return _tourName; }
            set
            {
                _tourName = value;
                OnPropertyChanged(nameof(TourName));
            }
        }

        private string _participantNumber;
        public string ParticipantNumber
        {
            get { return _participantNumber; }
            set
            {
                _participantNumber = value;
                OnPropertyChanged(nameof(ParticipantNumber));
            }
        }

        private TourViewModel _selectedTour;
        public TourViewModel SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                UpdateAgeStatistics(_selectedTour.Id);
            }
        }
        private readonly TourService tourService = new TourService(Injector.Injector.CreateInstance<ITourRepository>());
        private readonly GuideInfoService guideInfoService = new GuideInfoService();
        private readonly TourReservationService reservationService = new TourReservationService(Injector.Injector.CreateInstance<ITourReservationRepository>()); 
        private int GuideId;
        public List<int> age { get; set; }
        public MyICommand PDF {  get; set; }

        public TourStatsViewModel(int guide_id)
        {
            PDF = new MyICommand(GeneratePDF);
            age = new List<int>();
            GuideId = guide_id;
            series = new SeriesCollection();
            Combo = "System.Windows.Controls.ComboBoxItem: All time";
            getGridData();            
        }

        public void getGridData()
        {
            List<Tour> tours = tourService.findFinnishedToursByGuide(GuideId);
            ObservableCollection<TourViewModel> newViewModels = new ObservableCollection<TourViewModel>();
            foreach (Tour tour in tours)
            {
                newViewModels.Add(new TourViewModel(tour));
            }
            Tours = newViewModels;
            if(tours.Count > 0)
            {
                SelectedTour = new TourViewModel(tours[0]);
            }            
        }

        private void UpdateAgeStatistics(int tourId)
        {
            Caption = tourService.GetTourById(tourId).Name;
            List<int> ages = tourService.GetAgeStatistic(tourId);
            age = ages;
            series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Under 18",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[0]) },
                    Fill = Brushes.DodgerBlue,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Between 18 and 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[1]) },
                    Fill = Brushes.Coral,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Above 50",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(ages[2]) },
                    Fill = Brushes.DeepPink,
                    DataLabels = true
                }
            };
            OnPropertyChanged(nameof(series));
        }


        private void ChangeMostVisited()
        {
            if(Combo == "System.Windows.Controls.ComboBoxItem: All time")
            {
                ShowMostVisitedTourAllTime();
            }
            else
            {
                int selectedYear = 0;
                if (Combo.Contains("2023")) { selectedYear = 2023; }
                else if (Combo.Contains("2024")) { selectedYear = 2024; }  
                ShowMostVisitedTourPreviousYears(selectedYear);
            }
        }

        private void ShowMostVisitedTourAllTime()
        {
            Tour mostVisitedTourAllTime = tourService.GetMostPopularTourForGuide(GuideId);
            
            if (mostVisitedTourAllTime != null)
            {
                TourName = mostVisitedTourAllTime.Name;
                int participantCount = reservationService.GetNumberOfJoinedParticipants(mostVisitedTourAllTime.Id);
                ParticipantNumber= $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantNumber = "";
            }


        }

        private void ShowMostVisitedTourPreviousYears(int year)
        {
            Tour mostVisitedTourPreviousYears = tourService.GetMostPopularTourForGuideInYear(GuideId, year);

            if (mostVisitedTourPreviousYears != null)
            {
                TourName = mostVisitedTourPreviousYears.Name;
                int participantCount = reservationService.GetNumberOfJoinedParticipants(mostVisitedTourPreviousYears.Id);
                ParticipantNumber = $"Participants: {participantCount}";
            }
            else
            {
                TourName = "No data available";
                ParticipantNumber = "";
            }

        }

        public void GeneratePDF()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Save PDF File";
                saveFileDialog.FileName = "TourStatistics.pdf";
                Tour mostVisitedTourAllTime = tourService.GetMostPopularTourForGuide(GuideId);
                Tour mostVisitedTour2023 = tourService.GetMostPopularTourForGuideInYear(GuideId, 2023);
                Tour mostVisitedTour2024 = tourService.GetMostPopularTourForGuideInYear(GuideId, 2024);
                GuideInformation information = guideInfoService.GetByGuideId(GuideId);
                string fullname = information.Name + " " + information.Surname;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    var documentData = new DocumentData
                    {
                        ApplicationLogoPath = "../../../WPF/Resources/Images/logo.png",
                        ApplicationName = "Booking Application",
                        GuideName = fullname,
                        ContactEmail = information.Email,
                        ContactPhone = information.PhoneNumber,
                        GeneratedTime = DateTime.Now,
                        PieChart = ChartGenerator.GeneratePieChart(below18: age[0], between18And50: age[1], above50: age[2]),
                        Below18 = age[0],
                        Between18And50 = age[1],
                        Above50 = age[2],
                        TourName = SelectedTour.Name,
                        MostVisitedTours = new MostVisitedTours
                        {
                            AllTime = mostVisitedTourAllTime.Name,
                            Year2024 = mostVisitedTour2024.Name,
                            Year2023 = mostVisitedTour2023.Name
                        }
                    };

                    PDFGenerator.Generate(documentData, filePath);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
        }


    }
}
