﻿using BookingApp.Application.Services.FeatureServices;
using BookingApp.Domain.Model.Features;
using BookingApp.Domain.RepositoryInterfaces.Features;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.Observer;
using BookingApp.WPF.View.HostPages;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels
{
    public class ForumCommentPageViewModel : IObserver, INotifyPropertyChanged
    {
        private string selectedReport;
        public string SelectedReport
        {
            set
            {
                if (selectedReport != value)
                {

                    selectedReport = value;
                    OnPropertyChanged("SelectedReport");
                }
            }
            get { return selectedReport; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ForumCommentViewModel> Forums { get; set; }

        public NavigationService NavService { get; set; }

        public User User { get; set; }

        public ForumService forumService { get; set; }

        public ForumCommentService forumCommentService { get; set; }

        public bool ToReport { get; set; }

        public ForumViewModel ForumViewModel { get; set; }

        public ForumCommentViewModel ForumCommentViewModel { get; set; }

        public MyICommand ForumCommand { get; set; }

        public MyICommand AddCommentCommand { get; set; }

        public MyICommand<ForumCommentViewModel> ReportCommand { get; set; }

        public ForumCommentPageViewModel(User user, NavigationService navService, ForumViewModel forum) {
            NavService = navService;
            User = user;
            ForumViewModel = forum;
            ForumCommentViewModel = new ForumCommentViewModel();
            SelectedReport = "All";
            ToReport = false;
            ForumCommand = new MyICommand(SortByReport);
            AddCommentCommand = new MyICommand(AddForumCommentHost);
            ReportCommand = new MyICommand<ForumCommentViewModel>(ReportComment);
            forumService = new ForumService(Injector.Injector.CreateInstance<IForumRepository>(), Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Forums = new ObservableCollection<ForumCommentViewModel>();
            forumCommentService = new ForumCommentService(Injector.Injector.CreateInstance<IForumCommentRepository>(), Injector.Injector.CreateInstance<IUserRepository>(), Injector.Injector.CreateInstance<IAccommodationReservationRepository>(), Injector.Injector.CreateInstance<IDelayRequestRepository>());
            Update();
        }

        private void ReportComment(ForumCommentViewModel model)
        {
            forumCommentService.ReportCommentById(model.Id);
            MessageBox.Show("Comment reported.");
            Update();
        }

        public void Update()
        {
            Forums.Clear();
            foreach (ForumComment forum in forumCommentService.GetAll())
            {
                if(forum.ForumId == ForumViewModel.Id)
                {
                    ForumCommentViewModel forumVm = new ForumCommentViewModel(forum);
                    if(!ToReport || !forumVm.IsUnabled)
                    {
                        Forums.Add(forumVm);
                    }
                    
                }
                
            }
            if(ForumViewModel.IsClosed)
            {
                MessageBox.Show("This forum is closed.");
            }
        }

        public void SortByReport()
        {

            if (SelectedReport != "All")
            {
                ToReport = true;
            }
            else
            {
                ToReport = false;
            }
            
            Update();

        }

        public void AddForumCommentHost()
        {
            if(!ForumViewModel.IsClosed) { 
            ForumComment f = forumCommentService.CreateComment(User.Id, ForumCommentViewModel.Comment,
                ForumViewModel.City, ForumViewModel.Country,
                ForumViewModel.Id);
            forumService.CalculateGuestHostComments(ForumViewModel.ToForum());
            ForumViewModel.Comments.Add(f.Id);
            forumService.Update(ForumViewModel.ToForum());
                MessageBox.Show("Comment added");
            Update();

            ForumCommentPage page = new ForumCommentPage(User, NavService, ForumViewModel);
            this.NavService.Navigate(page, NavService);
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
