﻿using BookingApp.Application.Services.RateServices;
using BookingApp.Application.Services.ReservationServices;
using BookingApp.Domain.RepositoryInterfaces.Rates;
using BookingApp.Domain.RepositoryInterfaces.Reservations;
using BookingApp.WPF.ViewModel.GuideTouristViewModel;
using BookingApp.WPF.ViewModel.HostGuestViewModel.HostViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel.GuideViewModel
{
    public class ReviewsWindowViewModel
    {
        private readonly GuideRateService _guideRateService;
        private readonly TourParticipantService _tourParticipantService;

        public ObservableCollection<GuideRateViewModel> guideRateViewModels { get; set; }
        public GuideRateViewModel selectedRate { get; set; }
        public MyICommand InvalidCommand {  get; set; }
        public MyICommand CheckpointCommand { get; set; } 

        public ReviewsWindowViewModel(int tour_id)
        {
            _guideRateService = new GuideRateService(Injector.Injector.CreateInstance<IGuideRateRepository>());
            _tourParticipantService = new TourParticipantService(Injector.Injector.CreateInstance<ITourParticipantRepository>());
            getData(tour_id);
            InvalidCommand = new MyICommand(Invalid_Click);
            CheckpointCommand = new MyICommand(Checkpoint_Click);
        }

        private void getData(int tour_id)
        {
            guideRateViewModels = new ObservableCollection<GuideRateViewModel>();
            foreach (GuideRateViewModel rate in _guideRateService.getRatesByTour(tour_id))
            {
                guideRateViewModels.Add(rate);
            }
        }

        private void Invalid_Click()
        {
            if (selectedRate == null)
            {
                MessageBox.Show("PLease select a review in order to mark it as invalid");
            }
            else
            {
                _guideRateService.markAsInvalid(selectedRate.Id);
                MessageBox.Show("Review marked as invalid", "Reviews Notification");
            }
        }

        private void Checkpoint_Click()
        {
            if (selectedRate == null)
            {
                MessageBox.Show("PLease select a review");
            }
            else
            {
                int checkpoint = _tourParticipantService.getjoinedCheckpoint(selectedRate.TourId);
                MessageBox.Show("Joined checkpoint number: " + checkpoint.ToString());
            }

        }
    }
}
