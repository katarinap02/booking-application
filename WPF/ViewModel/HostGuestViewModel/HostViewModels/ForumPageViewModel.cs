using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.View.HostPages;
using BookingApp.WPF.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class ForumPageViewModel : INotifyPropertyChanged, IObserver
    {

        private string selectedLocation;
        public string SelectedLocation
        {
            set
            {
                if (selectedLocation != value)
                {

                    selectedLocation = value;
                    OnPropertyChanged("SelectedLocation");
                }
            }
            get { return selectedLocation; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ForumViewModel> Forums { get; set; }

        public ForumService forumService { get; set; }

        public NavigationService NavService { get; set; }

        public String LocationCity { get; set; }

        public User User { get; set; }

        public MyICommand SelectCityCommand {  get; set; }

        public MyICommand<ForumViewModel> NavigateToForumPageCommand { get; set; }

        private void Execute_NavigateToForumPageCommand(ForumViewModel forum)
        {
            ForumCommentPage page = new ForumCommentPage(User, NavService, forum);
            this.NavService.Navigate(page, NavService);
            Update();
        }

        public ForumPageViewModel(User user, NavigationService navService)
        {

            NavService = navService;
            User = user;
            forumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Forums = new ObservableCollection<ForumViewModel>();
            SelectCityCommand = new MyICommand(SortByCity);
            SelectedLocation = "All";
            LocationCity = "";
            NavigateToForumPageCommand = new MyICommand<ForumViewModel>(Execute_NavigateToForumPageCommand);
            Update();

        }

        public void Update()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetForumsForHost(User))
            {
                forumService.CalculateGuestHostComments(forum);
                if (forum.City.ToLower().Equals(LocationCity.ToLower()) || LocationCity.Equals(""))
                Forums.Add(new ForumViewModel(forum));
            }
        }

        public void SortByCity()
        {

            if (SelectedLocation != "All")
            {
                LocationCity = SelectedLocation;
            }
            else
            {
                LocationCity = "";
            }
            Update();

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
