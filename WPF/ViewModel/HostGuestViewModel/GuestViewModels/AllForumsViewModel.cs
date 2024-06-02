using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.View.Guest.GuestTools;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AllForumsViewModel : IObserver, INotifyPropertyChanged
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public ObservableCollection<ForumViewModel> Forums { get; set; }

        public ForumViewModel SelectedForum { get; set; }
        public ForumService ForumService { get; set; }

        // KOMANDE

        public GuestICommand CreateForumCommand { get; set; }
        public ObservableCollection<string> CountriesSearch { get; set; }
        public ObservableCollection<string> CitiesSearch { get; set; }
        public GuestICommand<object> ViewForumCommand { get; set; }

        private string citySearch;
        public string CitySearch
        {
            get
            {
                return citySearch;
            }
            set
            {
                if (value != citySearch)
                {
                    citySearch = value ?? string.Empty;
                    OnPropertyChanged(nameof(CitySearch));
                }
            }
        }

        private string countrySearch;
        public string CountrySearch
        {
            get
            {
                return countrySearch;
            }
            set
            {
                if (value != countrySearch)
                {
                    countrySearch = value ?? string.Empty;
                    LoadCitiesFromCSV();
                    OnPropertyChanged(nameof(CountrySearch));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GuestICommand SearchCommand { get; set; }
        public AllForumsPage Page { get; set; }
        public ComboBox ForumBox { get; set; }
        public AllForumsViewModel(User user, Frame frame, AllForumsPage page)
        {
            User = user;
            Frame = frame;
            Forums = new ObservableCollection<ForumViewModel>();
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            CreateForumCommand = new GuestICommand(OnCreateForum);
            ViewForumCommand = new GuestICommand<object>(OnViewForum);
            ForumBox = page.forumTypeBox;
            Page = page;
            // SelectedForum = new ForumViewModel();
            SearchCommand = new GuestICommand(OnSearch);
            CitiesSearch = new ObservableCollection<string>();
            CountriesSearch = new ObservableCollection<string>();
            CountriesSearch.Add("");
            LoadCountriesFromCSV();
            Update();

        }

        private void OnSearch()
        {
            List<string> queries = new List<string>();
           
            queries.Add(CitySearch); //cityQuery
            queries.Add(CountrySearch); //countryQuery
          

            Page.ForumListBox.ItemsSource = SearchForums(queries);
        }

        public List<ForumViewModel> SearchForums(List<string> queries)
        {


            ObservableCollection<ForumViewModel> totalForums = new ObservableCollection<ForumViewModel>();
            foreach (Forum forum in ForumService.GetAll())
                totalForums.Add(new ForumViewModel(forum));

            List<ForumViewModel> searchResults = new List<ForumViewModel>();
            switch (ForumBox.SelectedItem)
            {
                case ComboBoxItem allForumsItem when allForumsItem.Content.ToString() == "All forums" || allForumsItem.Content.ToString() == "Svi forumi":
                    searchResults = FilterForums(totalForums, queries);
                    break;
                case ComboBoxItem yourForumsItem when yourForumsItem.Content.ToString() == "Your forums" || yourForumsItem.Content.ToString() == "Vaši forumi":
                    searchResults = FilterUserForums(totalForums, queries);
                    break;

            }
          


            int totalItems = searchResults.Count;
            List<ForumViewModel> results = new List<ForumViewModel>();
            foreach (ForumViewModel forum in searchResults)
                results.Add(forum);

            return results;




        }

        private List<ForumViewModel> FilterForums(ObservableCollection<ForumViewModel> totalForums, List<string> queries)
        {
            List<ForumViewModel> filteredForums = totalForums.Where(forum => 
                                                                           (string.IsNullOrEmpty(queries[0]) || forum.City.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || forum.Country.ToUpper().Contains(queries[1].ToUpper()))
                                                                          
                                                                           ).ToList();
           
            return filteredForums.OrderByDescending(x => x.Date).ToList();
        }
        private List<ForumViewModel> FilterUserForums(ObservableCollection<ForumViewModel> totalForums, List<string> queries)
        {
            List<ForumViewModel> filteredForums = totalForums.Where(forum =>
                                                                           (string.IsNullOrEmpty(queries[0]) || forum.City.ToUpper().Contains(queries[0].ToUpper())) &&
                                                                           (string.IsNullOrEmpty(queries[1]) || forum.Country.ToUpper().Contains(queries[1].ToUpper())) &&
                                                                           (User.Id == forum.UserId)

                                                                           ).ToList();

            return filteredForums.OrderByDescending(x => x.Date).ToList();
        }


        private void OnViewForum(object sender)
        {
            Button button = sender as Button;
            SelectedForum = button.DataContext as ForumViewModel;
            Frame.Content = new ViewForumPage(User, Frame, SelectedForum);

        }

        private void OnCreateForum()
        {
            Frame.Content = new CreateForumPage(User, Frame);
        }

        public void Update()
        {
            Forums.Clear();
            switch (ForumBox.SelectedItem)
            {
                case ComboBoxItem allForumsItem when allForumsItem.Content.ToString() == "All forums" || allForumsItem.Content.ToString() == "Svi forumi":
                    ShowAllForums(Forums);
                    break;
                case ComboBoxItem yourForumsItem when yourForumsItem.Content.ToString() == "Your forums" || yourForumsItem.Content.ToString() == "Vaši forumi":
                    ShowYourForums(Forums);
                    break;
              
            }
            
        }

        private void ShowYourForums(ObservableCollection<ForumViewModel> forums)
        {
            List<Forum> sortedForums = ForumService.GetAll().OrderByDescending(x => x.Date).ToList(); 
            foreach (Forum forum in sortedForums)
            {
                if(forum.UserId == User.Id)
                {
                    ForumService.CalculateGuestHostComments(forum);
                    Forums.Add(new ForumViewModel(forum));
                }
               
            }
        }

        private void ShowAllForums(ObservableCollection<ForumViewModel> forums)
        {
            List<Forum> sortedForums = ForumService.GetAll().OrderByDescending(x => x.Date).ToList();
            foreach (Forum forum in sortedForums)
            {
                ForumService.CalculateGuestHostComments(forum);
                Forums.Add(new ForumViewModel(forum));
            }
        }

        public void ForumBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void LoadCountriesFromCSV()
        {


            string csvFilePath = "../../../Resources/Data/european_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    CountriesSearch.Add(values[0]);
                }
            }

        }

        private void LoadCitiesFromCSV()
        {
            CitiesSearch.Clear();
            CitiesSearch.Add("");
            string csvFilePath = "../../../Resources/Data/european_cities_and_countries.csv";

            using (var reader = new StreamReader(csvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[1].Equals(CountrySearch))
                        CitiesSearch.Add(values[0]);
                }
            }
            CitySearch = CountriesSearch[0];
        }
    }
}
