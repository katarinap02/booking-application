using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Observer;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class ProfileForumsViewModel : IObserver
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public ObservableCollection<ForumViewModel> Forums { get; set;}

        public ForumViewModel SelectedForum { get; set; }
        public ForumService ForumService { get; set; }

        // KOMANDE

        public GuestICommand CreateForumCommand { get; set; }

        public GuestICommand<object> ViewForumCommand { get; set; }
        public ProfileForumsViewModel(User user, Frame frame)
        {
            User = user;
            Frame = frame;
            Forums = new ObservableCollection<ForumViewModel>();
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>());
            CreateForumCommand = new GuestICommand(OnCreateForum);
            ViewForumCommand = new GuestICommand<object>(OnViewForum);
           // SelectedForum = new ForumViewModel();
            Update();
            
        }

        private void OnViewForum(object sender)
        {
            Button button = sender as Button;
            SelectedForum = button.DataContext as ForumViewModel;
            Frame.Content = new ProfileViewForum(User, Frame, SelectedForum);
            
        }

        private void OnCreateForum()
        {
            Frame.Content = new CreateForumPage(User, Frame);
        }

        public void Update()
        {
            Forums.Clear();
            foreach(Forum forum in ForumService.GetAll())
            {
                Forums.Add(new ForumViewModel(forum));
            }
        }
    }
}
