﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.WPF.View.GuideTestWindows.GuideControls;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.View.GuideTestWindows.TestViewModels
{
    public class RequestStatsViewModel: ViewModelBase
    {
        private int GuideId;

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private SeriesCollection _tourStatistics;
        public SeriesCollection TourStatistics
        {
            get { return _tourStatistics; }
            set
            {
                _tourStatistics = value;
                OnPropertyChanged(nameof(TourStatistics));
            }
        }

        public ObservableCollection<string> Items { get; set; }

        private string _locationLanguageCombo;
        public string LocationLanguageCombo
        {
            get { return _locationLanguageCombo; }
            set
            {
                _locationLanguageCombo = value;   
                UpdateOptions();
                OnPropertyChanged(nameof(LocationLanguageCombo));
            }
        } // location ili language koji je selektovan

        public ObservableCollection<string> Options { get; set; } // lokacije ili jezici koji su dostupni

        private string _selectedOption;
        public string SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
                UpdateGraph();
            }
        }

        public ObservableCollection<string> timeOptions { get; set; }

        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
                UpdateGraph();
            }
        }

        public MyICommand location {  get; set; }
        public MyICommand language {  get; set; }

        private readonly TourRequestService tourRequestService = new TourRequestService(Injector.Injector.CreateInstance<ITourRequestRepository>());
        public RequestStatsViewModel(int guide_id)
        {
            GuideId = guide_id;
            location = new MyICommand(AddLocation);
            language = new MyICommand(AddLanguage);
            // Directly setting the fields to avoid triggering OnPropertyChanged prematurely
            _time = "Yearly";
            _locationLanguageCombo = "Language";

            // Initialize collections
            TourStatistics = new SeriesCollection();
            Items = new ObservableCollection<string> { "Language", "Location" };
            timeOptions = new ObservableCollection<string> { "Yearly", "Monthly - 2023", "Monthly - 2024" };
            Options = new ObservableCollection<string>();

            // Set Language and Location suggestions
            Language = tourRequestService.GetLanguageSuggestion();
            Location = tourRequestService.GetLocationSuggestion();

            // Update options based on the initial settings
            UpdateOptions();

            // Update graph with the initial settings, if SelectedOption is set
            if (SelectedOption != null)
            {
                UpdateGraph();
            }
        }

        public void AddLocation()
        {
            (string city, string country) = SplitStringByComma(Location);
            AddingLocation addingLocation = new AddingLocation(GuideId, city, country);
            addingLocation.Show();
        }

        public void AddLanguage() {
            AddingLanguage addingLanguage = new AddingLanguage(GuideId, Language);
            addingLanguage.Show();
        }

        public (string, string) SplitStringByComma(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input string cannot be null or empty");
            }

            // Find the index of the first comma
            int commaIndex = input.IndexOf(',');

            // If no comma is found, return the input string as the first part and an empty string as the second part
            if (commaIndex == -1)
            {
                return (input, string.Empty);
            }

            // Split the string into two parts
            string part1 = input.Substring(0, commaIndex).Trim();
            string part2 = input.Substring(commaIndex + 1).Trim();

            return (part1, part2);
        }


        public void UpdateOptions()
        {
            if (LocationLanguageCombo.Contains("Language"))
            {
                ShowLanguages();
            }
            else
            {
                ShowLocations();
            }
        }

        public void ShowLanguages()
        {
            Options.Clear();
            foreach (var language in tourRequestService.getAllLanguages())
            {
                Options.Add(language);
            }
            setDefaultOption();
        }

        public void ShowLocations()
        {
            Options.Clear();
            foreach (var location in tourRequestService.getAllLocations())
            {
                Options.Add(location);
            }
            setDefaultOption();
        }

        public void setDefaultOption()
        {
            if (Options.Count > 0)
            {
                _selectedOption = Options[0]; // Set directly to avoid triggering OnPropertyChanged
                OnPropertyChanged(nameof(SelectedOption));
                UpdateGraph();
            }
        }


        public void UpdateGraph()
        {
            if (SelectedOption == null)
            {
                return;
            }

            if (LocationLanguageCombo.Contains("Language"))
            {
                List<TourRequest> tourRequests = tourRequestService.getRequestsForLanguage(SelectedOption);
                if (Time.Equals("Yearly"))
                {
                    TourStatistics = getGraphYearly(tourRequests);
                }
                else if (Time.Contains("2023"))
                {
                    TourStatistics = getMonthlyGraph(2023, tourRequests);
                }
                else // 2024
                {
                    TourStatistics = getMonthlyGraph(2024, tourRequests);
                }
            }
            else // Location
            {
                string[] strings = SelectedOption.Split(", ");
                List<TourRequest> tourRequests = tourRequestService.getRequestsForLocation(strings[0], strings[1]);
                if (Time.Equals("Yearly"))
                {
                    TourStatistics = getGraphYearly(tourRequests);
                }
                else if (Time.Contains("2023"))
                {
                    TourStatistics = getMonthlyGraph(2023, tourRequests);
                }
                else // 2024
                {
                    TourStatistics = getMonthlyGraph(2024, tourRequests);
                }
            }
        }


        public SeriesCollection getGraphYearly(List<TourRequest> tourRequests)
        {
            List<int> stats = tourRequestService.GetYearlyStatistic(tourRequests);
            SeriesCollection series = new SeriesCollection
                                        {
                                            new ColumnSeries
                                            {
                                                Title = "2023",
                                                Values = new ChartValues<int> { stats[0] }
                                            },
                                            new ColumnSeries
                                            {
                                                Title = "2024",
                                                Values = new ChartValues<int> { stats[1] }
                                            }
                                        };
            return series;
        }

        public SeriesCollection getMonthlyGraph(int year, List<TourRequest> tourRequests)
        {
            List<int> stats = tourRequestService.GetMonthlyStatistics(tourRequests, year);
            List<string> monthNames = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            // Create a SeriesCollection
            SeriesCollection series = new SeriesCollection();

            // Populate the SeriesCollection with ColumnSeries for each month
            for (int i = 0; i < stats.Count; i++)
            {
                var columnSeries = new ColumnSeries
                {
                    Title = monthNames[i],
                    Values = new ChartValues<int> { stats[i] }
                };

                // Add the ColumnSeries to the SeriesCollection
                series.Add(columnSeries);
            }
            return series;
        }
    }
}
