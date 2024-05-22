using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.View.GuestPages;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class CreateForumSuccessfulViewModel
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public ForumViewModel NewForum { get; set; }
        public GuestICommand ViewForumCommand { get; set; }
        public GuestICommand ViewForumsCommand { get; set; }
        public GuestICommand ViewProfileCommand { get; set; }

        public GuestICommand CloseForumCommand { get; set; }

        public ForumService ForumService { get; set; }
        public CreateForumSuccessfulViewModel(User user, Frame frame, ForumViewModel newForum) {

            User = user;
            Frame = frame;
            NewForum = newForum;
            ViewForumCommand = new GuestICommand(OnViewForum);
            ViewForumsCommand = new GuestICommand(OnViewForums);
            ViewProfileCommand = new GuestICommand(OnViewProfile);
            CloseForumCommand = new GuestICommand(OnCloseForum);
            ForumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>());
        }

        private void OnCloseForum()
        {
            NewForum.IsClosed = true;
            ForumService.Update(NewForum.ToForum());
            Frame.Content = new CloseThreadSuccessfulPage(User, Frame, NewForum);
        }

        private void OnViewProfile()
        {
            Frame.Content = new ProfileInfo(User, Frame);
        }

        private void OnViewForums()
        {
            Frame.Content = new ProfileForumsPage(User, Frame);
        }

        private void OnViewForum()
        {
            Frame.Content = new ProfileViewForum(User, Frame, NewForum);
        }
    }
}
