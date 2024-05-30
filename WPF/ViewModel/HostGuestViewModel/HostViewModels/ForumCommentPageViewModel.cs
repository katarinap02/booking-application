using BookingApp.Application.Services.FeatureServices;
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
    public class ForumCommentPageViewModel : IObserver, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ForumCommentViewModel> Forums { get; set; }

        public NavigationService NavService { get; set; }

        public User User { get; set; }

        public ForumService forumService { get; set; }

        public ForumCommentService forumCommentService { get; set; }

        public ForumViewModel ForumViewModel { get; set; }

        public ForumCommentPageViewModel(User user, NavigationService navService, ForumViewModel forum) {
            NavService = navService;
            User = user;
            ForumViewModel = forum;
            forumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Forums = new ObservableCollection<ForumCommentViewModel>();
            forumCommentService = new ForumCommentService(Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Update();
        }

        public void Update()
        {
            Forums.Clear();
            foreach (ForumComment forum in forumCommentService.GetAll())
            {
                if(forum.ForumId == ForumViewModel.Id)
                Forums.Add(new ForumCommentViewModel(forum));
            }
        }
    }
}
