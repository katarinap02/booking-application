using BookingApp.Application.Services.FeatureServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
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
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ForumViewModel> Forums { get; set; }

        public ForumService forumService { get; set; }

        public NavigationService NavService { get; set; }

        public User User { get; set; }

        public ForumPageViewModel(User user, NavigationService navService)
        {

            NavService = navService;
            User = user;
            forumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Forums = new ObservableCollection<ForumViewModel>();
            Update();

        }

        public void Update()
        {
            Forums.Clear();
            foreach (Forum forum in forumService.GetAll())
            {
                Forums.Add(new ForumViewModel(forum));
            }
        }
    }
}
