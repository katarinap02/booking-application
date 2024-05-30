using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.View.Guest.GuestPages;
using BookingApp.WPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.GuestViewModels
{
    public class AllForumsViewModel : IObserver
    {
        public User User { get; set; }
        public Frame Frame { get; set; }
        public ObservableCollection<ForumViewModel> Forums { get; set; }

        public ForumViewModel SelectedForum { get; set; }
        public ForumService ForumService { get; set; }

        // KOMANDE

        public GuestICommand CreateForumCommand { get; set; }

        public GuestICommand<object> ViewForumCommand { get; set; }

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
            // SelectedForum = new ForumViewModel();
            Update();

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
            foreach (Forum forum in ForumService.GetAll())
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
            foreach (Forum forum in ForumService.GetAll())
            {
                ForumService.CalculateGuestHostComments(forum);
                Forums.Add(new ForumViewModel(forum));
            }
        }

        public void ForumBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
    }
}
